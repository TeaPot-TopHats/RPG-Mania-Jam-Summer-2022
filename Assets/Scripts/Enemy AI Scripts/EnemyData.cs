using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public GameObject Player;
    public EnemyDataInitializer EnemyDataInitializer;

    [SerializeField] public int currentHealth { get; set; }
    public bool vulnerable { get; set; }

    public float chasePauseTime;
    public float hurtDelayTime;
    public float deathTime;
    public float chaseTime;
    public float startAttackTime;
    public float attackingTime;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        currentHealth = EnemyDataInitializer.maxHealth;
        ResetTimers();
    }

    public void ResetTimers()
    {
        chasePauseTime = EnemyDataInitializer.initialChasePauseTime;
        hurtDelayTime = EnemyDataInitializer.initialHurtDelayTime;
        deathTime = EnemyDataInitializer.initialDeathTime;
        chaseTime = EnemyDataInitializer.initialChaseTime;
        startAttackTime = EnemyDataInitializer.initialStartAttackTime;
        attackingTime = EnemyDataInitializer.initialAttackingTime;
    }
    public bool IsDead()
    {
        if (currentHealth > 0)
            return false;
        return true;
    }

}
