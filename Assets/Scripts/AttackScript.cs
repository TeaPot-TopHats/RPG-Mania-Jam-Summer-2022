using UnityEngine;

public class AttackScript : MonoBehaviour
{

    [Header("Animations")]
    public Animator animator;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.3f;
    public LayerMask enemyLayer;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 0.8f;
    [SerializeField] private float nextAttackTime;

    [Header("Damage")]
    [SerializeField] private int damage = 3;

    private void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextAttackTime)
        {
            MeeleAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void MeeleAttack()
    {
        //Triggers attack animation
        animator.SetTrigger("Attack");

        //Gets all the enemies that the circle interacts with
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyData>().TakeDamage(damage);
            Debug.Log("Michael Jackson took " + damage + " damage.");
            Debug.Log("Michael Jackson has " + enemy.GetComponent<EnemyData>().currentHealth + " health.");
        }
    }

    //Displays the atack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
//TODO: ATTACK SCRIPT - Add Ranged Attacks
