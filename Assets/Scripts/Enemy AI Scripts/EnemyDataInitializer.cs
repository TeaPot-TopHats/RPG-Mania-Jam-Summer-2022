using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataInitializer", menuName = "Scriptable Objects/EnemyDataInitializer")]
public class EnemyDataInitializer : ScriptableObject
{
    public int maxHealth;
    public int damage;
    public float initialChasePauseTime;
    public float initialHurtDelayTime;
    public float initialDeathTime;
    public float initialChaseTime;
    public float initialStartAttackTime;
    public float initialAttackingTime;
}
