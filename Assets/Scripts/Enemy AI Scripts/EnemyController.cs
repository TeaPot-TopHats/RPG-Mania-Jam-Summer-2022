using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Sensor Script
    [SerializeField] EnemySensors EnemySensors;

    //Behavioral Script
    [SerializeField] EnemyBehaviors EnemyBehaviors;

    //Actions
    [SerializeField] event Action CurrentState;

    //Starting States
    [SerializeField] bool patroling;

    public GameObject Player;
    public EnemyData EnemyData;
    public NPCData NpcData;
    public BoxCollider2D NPCCollider;
    public SpriteRenderer SpriteR;




    private void Awake()
    {
        if (patroling)
            CurrentState = Patrol;
        //NPCCollider = GetComponent<BoxCollider2D>();
        //SpriteR = GetComponent<SpriteRenderer>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        CurrentState();
    }

    void Patrol()
    {
        Debug.Log("Start Patrol");
        if (EnemySensors.seePlayer)
        {
            Debug.Log("Change Patrol");
            CurrentState = Chase;
        }
        else
        {
            if ((!EnemySensors.nearGround || EnemySensors.againstWall) && EnemySensors.feetPlanted)
                EnemyBehaviors.FlipSprite();
            EnemyBehaviors.Walk();
            Debug.Log("End Patrol");
        }
        
    }

    void Chase()
    {
        Debug.Log("Starting");
        if (!EnemySensors.chaseRange)
        {
            Debug.Log("Can't Chase");
            CurrentState = Patrol;
        }
        else if (EnemySensors.canAttack)
            CurrentState = Attack;
        else
        {
            Debug.Log("Chasing");
            if (Player.transform.position.x < transform.position.x && !EnemyBehaviors.facingLeft)
                EnemyBehaviors.FlipSprite();
            else if (Player.transform.position.x > transform.position.x && EnemyBehaviors.facingLeft)
                EnemyBehaviors.FlipSprite();
            if (EnemyBehaviors.jump)
                if (System.Math.Abs(Player.transform.position.x - transform.position.x)
                < System.Math.Abs(Player.transform.position.y - transform.position.y))
                    EnemyBehaviors.Jump();
                else if (!EnemySensors.nearGround)
                    EnemyBehaviors.Jump();
            EnemyBehaviors.Run();
        }

    }

    void Attack()
    {
        Debug.Log("ATTACK");
        CurrentState = Chase;
    }
    public void EventTest()
    {
        Debug.Log("Event works!!!");
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