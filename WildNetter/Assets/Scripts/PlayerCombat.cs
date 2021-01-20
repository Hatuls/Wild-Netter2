using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    // Script References:
    WeaponSO equippedWeaponSO;
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    // Component References:
    
   [SerializeField] Collider _weaponCollider;
    [SerializeField]GameObject _weaponGO;

    // Variables:
    bool canAttack;
     int  attackDMG;
    public string currentWeaponName;
    event Action AttackAction;

    //move to player manager
    public void GetHit(int RecieveDMG, Vector3 Source)
    {
        playerStats.GetSetCurrentHealth += -RecieveDMG;
        playerMovement.GetPlayerRB.AddExplosionForce(100 * 15, new Vector3(Source.x, 0, Source.z), 4);
    }
    // Getter & Setters:
    public WeaponSO GetSetWeaponSO {
        get { return equippedWeaponSO; }
        set { 
            
            equippedWeaponSO = value;
            currentWeaponName = equippedWeaponSO.Name;
        }

    }
    public int GetSetAttackDMG { 
        get {
            System.Random rnd = new System.Random();
            
            attackDMG = rnd.Next(GetSetWeaponSO.minDMG, GetSetWeaponSO.maxDMG);

            return attackDMG;
        } 
   
    }

    public void Init(WeaponSO startingWeapon)
    {
        ToggleWeaponCollider(false);
           playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();
        Debug.Log(startingWeapon.GetType());
        canAttack = true;
        GetSetWeaponSO = startingWeapon;
        ResetAttackAction();
        AttackAction += MeleeAttack;
    }
    private void Update()
    {
        SetAttackAction();
        Attack();
        if (Input.GetKeyDown(KeyCode.E))
        {
            DeployTotem(TotemType.detection);
            StartCoroutine(FreezeMovement(1f));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeployTotem(TotemType.healing);
            StartCoroutine(FreezeMovement(1f));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DeployTotem(TotemType.prey);
            StartCoroutine(FreezeMovement(1f));
        }
    }

    private void Attack() {  
        if (Input.GetMouseButtonDown(0) && canAttack && !EventSystem.current.IsPointerOverGameObject())
        {
            AttackAction();
            PlayerGFX._instance.SetAnimationTrigger("Attack");
        }
    }




    public void MeleeAttack() {
        Debug.Log("SWord AttacK");
        StartCoroutine(MeleeAttackCoroutine());
    // apply GFX Anim, sound
    }   
    public void RangeAttack() {
      
        Debug.Log("Range AttacK");

    }

    public void DeployTotem(TotemType type)
    {

        PlayerGFX._instance.SetAnimationTrigger("PlaceTotem");
        TotemManager._instance.DeployAtLocation((transform.position + playerMovement.GetAngleDirection()*2f), type);
    }

    public void SetAttackAction() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {

            ResetAttackAction();
          AttackAction += MeleeAttack;
            Debug.Log("MeleeAttack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetAttackAction();
            AttackAction += RangeAttack;
            Debug.Log("Range");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResetAttackAction();
            //AttackAction += DeployTotem;
            Debug.Log("Deploy Totem");
        }
    
    
    
    
    }

    private void ResetAttackAction()
    {
        AttackAction -= RangeAttack;
        //AttackAction -= DeployTotem;
        AttackAction -= MeleeAttack;
    }


    //  private Vector3 LockOnEnemy() { } <- will be used later on

    private void ToggleWeaponCollider(bool state) => _weaponCollider.enabled = state;


    // ienumerators:
    IEnumerator  MeleeAttackCoroutine() {
       
        canAttack = false;
        ToggleWeaponCollider(true);
        yield return FreezeMovement(1f);
        ToggleWeaponCollider(false);
        canAttack = true;
      
    }

    IEnumerator FreezeMovement(float duration) {
        playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(duration);
        playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnDestroy()
    {
        ResetAttackAction();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_weaponCollider.enabled)
        {
             other.gameObject.GetComponent<EnemyPart>().GetDamage(GetSetAttackDMG,transform.position,GetSetWeaponSO.vulnerabilityActivator);
            
        }
    }
}
