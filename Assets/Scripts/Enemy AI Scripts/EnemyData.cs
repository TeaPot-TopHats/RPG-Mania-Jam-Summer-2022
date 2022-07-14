using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public GameObject Player;
    public EnemyDataInitializer EnemyDataInitializer;

    [SerializeField] public int currentHealth;
    public bool vulnerable;

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
        vulnerable = true;
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
        vulnerable = false;
        return true;
    }

}
