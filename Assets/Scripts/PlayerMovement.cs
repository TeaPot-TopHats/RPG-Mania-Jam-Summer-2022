using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables you can alter in Unity
    //Movement
    public float moveSpeed = 5f;

    //Ladder
    public float onLadderSpeed = 5f;
    public float defaultGravity;
    public bool onTopOfLadder;
    public bool climbingLadder;

    //Jump
    public float jumpStrength = 10f;

    //Double Jump
    public bool allowDoubleJump = true;
    public bool alreadyDoubleJumped;
    public bool jump; //stores if the player is jumping

    //Dash
    public bool allowDash = true;
    public float dash_CooldownTime = 2f;
    public float dashStregth = 8f;
    public float dashTime = 0.2f; //how long to dash for

    public float timeWhenDashEnds; //stores current time + dashTime, telling the ifs when the dash ends
    public bool dash;
    public bool alreadyDashed;
    public float timeWhenCooldownEnds; //stores current time + dash_cooldown time
    public bool isDashing; //is the dash on?


    //Sprite
    private bool facingLeft = true;

    //Variables that store input
    public float movement;
    public float ladderMovement;

    //stores which side of the player is getting collided
    public float collisionSide;

    //Physics
    private Rigidbody2D Rigid2D;

    //Awake gets called before Start, always
    private void Awake()
    {
        //Gets Rigidbody2D
        Rigid2D = GetComponent<Rigidbody2D>();
        defaultGravity = Rigid2D.gravityScale;
    }

    //Called before first frame
    void Start()
    {
    }

    // Update per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");  //detects left and right for joystick and keyboard input, can be -1 or 1
        ladderMovement = Input.GetAxisRaw("Vertical");  //detects up and down for ladder movement, can be -1 or 1
        

        //Detects jump key
        if (Input.GetButtonDown("Jump") && collisionSide == 1 && !climbingLadder)
        {
            jump = true;
        }
        if (Input.GetButtonDown("Jump") && collisionSide == 0 && allowDoubleJump && !alreadyDoubleJumped && !climbingLadder)
        {
            jump = true;
            alreadyDoubleJumped = true;
        }


        //Dash
        if (alreadyDashed && Time.time >= timeWhenCooldownEnds)
        {
            alreadyDashed = false;
        }
        //Left Shift and you are pressing left or right
        if (Input.GetKeyDown(KeyCode.LeftShift) && movement != 0 && /*!alreadyDashed &&*/ allowDash)
        {
            dash = true;
        }

    }
    //Unity likes this better for physics and stuff
    private void FixedUpdate()
    {
        //moves the player
        transform.position += new Vector3(movement * Time.deltaTime * moveSpeed, 0)  ; //moves the player in the x direction,

        //Jumps
        if (jump)
        {
            Rigid2D.velocity = new Vector2(Rigid2D.velocity.x, 0); //makes velocity 0 before jumping
            Rigid2D.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            jump = false;
        }


        //Ladder
        if (onTopOfLadder && ladderMovement != 0)
        {
            climbingLadder = true;
        }
        if (climbingLadder && ladderMovement != 0)
        {
            alreadyDoubleJumped = false;

            Rigid2D.velocity = new Vector2(0, 0);
            Rigid2D.gravityScale = 0f;

            transform.position += new Vector3(0, ladderMovement * Time.deltaTime * onLadderSpeed, 0) ;
        }
        if (climbingLadder == false)
        {
            Rigid2D.gravityScale = defaultGravity;
        }


        //Dash
        if (dash && !climbingLadder)
        {
            Debug.Log("Dash");
            timeWhenCooldownEnds = Time.time + dash_CooldownTime;

            isDashing = true;
            timeWhenDashEnds = Time.time + dashTime;
            alreadyDashed = true;
            dash = false;
        }
        if (isDashing && Time.time < timeWhenDashEnds && movement != 0)
        {
            Rigid2D.velocity = transform.right * movement * dashStregth;
        }
        if ((isDashing && Time.time > timeWhenDashEnds) || movement == 0)
        {
            isDashing = false;
            Rigid2D.velocity = new Vector2(0, Rigid2D.velocity.y);
        }


        //Flip Sprite
        if (movement > 0 && facingLeft)
        {
            FlipSprite();
        }
        if (movement < 0 && !facingLeft)
        {
            FlipSprite();
        }

        void FlipSprite()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingLeft = !facingLeft;
        }
    }


    //Detection of Collision. collisionSide tells you if it's hitting the top (-1) or the bottom (1) of the player collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D hitPos in collision.contacts)
        {
            //Debug.Log("Player is hitting side of y: " + hitPos.normal.y); 
            collisionSide = hitPos.normal.y;
            //resets double jump when it touches bottom
            if (collisionSide == 1)
            {
                alreadyDoubleJumped = false;
            }
        }       
    }
    //it sets the collision side to 0 because it stops touching the object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisionSide == 1)
        {
            collisionSide = 0;
            //Debug.Log("Player not touching");
        }
    }


    //Detects if player is touching ladder
    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            onTopOfLadder = true;
            //Debug.Log("Player onLadder TRUE");
        }
    }
    private void OnTriggerExit2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            climbingLadder = false;
            onTopOfLadder = false;
            //Debug.Log("Player onLadder FALSE");
        }
    }
}

//TODO: MOVEMENT SCRIPT - Comment Everything (specially dash)
//TODO: MOVEMENT SCRIPT - Close range attacks, make invisible collider and isTrigger to hit
//TODO: MOVEMENT SCRIPT - Organize things into either functions or separate scripts
