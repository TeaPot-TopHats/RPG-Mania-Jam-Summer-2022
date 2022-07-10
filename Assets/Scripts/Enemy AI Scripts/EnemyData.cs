using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public LayerMask GroundLayer;
    public LayerMask PlayerLayer;
    public Transform SoundChecker;
    public Transform WallChecker;
    public Transform GroundChecker;
    public Transform FeetChecker;
    public Transform VisionChecker;
    public Collider2D heardObject;
    public Collider2D seenObject;
    public Vector2 FeetSize;
    public Vector2 VisionDistance;
    public float genericRadius;//poorly named, but default radius size for circular gizmos detection
    public float soundRadius;
    public bool nearGround = true;
    public bool feetPlanted;
    public bool againstWall;
    public bool isFalling;
    public bool hearingRange;
    public bool hearingNoise;
    public bool seePlayer;
    public bool seeObstruction;

}
 