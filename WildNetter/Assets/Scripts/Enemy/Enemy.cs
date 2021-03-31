
using System.Collections;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    // Script References:
    public Enemy _instance;
    public EnemySO _enemySO;
    public EnemySheet _enemySheet;
    public EnemyType enemytype;
    
    
    
    // Component References:
    public MeshRenderer _enemyMesh;
    public Collider _enemyHitTriggerCollider;
    public NavMeshAgent agent;
    public Animator _animator;
    public GameObject TargetAquierd;
    internal Rigidbody rb;
    public Material originalMat;
    public ParticleSystem footsteps;
    
        
    [SerializeField] bool showGizmos;

    [SerializeField] int currentHP;
    private monsterParts partGotHit;
    private int enemyID = 0;
    private int dropLevel;
    private int dropAmont;

  [SerializeField]  private float defaultSpeed;
    private float slowAmount;
 
    bool isHeading = false;
    bool isKnocked = false;
    bool aggroChecking;

   internal bool attack1_inCd, attack2_inCd;
    Vector3 CurrentPos;
    Vector3 nextPos;
    public bool wanderingEnabled;
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
        rb = GetComponent<Rigidbody>();
        
    }
    private void Awake()
    {
        
        Init();
    }
    public void Init()
    {

        _instance = this;

        _enemySheet = new EnemySheet();
        defaultSpeed = _enemySheet.movementSpeed;
        GetInfoFromEnemySO();
        _enemyMesh = GetComponentInChildren<MeshRenderer>();
        originalMat = _enemyMesh.material;

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
        _enemySheet.DamagedMat = GetEnemySO.DamagedMat;
        _enemySheet.Trails = GetEnemySO.Trails;


        //SetValues//
        int StatsMultiplayer = 1;
        int SizeMultiplayer = 1;
        switch (_enemySheet.enemyDifficulty)
        {
            case Difficulty.Easy:
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
                  dropAmont = 3;// <= used for presentation delete me and undo the next one
                //  dropAmont = 1;
                break;

            case Size.Medium:
                SizeMultiplayer = 2;
                dropAmont =3 ; // <= used for presentation delete me and undo the next one
                //dropAmont = 2;
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
        _enemySheet.Attack1_Cd = GetEnemySO.Attack1_Cd * SizeMultiplayer;
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
        //aggroPoint
        _enemySheet.lookRange = GetEnemySO.lookRange;
        _enemySheet.rangeFromBody = GetEnemySO.rangeFromBody;
        _enemySheet.stayInRangeTime = GetEnemySO.stayInRangeTime;
        //Timers
        _enemySheet.getUpAnimTime = GetEnemySO.getUpAnimTime;


        //setting Componnents Values
        SetSpeed(_enemySheet.movementSpeed);
        // footsteps = _enemySheet.Trails;


        defaultSpeed = agent.speed;
    }
    public void AddPart(monsterParts partType,GameObject partGO,Collider col)
    {
        _enemySheet.EnemyParts.Add(partType, partGO);
        _enemySheet.Colliders.Add(partType, col);
    
    }
   
   
    private void FixedUpdate()
    {
        //determains Enemy Current State
        BehaveByState(_enemySheet.enemyState);
        //Determains Current Target
        TargetFinder();
        
    }
    public void ActivateFootsteps(bool Activate)
    {
        if (Activate)
        {
            footsteps.Play();

        }
        else
        {
            footsteps.Stop();
        }

    }
    void BehaveByState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle :
               
                StartCoroutine(Iwander(wanderingEnabled,state, _enemySheet.wanderRadius));
               

              
                break; 

            case EnemyState.Chase :
                
                if (TargetAquierd.gameObject != null)
                {
                    if (!isKnocked)
                    {
                      Move(TargetAquierd.transform.position);
                      AttacksAI();
                    }
                   

                }
                break;

            case EnemyState.Attack:
                StopMoving();
                break;

            case EnemyState.lured:
                StartCoroutine(Iwander(true, state,TargetAquierd.GetComponent<Totem>().relevantSO.range));
                if (!aggroChecking)
                {
                    StartCoroutine(AggroCheck());
                }
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
                ActivateFootsteps(true);

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
        if (agent.enabled!=false)
        {

        if (agent.isStopped)
        {
        agent.Resume();
            ActivateFootsteps(true);
        }
        agent.destination = Target;
        }
        
    }
    //Currently for totems
    public void StopMoving()
    {
        if (agent.enabled != false)
        {

        ActivateFootsteps(false);

        if (agent.isOnNavMesh)
   agent.isStopped = true ;
        

        }

        
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

        GetSetEnemyCurrentHP -=  dmgToApply;
    }
    private void ApplyKnockback(int force,Vector3 Source)
    {
        isKnocked = true;
        agent.enabled = false;
         rb.isKinematic = false;
        
        
        rb.AddExplosionForce(force*20,new Vector3(Source.x, 0, Source.z), 4);
        
        
    }
    public void SlowSetter(float slowAmount, bool add)
    {
        if (isSlowed == add)
            return;
        isSlowed = add;



        if (!add)
            SetSpeed(defaultSpeed);

        else
            SetSpeed(_enemySheet.movementSpeed - (slowAmount * _enemySheet.movementSpeed / 100f));


    }
   public void SetSpeed(float speed)
    {
        agent.speed = speed;
        Debug.Log("Speed is : " + agent.speed);
    }
    public void Debuffer(Debuff debuffType, int effectTime, int effectInPresentage)
    {
        StartCoroutine(DebuffHandler(debuffType, effectTime, effectInPresentage));
         
    }

   
   
    IEnumerator DebuffHandler(Debuff debuffType, int effectTime, int effectInPresentage)
    {
        switch (debuffType)
        {
            case Debuff.Slow:
                float slowInValue= (_enemySheet.movementSpeed / 100) *effectInPresentage;
                SlowSetter(slowInValue, true);
                yield return new WaitForSeconds(effectTime);
                SlowSetter(slowInValue, false);

                break;
             
        }
       
    }

    public void FlatDamage(int Damage)
    {
        PlayerManager._Instance.getPlayerGfx.ApplyPlayerVFX( transform.position , VFXWorldType.EnemyGotHit);
        OnRecieveDmg(Damage);
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


        for (int i = 0; i < dropAmont; i++)
        {
            var Dropable = PickUpObject.SpawnItemInWorld(_enemySO.lootDropsID[Random.Range(0 , _enemySO.lootDropsID.Length)], dropAmont, RetrieveDeathLocation(), TargetAquierd.transform);

            Debug.Log("Deploy Me" + _enemySO.lootDropsID[dropLevel - 1]);

        }



    }

    public bool CheckIfPlayerIsClose() { return true; }
    //public Transform GetTarget() { } <- clear comment when getting new target to move to

     
 bool isSlowed = false;
  

    public void TotemEffect(TotemName type,GameObject totem) 
    {
        switch (type)
        {
           

            case TotemName.prey:
                
                TargetAquierd = totem.gameObject;
                _enemySheet.enemyState = EnemyState.lured;
                break;
        }
    }
    public void CancelEffect()
    {        
        //BehaveByState(EnemyState.Idle);
    }

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




    //public void OnTriggerEnter(Collider other)
    //{
        
    //    if (other.gameObject.name == "WeaponCollider")
    //    {
           
    //        if (!isTriggered)
    //        {
    //            isTriggered = true;
    //         _enemyHitTriggerCollider.enabled = false;
    //        StartCoroutine(ReceiveDmgCoolDown());
                
    //        }
          
    //    }
    //}
    public void GetDamageFromPart(int Damage,monsterParts part,Vector3 hitPoint)
    {
        
        
            _enemySheet.Colliders[part].enabled = false;
            partGotHit = part;
            Debug.Log(_enemySheet.Colliders[part].enabled);
           _enemyMesh.material = _enemySheet.DamagedMat;

            
            
            StartCoroutine(ReceiveDmgCoolDown(Damage,part));
       
            ApplyKnockback(Damage , hitPoint);

        if (_enemySheet.enemyState == EnemyState.lured)
        {
            _enemySheet.enemyState = EnemyState.Chase;
        }
   
    }
    public  IEnumerator ReceiveDmgCoolDown(int Damage,monsterParts part)
    {
        float cooldown = 1.5f;
        OnRecieveDmg(Damage);

        yield return new WaitForSeconds(0.2f);
        _enemyMesh.material = originalMat;
        yield return new WaitForSeconds(cooldown);
        
       StartCoroutine(GetuP());
        _enemySheet.Colliders[part].enabled = true;

        
    }
    IEnumerator GetuP()
    {
        yield return new WaitForSeconds(_enemySheet.getUpAnimTime);
        rb.isKinematic = true;
        agent.enabled = true;
        isKnocked = false;
        transform.LookAt(TargetAquierd.transform.position);
    }
    public void PartBroke(monsterParts part)
    {
        Debug.Log(part +"Broke");
        _enemySheet.EnemyParts[part].SetActive(false);

        PartDebuffer();
    }
    void PartDebuffer()
    {
        
    }
    public IEnumerator Iwander(bool wander,EnemyState state,float range)
    {

      
         

        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.velocity.magnitude < 0.15f && (_enemySheet.enemyState == EnemyState.Idle|| _enemySheet.enemyState == EnemyState.lured) &&!isHeading)
        {
            Debug.Log(state);
            isHeading = true;
            CurrentPos = transform.position;
            if(state == EnemyState.lured)
            {
                nextPos = TargetAquierd.transform.position + (Random.insideUnitSphere * range);
            }
            else
            {

            nextPos = CurrentPos + (Random.insideUnitSphere *_enemySheet.wanderRadius);
            }
            if (wander)
            {
                agent.SetDestination(nextPos);
            }
            yield return new WaitForSeconds(5);
            isHeading = false;
        }
        
        yield return null;
       
    }
    public abstract IEnumerator Attack1();
    public abstract IEnumerator Attack2();

    public virtual IEnumerator AggroCheck()
    {
        aggroChecking = true;
        Collider[] col = Physics.OverlapSphere(new Vector3(transform.position.x,transform.position.y,transform.position.z+ _enemySheet.rangeFromBody) 
            , _enemySheet.lookRange,TargetLayer);
        foreach(Collider found in col)
        {
            Debug.Log("aggroChecking");
            yield return new WaitForSeconds(_enemySheet.stayInRangeTime);
            Collider[] col2 = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + _enemySheet.rangeFromBody)
            , _enemySheet.lookRange, TargetLayer);
            foreach(Collider found2 in col2)
            {
                if (found2 == found)
                {
                    _enemySheet.enemyState = EnemyState.Chase;
                }
            }


        }
        yield return null;
        aggroChecking = false;
       
    }
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

