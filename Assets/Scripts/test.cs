using UnityEngine;

public class test : MonoBehaviour
{

    public GameEvent testEvent;
    //Variables you can alter in Unity

    //Move
    public float moveSpeed = 5f;

    //Ladder
    public float onLadderSpeed = 5f;
    public float defaultGravity = 1.8f;

    //Jump
    public float jumpStrength = 10f;

    //Dash
    public float dashStrength = 8f;
    public bool allowDash = true;
    public bool dash = false;
    public bool justDashed = false;
    public float dashCooldown = 1f;
    public float nextDashTime;

    public bool allowDoubleJump = true;
    public bool alreadyDoubleJumped;
    public bool jump; //stores if the player is jumping
    public bool onLadder;

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
    }

    //Called before first frame
    void Start()
    {
    }

    // Update per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal"); //detects left and right for joystick and keyboard input, can be -1 or 1
        ladderMovement = Input.GetAxisRaw("Vertical"); //detects up and down for ladder
        
        //Detects jump key
        if (Input.GetButtonDown("Jump") && collisionSide == 1)
        {
            jump = true;
        }
        if (Input.GetButtonDown("Jump") && collisionSide == 0 && allowDoubleJump && alreadyDoubleJumped == false)
        {
            jump = true;
            alreadyDoubleJumped = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("h clicked");
            trigger();
        }

        if (Time.time > nextDashTime)
            justDashed = false;
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dash = true;
                nextDashTime = Time.time + dashCooldown;
            }
        }
    }
    //Unity Liked this better for physics and stuff
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
        if (onLadder)
        {
            Rigid2D.gravityScale = 0f;
            Rigid2D.velocity = new Vector2 (Rigid2D.velocity.x, 0);
            transform.position += new Vector3(0, ladderMovement * Time.deltaTime * onLadderSpeed, 0) ;
        }
        if (onLadder == false)
        {
            Rigid2D.gravityScale = defaultGravity;
        }
        if (dash && justDashed == false && onLadder == false)
        {
            Rigid2D.AddForce(new Vector2(movement * dashStrength, 0), ForceMode2D.Impulse);
            dash = false;
            justDashed = true;
        }
    }

    //Detection of Collision. collisionSide tells you if it's hitting the top (-1) or the bottom (1) of the player collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D hitPos in collision.contacts)
        {
            Debug.Log("Player is hitting side of y: " + hitPos.normal.y); 
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
            Debug.Log("Player not touching");
        }
    }

    //Detects if player is touching ladder
    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            onLadder = true;
            alreadyDoubleJumped = false;
            Debug.Log("Player onLadder");
            Rigid2D.velocity = new Vector2(0, Rigid2D.velocity.y);
        }
    }
    private void OnTriggerExit2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            onLadder = false;
            Debug.Log("Player NO longer on ladder");
        }
    }
    void trigger()
    {
        Debug.Log("Starting Event");
        testEvent.TriggerEvent();
    }

    public void testingEvent()
    {
        Debug.Log("Event works 2");
    }

}

//TODO: MOVEMENT SCRIPT - Player should dash very fast but only for a short distance
//TODO: MOVEMENT SCRIPT - Player should dash past the ladder unless they are pressing up or down to climb it
//TODO: MOVEMENT SCRIPT - Player should be able to dash off the ladder
