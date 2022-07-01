using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    public static float movement; //stores movement, left or right

    [Header("Ladder")]
    [SerializeField] private float onLadderSpeed = 5f;
    private float ladderMovement; //stores ladder movement, up or down
    private float defaultGravity;
    private bool onTopOfLadder; //saves if the player is on the ladder but not climbing
    public static bool climbingLadder; //stores if the players is actually climbing the ladder

    [Header("Jump")]
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float collisionSide; //stores which side of the player is getting collided, ONLY up or down NOT right or left. See OnCollisionEnter2D

    //Detect ground cast
    private float BoxCastLength = 0.01f;
    [SerializeField] private LayerMask platformLayerMask;

    [Header("Double Jump")]
    [SerializeField] private bool allowDoubleJump = true;
    private bool alreadyDoubleJumped;
    private bool jump; //stores if the player should jump

    [Header("Crouch")]
    [SerializeField] private float crouchMoveSpeed = 2f;
    private bool crouch; //stores if the player should crouch
    [SerializeField] private bool touchingCrouching; //stores if there's somethings above the character's head while crouching
    //Crouch Colliders
    private float default_ColliderSizeY;
    [SerializeField] private float crouchColliderSizeY = 0.55f;
    //Crouch Box Cast
    [SerializeField] private float BoxCastLength_Crouch = 0.05f;
    //Crouch Sprites
    [SerializeField] private Sprite pusheen; //Regular Sprite
    [SerializeField] private Sprite pusheen_crouch; //Crouched Sprite

    //Sprite
    private bool facingLeft = true; //original sprite faces left

    //Physics
    private Rigidbody2D Rigid;
    [SerializeField] private BoxCollider2D PlayerCollider;
    private SpriteRenderer SpriteR;

    //Awake gets called before Start, always
    private void Awake()
    {
        //Gets Rigidbody2D
        Rigid = GetComponent<Rigidbody2D>();
        defaultGravity = Rigid.gravityScale; //Grabs the default gravity from rigid body for ladder use

        //Get Collider
        PlayerCollider = GetComponent<BoxCollider2D>();
        default_ColliderSizeY = PlayerCollider.size.y; //Grabs the default player collider size from collider for crouch use

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
        if (IsGrounded())
        {
            collisionSide = 1;
            alreadyDoubleJumped = false;
        }
        else
        {
            collisionSide = 0;
        }
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
        if (collisionSide == 1)
        {
            CheckTouchingCrouching();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !climbingLadder)
        {
            crouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !touchingCrouching)
        {
            crouch = false;
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
            transform.Translate(new Vector2(movement * Time.deltaTime * moveSpeed, 0)); //moves the player in the x direction,
        }
        if (crouch)
        {
            transform.Translate(new Vector2(movement * Time.deltaTime * crouchMoveSpeed, 0)); //moves the player in the x direction,
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
            PlayerCollider.size = new Vector2(PlayerCollider.size.x, crouchColliderSizeY);
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

    bool IsGrounded()
    {
        RaycastHit2D boxcastHit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.down, PlayerCollider.bounds.extents.y + BoxCastLength, platformLayerMask);
        Color rayColor;
        if (boxcastHit.collider != null)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.yellow;
        }
        Debug.DrawRay(PlayerCollider.bounds.center + new Vector3(PlayerCollider.bounds.extents.x, 0), Vector2.down * (PlayerCollider.bounds.extents.y + BoxCastLength), rayColor);
        Debug.DrawRay(PlayerCollider.bounds.center - new Vector3(PlayerCollider.bounds.extents.x, 0), Vector2.down * (PlayerCollider.bounds.extents.y + BoxCastLength), rayColor);
        Debug.DrawRay(PlayerCollider.bounds.center - new Vector3(PlayerCollider.bounds.extents.x, PlayerCollider.bounds.extents.y), Vector2.right * (PlayerCollider.bounds.extents.x * 2), rayColor);
        return boxcastHit.collider;
    }

    void CheckTouchingCrouching()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size + new Vector3(0.3f, 0f), 0f, Vector2.up, PlayerCollider.bounds.extents.y + BoxCastLength_Crouch, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            //Debug.Log("touchingCrouching TRUE");
            rayColor = Color.red;
            touchingCrouching = true;
        }
        else
        {
            //Debug.Log("touchingCrouching FALSE");
            rayColor = Color.yellow;
            touchingCrouching = false;
            if (crouch && Input.GetKey(KeyCode.LeftControl) == false)
            {
                crouch = false;
            }
        }
        Debug.DrawRay(PlayerCollider.bounds.center - new Vector3(PlayerCollider.bounds.extents.x, 0) + new Vector3(-0.12f, 0.3f), Vector2.up * (PlayerCollider.bounds.extents.y + BoxCastLength_Crouch), rayColor);
        Debug.DrawRay(PlayerCollider.bounds.center + new Vector3(PlayerCollider.bounds.extents.x, 0) + new Vector3(0.12f, 0.3f), Vector2.up * (PlayerCollider.bounds.extents.y + BoxCastLength_Crouch), rayColor);
        Debug.DrawRay(PlayerCollider.bounds.center + new Vector3(PlayerCollider.bounds.extents.x, PlayerCollider.bounds.extents.y + BoxCastLength_Crouch) + new Vector3(0.12f, 0.3f), Vector2.left * (PlayerCollider.bounds.extents.x * 2 + 0.24f), rayColor);

    }

    //Detects if player is touching ladder
    private void OnTriggerEnter2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            onTopOfLadder = true;
            //Debug.Log("Player onTopOfLadder TRUE");
        }
    }
    private void OnTriggerExit2D(Collider2D touch)
    {
        if (touch.CompareTag("Ladder"))
        {
            climbingLadder = false;
            onTopOfLadder = false;
            //Debug.Log("Player onTopOfLadder FALSE");
        }
    }

}
//TODO: MOVEMENT SCRIPT - Fix Crouch bug (make 2 separate colliders, a bigger one that will actually trigger the touchingCrouching false)
//TODO: MOVEMENT SCRIPT - Fix BoxCast for IsGrounded
//TODO: MOVEMENT SCRIPT - Check out Gizmos, and debugging mode
