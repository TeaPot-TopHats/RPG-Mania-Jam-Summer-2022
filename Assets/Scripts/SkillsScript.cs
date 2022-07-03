using UnityEngine;

public class SkillsScript : MonoBehaviour
{
    [Header("Dash Skill")]
    [SerializeField] private bool allowDash = true;
    [SerializeField] private float dashTime = 0.2f; //how long to dash for
    [SerializeField] private float dashStregth = 8f; //how fast the dash is
    [SerializeField] private float dashCooldownTime = 2f; //the cool down time to use the skill
    //Functionality
    [SerializeField] private bool isDashing; //is the player currently dashing?
    private float timeWhenDashEnds;
    private bool dash; //stores if the player should dash, works for the fixed update function
    private bool alreadyDashed;
    //Cooldown
    private float timeWhenCooldownEnds; //stores current time + dash_cooldown time

    //Physics
    private Rigidbody2D Rigid;


    void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
    }


    void Update()
    {
        //Dash
        if (alreadyDashed && Time.time >= timeWhenCooldownEnds) //Checks if cooldown is over
        {
            alreadyDashed = false;
        }
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
        if (dash && !PlayerMovement.climbingLadder && !PlayerMovement.crouch) //you can dash if player is not climbing
        {
            //Debug.Log("Dash");
            timeWhenCooldownEnds = Time.time + dashCooldownTime; //sets the time when the cooldown ends

            isDashing = true;
            timeWhenDashEnds = Time.time + dashTime;
            alreadyDashed = true;
            dash = false;
        }
        if (isDashing && Time.time < timeWhenDashEnds && PlayerMovement.movement != 0)
        {
            Rigid.velocity = transform.right * PlayerMovement.movement * dashStregth; //move the player using velocity
        }
        else if ((isDashing && Time.time > timeWhenDashEnds) || PlayerMovement.movement == 0)
        {
            isDashing = false;
            Rigid.velocity = new Vector2(0, Rigid.velocity.y);
        }
    }
}
//TODO: SKILLS SCRIPT - Add Wall jumps
