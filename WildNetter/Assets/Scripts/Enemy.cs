
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Script References:
    public Enemy _instance;
    [SerializeField] EnemySO _enemySO;

    // Component References:
    public MeshRenderer _enemyMesh;
    public Collider _enemyHitTriggerCollider;
    [SerializeField] NavMeshAgent agent;
    public Animator _animator;
    [SerializeField] Transform TargetPos;

    [SerializeField] int currentHP;
    private int enemyID = 0;
    bool isTriggered = false;
    private float sightRange = 10f;
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

    private void Start()
    {
        Init();

    }
    public void Init()
    {
        _instance = this;




        GetInfoFromEnemySO();
     



    }
   
    private void GetInfoFromEnemySO()
    {
        GetSetEnemyCurrentHP = GetEnemySO.maxHealth;
    }

    private void FixedUpdate()
    {
        IsPlayerInRange();
    }

    private void OnRecieveDmg(int dmgToApply) {

        Debug.Log("GotDMG");
        

        GetSetEnemyCurrentHP -=  dmgToApply;
    }
    public void Move()
    {
        agent.destination = TargetPos.position;
    }
    public void Attack() { }
    public void EnemyKilled() { 
        DropLoot(); 
        Destroy(gameObject);
    }
    public void DropLoot() {
     
        for (int i = 0; i < _enemySO.lootDropsID.Length; i++)
        {
        var f=  PickUpObject.SpawnItemInWorld(ItemFactory.GetInstance().GenerateItem(_enemySO.lootDropsID[i]), RetrieveDeathLocation(), TargetPos);
            if (i ==0)
            {
                f.GetComponent<PickUpObject>().GetItem().amount = 5;

            }  
        }
    
    
    
    }
    public bool CheckIfPlayerIsClose() { return true; }
    //public Transform GetTarget() { } <- clear comment when getting new target to move to
    public void TotemEffect() { }

    public void PlayAnimation() { }
    public Vector3 RetrieveDeathLocation() { return transform.position; }

    public void IsPlayerInRange()
    {

        if (Vector3.Distance(TargetPos.position, transform.position) >= sightRange)
        {
            if (transform.GetChild(0).gameObject.activeSelf)
                ToggleTriggerGameObject(false);

            return;
        }
        else { 
            ToggleTriggerGameObject(true);
            Move();

        }

    }

    public void ToggleTriggerGameObject(bool isInRange)
    {
          _enemyHitTriggerCollider.enabled = isInRange;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isInRange);
           
        }
    }

   
   

    private void OnTriggerEnter(Collider other)
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
    IEnumerator ReceiveDmgCoolDown()
    {
        float cooldown = 3f;
        
        OnRecieveDmg(PlayerManager.GetInstance().GetPlayerCombat.GetSetAttackDMG);
        yield return new WaitForSeconds(cooldown);
        _enemyHitTriggerCollider.enabled = true;
      isTriggered = false;
    }
}
