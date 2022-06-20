using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables you can alter in Unity
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public bool doubleJump = true;

    //stores which side of the player is getting collided
    public float collisionSide;
    //records if the player has double jumped
    public bool alreadyDoubleJumped = false;

    //Physics
    private Rigidbody2D Rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        //Gets Rigidbody2D
        Rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Input.GetAxisRaw("Horizontal"); //for joystick and keyboard input, can be -1 or 1
        //Moves the player based on movement
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * moveSpeed; //moves the player in the x direction, 

        //jump
        if (Input.GetKeyDown("space") && collisionSide == 1)
        {
            Rigid2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButtonDown("Jump") && collisionSide == 0 && doubleJump && alreadyDoubleJumped == false)
        {
            Rigid2D.velocity = new Vector2(Rigid2D.velocity.x, 0); //makes velocity 0 before jumping
            Rigid2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            alreadyDoubleJumped = true;
        }
    }

    //Detection of Collision. collisionSide tells you if it's hitting the top (-1) or the bottom (1) of the collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach(ContactPoint2D hitPos in other.contacts)
        {
            Debug.Log(hitPos.normal.y); 
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
            Debug.Log("Not touching");
        }
    }
}