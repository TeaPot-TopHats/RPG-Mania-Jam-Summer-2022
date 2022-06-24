using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //I've temporarly made all variables public for debugging. We must change it before we release the game demo

    //Movement
    public float moveSpeed = 5f; //movement speed
    public float movement; //stores movement, left or right

    //Ladder
    public float ladderMovement; //stores ladder movement, up or down
    public float onLadderSpeed = 5f; //speed on ladder
    public float defaultGravity; //When climbing a ladder gravity is set to 0, we need to reset it at one point. This variable saves the default one
    public bool onTopOfLadder; //saves if the player is on the ladder but not climbing
    public bool climbingLadder; //stores if the players is climbing the ladder

    //Jump
    public float jumpStrength = 10f; //strength of the jump

    //Double Jump
    public bool allowDoubleJump = true; //allow the skill of double jumping
    public bool alreadyDoubleJumped; //
    public bool jump; //stores if the player should jump this allows the fixed update function to work. See fixed update function for the code

    //Crouch
    public float crouchMoveSpeed = 2f;
    public bool crouch;
    public float default_ColliderSizeY;
    public float crouchColliderSizeY = 0.5f;
    public Sprite pusheen;
    public Sprite pusheen_crouch;
    public bool touchingCrouching;

    //Dash
    public bool allowDash = true; //allows the skill of double jumping
    public float dash_CooldownTime = 2f; //the cool down time to use the skill
    public float dashStregth = 8f; //how fast the dash is
    public float dashTime = 0.2f; //how long to dash for

    public float timeWhenDashEnds; //stores current time + dashTime, telling the ifs when the dash ends
    public bool dash; //stores if the player should dash, works for the fixed update function
    public bool alreadyDashed; //stores if the player already dashed
    public float timeWhenCooldownEnds; //stores current time + dash_cooldown time
    public bool isDashing; //is the player currently dashing

    //Sprite
    private bool facingLeft = true; //it's true by default because the sprite faces left

    public float collisionSide; //stores which side of the player is getting collided, ONLY up or down NOT right or left. See OnCollisionEnter2D

    //Physics
    private Rigidbody2D Rigid;
    public BoxCollider2D PlayerCollider;
    private SpriteRenderer SpriteR;

    //Collider that checks crouch
    public BoxCollider2D CrouchCheckCollider;

    //Awake gets called before Start, always
    private void Awake()
    {
        //Gets Rigidbody2D
        Rigid = GetComponent<Rigidbody2D>();
        defaultGravity = Rigid.gravityScale; //Grabs the default gravity from rigid body

        //Get Collider
        PlayerCollider = GetComponent<BoxCollider2D>();
        default_ColliderSizeY = PlayerCollider.size.y;

        //Gets Sprite Renderer
        SpriteR = GetComponent<SpriteRenderer>();
        SpriteR.sprite = pusheen;
    }

    //Called before first the frame
    void Start()
    {
    }


    // Update per frame
    void Update()
    {
        //Movement
        movement = Input.GetAxisRaw("Horizontal");  //detects left and right for joystick and keyboard input, can be -1 or 1
        ladderMovement = Input.GetAxisRaw("Vertical");  //detects up and down for ladder movement, can be -1 or 1
        

        //Detects jump key
        if (Input.GetButtonDown("Jump") && collisionSide == 1 && !climbingLadder) //if collision side is the bottom and you are not climbing a ladder
        {
            jump = true;
        }
        if (Input.GetButtonDown("Jump") && collisionSide == 0 && allowDoubleJump && !alreadyDoubleJumped && !climbingLadder) //if collision side 0 (mid air) you can double jump if you have not alreadyDouble jumped
        {
            jump = true;
            alreadyDoubleJumped = true;
        }

        
        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl) && !climbingLadder)
        {
            crouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !touchingCrouching)
        {
            crouch = false;
        }


        //Dash
        if (alreadyDashed && Time.time >= timeWhenCooldownEnds)
        {
            alreadyDashed = false;
        }
        //Left Shift and you are pressing left or right
        if (Input.GetKeyDown(KeyCode.LeftShift) && movement != 0 && !alreadyDashed && allowDash)
        {
            dash = true;
        }

    }


    //Unity likes this better for physics and stuff
    private void FixedUpdate()
    {
        //moves the player
        MovePlayer();

        //Jumps
        Jump();

        //Crouch
        Crouch();

        //Ladder
        LadderMovement();

        //Dash
        DashSkill();

        //Flip Sprite
        if (movement > 0 && facingLeft)
        {
            FlipSprite();
        }
        if (movement < 0 && !facingLeft)
        {
            FlipSprite();
        }
        
    }


    void MovePlayer()
    {
        if (!crouch)
        {
            transform.position += new Vector3(movement * Time.deltaTime * moveSpeed, 0); //moves the player in the x direction,
        }
        if (crouch)
        {
            transform.position += new Vector3(movement * Time.deltaTime * crouchMoveSpeed, 0); //moves the player in the x direction,
        }
    }
    
    void Jump()
    {
        if (jump)
        {
            Rigid.velocity = new Vector2(Rigid.velocity.x, 0); //makes velocity 0 before jumping
            Rigid.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            jump = false;
        }

    }

    void Crouch()
    {
        if (crouch)
        {
            PlayerCollider.size = new Vector2 (PlayerCollider.size.x, crouchColliderSizeY);
            SpriteR.sprite = pusheen_crouch;
        }
        if (!crouch)
        {
            PlayerCollider.size = new Vector2(PlayerCollider.size.x, default_ColliderSizeY);
            SpriteR.sprite = pusheen;
        }
    }

    void FlipSprite()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    void LadderMovement()
    {
        if (onTopOfLadder && ladderMovement != 0)
        {
            climbingLadder = true;
        }
        if (climbingLadder && ladderMovement != 0)
        {
            alreadyDoubleJumped = false;

            Rigid.velocity = new Vector2(0, 0);
            Rigid.gravityScale = 0f;

            transform.position += new Vector3(0, ladderMovement * Time.deltaTime * onLadderSpeed, 0);
        }
        if (climbingLadder == false)
        {
            Rigid.gravityScale = defaultGravity;
        }
    }

    void DashSkill()
    {
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
            Rigid.velocity = transform.right * movement * dashStregth;
        }
        if ((isDashing && Time.time > timeWhenDashEnds) || movement == 0)
        {
            isDashing = false;
            Rigid.velocity = new Vector2(0, Rigid.velocity.y);
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
        if (touch.CompareTag("Platform"))
        {
            Debug.Log("Touching above while crouching TRUE");
            touchingCrouching = true;
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
        if (touch.CompareTag("Platform"))
        {
            touchingCrouching = false;
            if (crouch && Input.GetKey(KeyCode.LeftControl) == false)
            {
                crouch = false;
            }
        }
    }
}

//TODO: MOVEMENT SCRIPT - Comment Everything (dash)
//TODO: MOVEMENT SCRIPT - Close range attacks, make invisible collider and isTrigger to hit
//TODO: MOVEMENT SCRIPT - Organize things into either functions or separate scripts
//TODO: MOVEMENT SCRIPT - Add Crouch capability
//TODO: MOVEMENT SCRIPT - Add Wall jumps
//TODO: MOVEMENT SCRIPT - Fix Crouch bug (make 2 separate colliders, a bigger one that will actually trigger the touchingCrouching false)
