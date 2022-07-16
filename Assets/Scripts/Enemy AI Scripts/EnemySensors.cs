using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensors : MonoBehaviour
{
    //Gizmos Sensors
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Transform SoundChecker;
    [SerializeField] Transform WallChecker;
    [SerializeField] Transform GroundChecker;
    [SerializeField] Transform FeetChecker;
    [SerializeField] Transform VisionChecker;
    [SerializeField] Transform AttackChecker;
    [SerializeField] Transform ChaseChecker;
    [SerializeField] Collider2D HeardObject;
    [SerializeField] Collider2D SeenObject;
    [SerializeField] Vector2 FeetSize;
    [SerializeField] Vector2 VisionDistance;
    [SerializeField] float AttackZone;
    [SerializeField] float genericRadius;//poorly named, but default radius size for circular gizmos detection
    [SerializeField] float soundRadius;
    [SerializeField] float chaseRadius;
    [SerializeField] public bool nearGround;
    [SerializeField] public bool feetPlanted;
    [SerializeField] public bool againstWall;
    [SerializeField] public bool hearingRange;
    [SerializeField] public bool hearingNoise;
    [SerializeField] public bool seePlayer;
    [SerializeField] public bool seeObstruction;
    [SerializeField] public bool canAttack;
    [SerializeField] public bool chaseRange;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(canAttack);
        feetPlanted = Physics2D.OverlapBox(FeetChecker.position, FeetSize, 0, GroundLayer);
        nearGround = Physics2D.OverlapCircle(GroundChecker.position, genericRadius, GroundLayer);
        againstWall = Physics2D.OverlapCircle(WallChecker.position, genericRadius, GroundLayer);
        hearingRange = Physics2D.OverlapCircle(SoundChecker.position, soundRadius, PlayerLayer);
        PlayerCheck();
        chaseRange = Physics2D.OverlapCircle(ChaseChecker.position, chaseRadius, PlayerLayer);
        //chaseRange = Physics2D.OverlapBox(AttackChecker.position, AttackZone, PlayerLayer);
        canAttack = Physics2D.OverlapCircle(AttackChecker.position, AttackZone, PlayerLayer);
        //Debug.Log(Physics2D.OverlapCircle(AttackChecker.position, AttackZone, PlayerLayer).ClosestPoint(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        feetPlanted = Physics2D.OverlapBox(FeetChecker.position, FeetSize, 0, GroundLayer);
        nearGround = Physics2D.OverlapCircle(GroundChecker.position, genericRadius, GroundLayer);
        againstWall = Physics2D.OverlapCircle(WallChecker.position, genericRadius, GroundLayer);
        hearingRange = Physics2D.OverlapCircle(SoundChecker.position, soundRadius, PlayerLayer);
        PlayerCheck();
        chaseRange = Physics2D.OverlapCircle(ChaseChecker.position, chaseRadius, PlayerLayer);


        GetComponent<Animator>().SetFloat("XVel", System.Math.Abs(GetComponent<Rigidbody2D>().velocity.x));
        GetComponent<Animator>().SetFloat("YVel", GetComponent<Rigidbody2D>().velocity.y);
    }



    //Gizmos sensors
    private void Listening()
    {
        if (hearingRange)
        {

            HeardObject = Physics2D.OverlapCircle(SoundChecker.position, soundRadius, PlayerLayer);
            if (HeardObject.GetComponent<Rigidbody2D>().velocity.x != 0)
                hearingNoise = true;
            else
                hearingNoise = false;
        }

    }

    private void Looking()
    {
        if (seePlayer)
        {
            if (Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer))
                SeenObject = Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer);

        }
    }

    private void PlayerCheck()
    {
        seePlayer = Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer);
        if (seePlayer)
        {
            Debug.Log("Step 1");
            if (Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer))
            {
                Debug.Log("Step 2");
                Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer).ClosestPoint(transform.position).x));
                Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer).ClosestPoint(transform.position).x));
                Debug.Log(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer).ClosestPoint(transform.position).x);
                Debug.Log(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer).ClosestPoint(transform.position).x);
                Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer).ClosestPoint(transform.position).x - transform.position.x));
                Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer).ClosestPoint(transform.position).x - transform.position.x));

                if ((System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer).ClosestPoint(transform.position).x - transform.position.x) >
                    System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer).ClosestPoint(transform.position).x - transform.position.x)))
                {
                    Debug.Log("testing vision:");
                    Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, GroundLayer).ClosestPoint(transform.position).x));
                    Debug.Log(System.Math.Abs(Physics2D.OverlapBox(VisionChecker.position, VisionDistance, 0, PlayerLayer).ClosestPoint(transform.position).x));
                    Debug.Log(transform.position.x);
                    seeObstruction = false;
                    canAttack = Physics2D.OverlapCircle(AttackChecker.position, AttackZone, PlayerLayer);
                }
                else
                    seeObstruction = true; Debug.Log("Step 4");
            }
            else
            {
                Debug.Log("Step 3");
                seeObstruction = false;
                canAttack = Physics2D.OverlapCircle(AttackChecker.position, AttackZone, PlayerLayer);
                Debug.Log("Can he attack?: " + Physics2D.OverlapCircle(AttackChecker.position, AttackZone, PlayerLayer));
            }
        }
        
            
        //there's an issue here. If I take the closest point of collider, wouldnt that be the ground? 
    }

    /*private void PlayerCheck()
    {
        canAttack = Physics2D.OverlapBox(AttackChecker.position, AttackZone, PlayerLayer);
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundChecker.position, genericRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(WallChecker.position, genericRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(SoundChecker.position, soundRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(FeetChecker.position, FeetSize);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(VisionChecker.position, VisionDistance);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(AttackChecker.position, AttackZone);
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(ChaseChecker.position, chaseRadius);
    }
}
