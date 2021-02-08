using UnityEngine;
public enum monsterParts {Tail,Leg,Groin,Chest, back, head }
public class EnemyPart : MonoBehaviour
{
    [SerializeField] PartSo _PartSo; 
    private Enemy _Enemy;
    private GameObject EnemyGO;
    int brakeMultiplier=2;



    public bool creatOnAwake;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject particleSystemGO;

    private Vulnerability vulnerability;
    [SerializeField] monsterParts thisPart;
    public int armor;
    int BreakPoints,VuDamageMultiplayer;
    private Collider collider;
    public bool isBroken;


    void Start()
    {

        collider = GetComponent<Collider>();
        GetMonster();
        GetPartSettings();
        if (creatOnAwake)
        {
            Instantiate(particleSystem, transform.position, Quaternion.identity);
        }
    }
    public void GetDamage(int hitDamage,Vector3 hitPoint,Vulnerability weaponVulnerabilityEffect)
    {
        if (vulnerability == weaponVulnerabilityEffect)
        {
            hitDamage *= VuDamageMultiplayer;
        }
        if (BreakPoints > 0)
        {
            BreakPoints -= hitDamage;
            if (BreakPoints <= 0)
            {
                BreakPart();
            }
        }
        else
        {
            hitDamage *= brakeMultiplier;
        }


        _Enemy.GetDamageFromPart(hitDamage, thisPart, hitPoint);

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



