using UnityEditor;
using UnityEngine;
[CreateAssetMenu (menuName = "Enemy", fileName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("rawData")]
    public string enemyName;
    public int maxHealth;
    public int level;
    public int attackDMG;
    public int armor;
    public int sightRange;
    public float movementSpeed;
    public float attackSpeed;
    public float wanderRadius;

    [Header("SetAttack1")]
   
    [Tooltip("CD Starts At Lunch")]
    public float Attack1_Cd;
    public float Attack1_Range;
    public float Attack1_RangeFromSource;
    public float Attack1_AnimDelay;
    public float attack1_animLenght;
    [Header("SetAttack2")]
    
    [Tooltip("CD Starts At Lunch")]
    public float Attack2_Cd;
    public float Attack2_Range;
    public float Attack2_RangeFromSource;
    public float Attack2_AnimDelay;
    public float attack2_animLenght;

    [Header("Components")]
    public int[] lootDropsID;
    public Animation[] enemyAnim;

    [Header("Categories")]
   
    public AttackType attackType;
    public Vulnerability enemyVulnerability;
    public Habitat[] habitats;
    public EnemyState enemyState;
    public Size enemySize;
    public Difficulty enemyDifficulty;









}
