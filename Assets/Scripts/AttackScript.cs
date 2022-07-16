using System;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    [Header("Animations")]
    public Animator animator;

    [Header("Melee or Ranged")]
    [SerializeField] private bool melee = true;

    [Header("Melee")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.3f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Melee Cooldown")]
    [SerializeField] private float attackCooldown = 0.8f;
    [SerializeField] private float nextAttackTime;

    [Header("Melee Damage")]
    [SerializeField] private int meleeDamage = 3;

    [Header("Interact")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRange = 0.3f;
    [SerializeField] private LayerMask interactLayer;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextAttackTime && melee)
        {
            
            MeeleAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }

    void MeeleAttack()
    {
        Debug.Log("It attacc");
        //Triggers attack animation
        animator.SetTrigger("Attack");
        Debug.Log("And protecc");
        //Gets all the enemies that the circle interacts with
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().Attacked(meleeDamage);
            Debug.Log("Michael Jackson took " + meleeDamage + " Damage.");
            Debug.Log("Michael Jackson has " + enemy.GetComponent<EnemyData>().currentHealth + " health.");
        }
    }

    void Interact()
    {
        Collider2D interactObject = Physics2D.OverlapCircle(interactPoint.position, interactRange, interactLayer);
        interactObject.GetComponent<InteractableObject>().Interact();
        Debug.Log("Interacted");
    }

    //Displays the atack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(interactPoint.position, interactRange);
    }
}
//TODO: ATTACK SCRIPT - Add Ranged Attacks
