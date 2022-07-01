using UnityEngine;

public class SkillsScript : MonoBehaviour
{
    //Dash Skill

    //Variables to configure the dash
    [SerializeField] private bool allowDash = true; //allows the skill of double jumping
    [SerializeField] private float dashTime = 0.2f; //how long to dash for
    [SerializeField] private float dashStregth = 8f; //how fast the dash is
    [SerializeField] private float dashCooldownTime = 2f; //the cool down time to use the skill
    //Variables for functionallity
    [SerializeField] private bool isDashing; //is the player currently dashing?
    private float timeWhenDashEnds; //stores current time + dashTime, telling the ifs when the dash ends
    private bool dash; //stores if the player should dash, works for the fixed update function
    private bool alreadyDashed; //stores if the player already dashed
    //Cooldown
    private float timeWhenCooldownEnds; //stores current time + dash_cooldown time

    //Physics
    private Rigidbody2D Rigid;


    //Awake gets called before Start, always
    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        //Dash
        if (alreadyDashed && Time.time >= timeWhenCooldownEnds)
        {
            alreadyDashed = false;
        }
        //Left Shift and you are pressing left or right
        if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerMovement.movement != 0 && !alreadyDashed && allowDash)
        {
            dash = true;
        }
    }


    private void FixedUpdate()
    {
        //Dash
        DashSkill();
    }


    void DashSkill()
    {
        if (dash && !PlayerMovement.climbingLadder) //you can dash if player is not climbing
        {
            //Debug.Log("Dash");
            timeWhenCooldownEnds = Time.time + dashCooldownTime; //sets the time when the cooldown ends

            isDashing = true;
            timeWhenDashEnds = Time.time + dashTime;
            alreadyDashed = true;
            dash = false;
        }
        //if player isDashing, the current time is less than the time when the dash ends, and the player is moving left or right
        if (isDashing && Time.time < timeWhenDashEnds && PlayerMovement.movement != 0)
        {
            Rigid.velocity = transform.right * PlayerMovement.movement * dashStregth; //move the player using velocity
        }
        //if the player is dashing and the time ends or the player stopped moving left or right the set velocity to 0
        else if ((isDashing && Time.time > timeWhenDashEnds) || PlayerMovement.movement == 0)
        {
            isDashing = false;
            Rigid.velocity = new Vector2(0, Rigid.velocity.y);
        }
    }
}
//TODO: SKILLS SCRIPT - Add Wall jumps
