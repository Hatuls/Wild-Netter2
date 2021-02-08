using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttackEffectType { None, Shock, Fire, Cold, Poison }
public enum Vulnerability { None, Fire, Poison, Cold }
public enum Habitat { Forest, Mountain, GrassLands, Deserts };
public enum EnemyState { None, Idle, Chase ,Attack,lured,Slow};
public enum Debuff { Slow};

public enum Size { Small,Medium,Large }
public enum Difficulty {Easy,Hard,Challange }

[System.Serializable]
public class EnemySheet
{
    
    public Dictionary<monsterParts, GameObject> EnemyParts = new Dictionary<monsterParts, GameObject>();
    public Dictionary<monsterParts, Collider> Colliders = new Dictionary<monsterParts, Collider>();


    

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
   
    [Header("AnimationTimers")]

    public float getUpAnimTime;


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


    [Header("AggroView")]
    public float lookRange;
    public float rangeFromBody;
    public float stayInRangeTime;
    


    [Header("Components")]
    public int[] lootDropsID;
    public Animation[] enemyAnim;
    public Material DamagedMat;
    public ParticleSystem Trails;

    [Header("Categories")]
    public AttackType attackType;
    public Vulnerability enemyVulnerability;
    public Habitat[] habitats;
    public EnemyState enemyState;
    public Size enemySize;
    public Difficulty enemyDifficulty;




}


