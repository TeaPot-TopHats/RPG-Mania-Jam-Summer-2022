using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public NPCData NpcData;
    public Rigidbody2D Rigid;
    public BoxCollider2D NPCCollider;
    public SpriteRenderer SpriteR;
    public bool jump;
    public float jumpStrength;
    public bool facingLeft = true;
    public float movementDirection;
    public float moveSpeed = 5f;


    //Gizmos properties
    public LayerMask ChosenLayer;
    public Transform SoundChecker;
    public Transform WallChecker;
    public Transform GroundChecker;
    public float genericRadius;//poorly named, but default radius size for circular gizmos detection
    public float soundRadius;
    public bool nearGround = true;
    public bool againstWall;
    public bool isFalling;
    public bool hearingNoise;

    private void Awake()
    {

        Debug.Log(nearGround);
        Debug.Log(isFalling);
        //Gets Rigidbody2D
        Rigid = GetComponent<Rigidbody2D>();

        //Get Collider
        NPCCollider = GetComponent<BoxCollider2D>();

        //Gets Sprite Renderer
        SpriteR = GetComponent<SpriteRenderer>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(EnemyMoveTowards());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector2.Distance(transform.position, Player.transform.position) > NpcData.detectRange)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,
                NpcData.speed * Time.deltaTime);

        }*/
        Debug.Log("regular");
        nearGround = Physics2D.OverlapCircle(GroundChecker.position, genericRadius, ChosenLayer);
        Debug.Log(nearGround.ToString());
        Debug.Log(nearGround);
        againstWall = Physics2D.OverlapCircle(WallChecker.position, genericRadius);
        hearingNoise = Physics2D.OverlapCircle(SoundChecker.position, soundRadius);
        CheckFall();
    }


    private void FixedUpdate()
    {
        Debug.Log("fixed");
        Move();
    }
    void CheckFall()
    {
        if (Rigid.velocity.y < -0.1)
            isFalling = true;
        else
            isFalling = false;
    }

    void Move()
    {
        if (nearGround == false && isFalling == false)
            FlipSprite();


        transform.position += new Vector3(movementDirection * Time.deltaTime * moveSpeed, 0);

    }

    void Listening()
    {
        if(hearingNoise == true)
            Debug.Log("working");
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

    public void EventTest()
    {
        Debug.Log("Event works!!!");
    }

    void FlipSprite()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        movementDirection *= -1;

        facingLeft = !facingLeft;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundChecker.position, genericRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(WallChecker.position, genericRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(SoundChecker.position, soundRadius);
    }
}
//patrolling state

    /*
    IEnumerator EnemyMoveTowards()
    {
        Debug.Log("Moving");
        while (Vector2.Distance(transform.position, player.transform.position) > npcData.detectRange)
        {
            Vector2 destination = Vector2.MoveTowards(transform.position, player.transform.position,
                npcData.speed * Time.deltaTime);
            transform.position = destination;
            yield return null;
        }
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        Debug.Log("Attacking");
        yield return new WaitForSeconds(3);
        StartCoroutine(EnemyMoveTowards());
    }
    */