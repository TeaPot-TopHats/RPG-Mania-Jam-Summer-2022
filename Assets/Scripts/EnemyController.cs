using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public NPCData npcData;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(EnemyMoveTowards());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > npcData.detectRange)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                npcData.speed * Time.deltaTime);

        }
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