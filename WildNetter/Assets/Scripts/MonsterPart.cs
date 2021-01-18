using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum monsterParts {Tail,Leg,Groin,Chest, back, head }
public class MonsterPart : MonoBehaviour
{
    [SerializeField] PartSo _PartSo; 
    private Enemy _Enemy;
    private GameObject EnemyGO;
    int brakeMultiplier;


    

    private Vulnerability vulnerability;
    [SerializeField] monsterParts thisPart;
    int BreakPoints,armor,VuDamageMultiplayer;
    private Collider collider;
    public bool isBroken;


    void Start()
    {

        collider = GetComponent<Collider>();
        GetMonster();
        GetPartSettings();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "WeaponCollider")
        {
            int HitDamage = other.GetComponentInParent<PlayerCombat>().GetSetAttackDMG;
            Vector3 hitPoint = other.transform.root.position;
            Vulnerability vulnerabilityActivator = other.GetComponentInParent<PlayerCombat>().GetSetWeaponSO.vulnerabilityActivator;
            if (vulnerability == vulnerabilityActivator)
            {
                HitDamage *= VuDamageMultiplayer;
            }
            if (BreakPoints > 0)
            {
            BreakPoints -= HitDamage;
                if (BreakPoints <= 0)
                {
                    BreakPart();
                }
            }
            else
            {
                HitDamage *= brakeMultiplier;
            }
            

             _Enemy.GetDamageFromPart(HitDamage,thisPart,hitPoint);
            
            Debug.Log(HitDamage);

        }
    }
    void BreakPart()
    {
        isBroken = true;
        _Enemy.PartBroke(thisPart);
    }
    public void GetMonster()
    {
        _Enemy = GetComponentInParent<Enemy>();
        EnemyGO = transform.root.gameObject;
        _Enemy.AddPart(thisPart, gameObject,collider);
    }
    void GetPartSettings()
    {
        BreakPoints = _PartSo.breakPoints;
        armor = _PartSo.armor;
        vulnerability = _Enemy._enemySheet.enemyVulnerability;
    }

}

