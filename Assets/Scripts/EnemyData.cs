using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private int maxHealth = 9;
    [SerializeField] public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject.Find("Main Camera").GetComponent<AudioManager>().Play("mj");
        Destroy(gameObject);

    }
}
