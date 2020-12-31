
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    // Script References:
    public Enemy _instance;
    [SerializeField] EnemySO _enemySO;
    public EnemySheet _enemySheet;

    // Component References:
    public MeshRenderer _enemyMesh;
    public Collider _enemyHitTriggerCollider;
    public NavMeshAgent agent;
    public Animator _animator;
    public GameObject TargetAquierd;
    public Rigidbody rb;
    [SerializeField] bool showGizmos;

    [SerializeField] int currentHP;

    private int enemyID = 0;
    private int dropLevel;
    private int dropAmont;
    bool isTriggered = false;
    bool isHeading = false;
   internal bool attack1_inCd, attack2_inCd;
    Vector3 CurrentPos;
    Vector3 nextPos;
    
    public LayerMask TargetLayer;

    public enum EnemyAnimation { };
    EnemyAnimation enemyAnimation;
    

    

    // Getter & Setters:
    public int GetSetEnemyCurrentHP {
        get { return currentHP; }
        set {
            currentHP = value;
            if (currentHP <= 0)
            {
                Debug.Log("Enemy Died");
                EnemyKilled();
            }
        }
    }
    public EnemySO GetEnemySO {
        get { return _enemySO; }
    }

    // Collections:

    // Functions:
    
    public void Start()
    {
        Init();

        
    }
    public void Init()
    {
        _instance = this;
       
        _enemySheet = new EnemySheet();
        
        GetInfoFromEnemySO();
        

     
    }
   
    public void GetInfoFromEnemySO()
    {
        //EnemyBuildingStats
        _enemySheet.enemyAnim = GetEnemySO.enemyAnim;
        _enemySheet.enemyName = GetEnemySO.enemyName;
        _enemySheet.enemyDifficulty = GetEnemySO.enemyDifficulty;
        _enemySheet.enemySize = GetEnemySO.enemySize;
        _enemySheet.enemyVulnerability = GetEnemySO.enemyVulnerability;
        _enemySheet.habitats = GetEnemySO.habitats;
        _enemySheet.level = GetEnemySO.level;
        _enemySheet.lootDropsID = GetEnemySO.lootDropsID;
        _enemySheet.enemyState = GetEnemySO.enemyState;
        

        //SetValues//
        int StatsMultiplayer=1;
        int SizeMultiplayer=1;
        switch (_enemySheet.enemyDifficulty)
        {
            case Difficulty.Easy :
                StatsMultiplayer = 1;
                dropLevel = 1;
                break;

            case Difficulty.Hard:
                StatsMultiplayer = 2;
                dropLevel = 2;
                break;

            case Difficulty.Challange:
                StatsMultiplayer = 3;
                dropLevel = 3;
                break;
        }
        switch (_enemySheet.enemySize)
        {
            case Size.Small:
                SizeMultiplayer = 1;
                dropAmont = 1;
                break;

            case Size.Medium:
                SizeMultiplayer = 2;
                dropAmont = 2;
                break;

            case Size.Large:
                SizeMultiplayer = 3;
                dropAmont = 3;
                break;
        }

        //RawStats
        GetSetEnemyCurrentHP = GetEnemySO.maxHealth * StatsMultiplayer;
        _enemySheet.attackDMG = GetEnemySO.attackDMG * StatsMultiplayer;
        _enemySheet.attackSpeed = GetEnemySO.attackSpeed * StatsMultiplayer;
        _enemySheet.maxHealth = GetEnemySO.maxHealth * StatsMultiplayer;
        _enemySheet.movementSpeed = GetEnemySO.movementSpeed * StatsMultiplayer;
        _enemySheet.armor = GetEnemySO.armor * StatsMultiplayer;

  
       
       
        _enemySheet.wanderRadius = GetEnemySO.wanderRadius * SizeMultiplayer;
        _enemySheet.sightRange = GetEnemySO.sightRange * SizeMultiplayer;
        //attack1
        _enemySheet.Attack1_Cd = GetEnemySO.Attack1_Cd*SizeMultiplayer;
        _enemySheet.Attack1_Range = GetEnemySO.Attack1_Range * SizeMultiplayer;
        _enemySheet.Attack1_RangeFromSource = GetEnemySO.Attack1_RangeFromSource * SizeMultiplayer;
        _enemySheet.Attack1_AnimDelay = GetEnemySO.Attack1_AnimDelay * SizeMultiplayer;
        _enemySheet.attack1_animLenght = GetEnemySO.attack1_animLenght * SizeMultiplayer;
        //attack2
        _enemySheet.Attack2_AnimDelay = GetEnemySO.Attack2_AnimDelay * SizeMultiplayer;
        _enemySheet.Attack2_Cd = GetEnemySO.Attack2_Cd * SizeMultiplayer;
        _enemySheet.Attack2_Range = GetEnemySO.Attack2_Range * SizeMultiplayer;
        _enemySheet.Attack2_RangeFromSource = GetEnemySO.Attack2_RangeFromSource * SizeMultiplayer;
        _enemySheet.attack2_animLenght = GetEnemySO.attack2_animLenght * SizeMultiplayer;

        //setting Componnents Values
        agent.speed = _enemySheet.movementSpeed;
}
    private void Update()
    {
     
    }
    private void FixedUpdate()
    {
        //determains Enemy Current State
        BehaveByState(_enemySheet.enemyState);
        //Determains Current Target
        TargetFinder();
        
    }
    void BehaveByState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle :
                StartCoroutine(Iwander());
                break; 

            case EnemyState.Chase :
                Move(TargetAquierd.transform.position);
                AttacksAI();
                break;

            case EnemyState.Attack:
                stopMoving();
                break;

            case EnemyState.lured:
                
                break;

            case EnemyState.None:

                break;
        }
    }
  
    void TargetFinder()
    {
        Collider[] Found = Physics.OverlapSphere(CurrentPos, _enemySheet.sightRange, TargetLayer);
        foreach (Collider found in Found)
        {
            if (found != null&& _enemySheet.enemyState != EnemyState.lured)
            {
            _enemySheet.enemyState = EnemyState.Chase;
            TargetAquierd = found.gameObject;

            }
            
          //  agent.SetDestination(found.transform.position);
        }
        if (Found == null)
        {
            Debug.Log("No Player Found");
        }
    }



    public void Move(Vector3 Target)
    {
        if (agent.isStopped)
        {
        agent.Resume();
        }
        agent.destination = Target;
    }
    //Currently for totems
    public void stopMoving()
    {
        agent.Stop();
    }

    //Decide wich Attack to Cast when
    public void AttacksAI()
    {
        if (Vector3.Distance(transform.position, TargetAquierd.transform.position) < _enemySheet.Attack1_RangeFromSource + (_enemySheet.Attack1_Range / 2)&&!attack1_inCd)
        {
            Debug.Log("tryhitting1");
            StartCoroutine(Attack1());
        }
        if (Vector3.Distance(transform.position, TargetAquierd.transform.position) < _enemySheet.Attack2_RangeFromSource + (_enemySheet.Attack2_Range / 2) && !attack2_inCd)
        {
            Debug.Log("tryhitting2");
            StartCoroutine(Attack2());
        }
    }

    

    private void OnRecieveDmg(int dmgToApply) {

        Debug.Log("GotDMG");
        GetSetEnemyCurrentHP -=  dmgToApply;
    }
    public virtual void EnemyKilled() 
    { 

        DropLoot(); 
        Destroy(gameObject);
    }

    public void DropLoot()
    {

        if(TargetAquierd==null)
        {
           
                TargetFinder();
                
        }
        var Dropable = PickUpObject.SpawnItemInWorld(ItemFactory.GetInstance().GenerateItem(_enemySO.lootDropsID[dropLevel-1]), RetrieveDeathLocation(), TargetAquierd.transform);

        Dropable.GetComponent<PickUpObject>().GetItem().amount = dropAmont;




    }

    public bool CheckIfPlayerIsClose() { return true; }
    //public Transform GetTarget() { } <- clear comment when getting new target to move to
    public void TotemEffect() { }

    public void PlayAnimation() { }
    
    public Vector3 RetrieveDeathLocation() { return transform.position; }

    //public void IsPlayerInRange()
    //{

    //    if (Vector3.Distance(TargetPos.position, transform.position) >= _enemySheet.sightRange)
    //    {
    //        _enemySheet.enemyState = EnemyState.Idle;
    //       // if (transform.GetChild(0).gameObject.activeSelf)
    //        //    ToggleTriggerGameObject(false);
    //        return;
    //    }
    //    else
    //    {

    //      //  ..ToggleTriggerGameObject(true);
    //        _enemySheet.enemyState = EnemyState.Fight;
    //    }

    //}

    public void ToggleTriggerGameObject(bool isInRange)
    {
        _enemyHitTriggerCollider.enabled = isInRange;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isInRange);

        }
    }




    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WeaponCollider")
        {
           
            if (!isTriggered)
            {
                isTriggered = true;
             _enemyHitTriggerCollider.enabled = false;
            StartCoroutine(ReceiveDmgCoolDown());
                
            }
          
        }
    }
    public  IEnumerator ReceiveDmgCoolDown()
    {
        float cooldown = 3f;
        
        OnRecieveDmg(PlayerManager.GetInstance().GetPlayerCombat.GetSetAttackDMG);
        yield return new WaitForSeconds(cooldown);
        _enemyHitTriggerCollider.enabled = true;
      isTriggered = false;
    }
    public IEnumerator Iwander()
    {
        
        
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.velocity.magnitude < 0.15f && _enemySheet.enemyState == EnemyState.Idle&&!isHeading)
        {
            isHeading = true;
            CurrentPos = transform.position;
            nextPos = CurrentPos + (Random.insideUnitSphere *_enemySheet.wanderRadius);

            agent.SetDestination(nextPos);
            yield return new WaitForSeconds(5);
            isHeading = false;
        }
        yield return null;
       
    }
    public abstract IEnumerator Attack1();
    public abstract IEnumerator Attack2();
    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {

        //sightGizmos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemySheet.sightRange);
        //sightGizmos
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _enemySheet.wanderRadius);
        
        //attack1Gizmos
        Gizmos.color = Color.green;
       Gizmos.DrawWireSphere(transform.position+transform.forward* _enemySheet.Attack1_RangeFromSource, _enemySheet.Attack1_Range);
        //attack2Gizmos
        Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position+transform.forward* _enemySheet.Attack2_RangeFromSource, _enemySheet.Attack2_Range);
        
        }
    }
}

