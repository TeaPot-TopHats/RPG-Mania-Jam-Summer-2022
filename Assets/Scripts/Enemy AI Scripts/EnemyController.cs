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

    //Data Script
    [SerializeField] EnemyData EnemyData;

    //Actions
    [SerializeField] event Action CurrentState;

    //Starting States
    [SerializeField] bool patroling;

    private void Awake()
    {
        if (patroling)
            CurrentState = Patrol;
 
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
            CurrentState = ChasePause;
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
            if (EnemyData.chaseTime > 0)
            {
                if (EnemySensors.feetPlanted)
                    if (System.Math.Abs(EnemyData.Player.transform.position.x - transform.position.x)
                    < System.Math.Abs(EnemyData.Player.transform.position.y - transform.position.y))
                        EnemyBehaviors.Jump();
                    else if (!EnemySensors.nearGround)
                        EnemyBehaviors.Jump();
                EnemyBehaviors.Run();
                EnemyData.chaseTime -= Time.deltaTime;
            }
            else
                CurrentState = Patrol;
        }
        else if (EnemySensors.canAttack)
            CurrentState = Attack;
        else
        {
            EnemyData.chaseTime = EnemyData.EnemyDataInitializer.initialChaseTime;
            Debug.Log("Chasing");
            if (EnemyData.Player.transform.position.x < transform.position.x-1 && !EnemyBehaviors.facingLeft)
                EnemyBehaviors.FlipSprite();
            else if (EnemyData.Player.transform.position.x > transform.position.x+1 && EnemyBehaviors.facingLeft)
                EnemyBehaviors.FlipSprite();
            if (EnemySensors.feetPlanted)
                if (System.Math.Abs(EnemyData.Player.transform.position.x - transform.position.x)
                < System.Math.Abs(EnemyData.Player.transform.position.y - transform.position.y))
                    EnemyBehaviors.Jump();
                else if (!EnemySensors.nearGround)
                    EnemyBehaviors.Jump();
            EnemyBehaviors.Run();
        }

    }

    void Attack()
    {
        if(EnemyData.startAttackTime > 0)
        {
            EnemyData.startAttackTime -= Time.deltaTime;
        }
        else
        {
            if (EnemyData.attackingTime == EnemyData.EnemyDataInitializer.initialAttackingTime)
            {
                //Player.Attacked(EnemyData.EnemyDataInitializer.damage);
                EnemyData.attackingTime -= Time.deltaTime;
            }
            else if (EnemyData.attackingTime > 0)
                EnemyData.attackingTime -= Time.deltaTime;
            else 
            {
                EnemyData.startAttackTime = EnemyData.EnemyDataInitializer.initialStartAttackTime;
                EnemyData.attackingTime = EnemyData.EnemyDataInitializer.initialAttackingTime;
                CurrentState = Chase;
            }
        }
    }
    public void EventTest()
    {
        Debug.Log("Event works!!!");
    }

    void ChasePause()
    {
        EnemyBehaviors.Stop();
        if (EnemyData.chasePauseTime > 0)
            EnemyData.chasePauseTime -= Time.deltaTime;
        else
        {
            EnemyData.chasePauseTime = EnemyData.EnemyDataInitializer.initialChasePauseTime;
            CurrentState = Chase;
        }
            
    }
    void Hurt()
    {
        if (EnemyData.hurtDelayTime > 0)
            EnemyData.hurtDelayTime -= Time.deltaTime;
        else
        {
            EnemyData.hurtDelayTime = EnemyData.EnemyDataInitializer.initialHurtDelayTime;
            CurrentState = Chase;
        }
    }

    void Dying()
    {
        if(EnemyData.deathTime == EnemyData.EnemyDataInitializer.initialDeathTime)
        {
            EnemyData.vulnerable = false;
            EnemyData.deathTime -= Time.deltaTime;
        }
        else if (EnemyData.deathTime > 0)
            EnemyData.deathTime -= Time.deltaTime;
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioManager>().Play("mj");
            Destroy(gameObject);
        }
    }

    public void Attacked(int receivingDamage)
    {
        if (EnemyData.vulnerable)
        {
            EnemyData.ResetTimers();
            EnemyData.currentHealth -= receivingDamage;
            if (EnemyData.IsDead())
                CurrentState = Dying;
            else
                CurrentState = Hurt;
        }

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