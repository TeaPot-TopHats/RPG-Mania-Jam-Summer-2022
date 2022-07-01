using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public string fightingType;
    public string description;
    public GameObject sprite;
    public int health;
    public float speed;
    public float detectRange;
    public float attackSpeed;
    public float damageOutput;
    public bool aggressive;

}
 