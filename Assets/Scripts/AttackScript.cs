using UnityEngine;

public class AttackScript : MonoBehaviour
{
    //Attack animation
    public Animator animator;

    //Attack stuff
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.3f;
    public LayerMask enemyLayer;

    private bool alreadyAttacked = false;
    

    private void Start()
    {
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && !alreadyAttacked)
        {
            alreadyAttacked = true;
            Attack();
        }
    }

    void Attack()
    {
        //Attack animation
        animator.SetTrigger("Attack");

        //Gets all the enemies that the circle interacts with
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Michael Jackson Slain");
        }
        alreadyAttacked = false;
    }

    //Displays the atack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
//TODO: ATTACK SCRIPT - Close range attacks, make invisible collider and isTrigger to hit
