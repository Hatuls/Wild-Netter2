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
    // Component References:
    
   [SerializeField] Collider _weaponCollider;
    [SerializeField]GameObject _weaponGO;

    // Variables:
    bool canAttack;
     int  attackDMG;
    public string currentWeaponName;
    event Action AttackAction;

    //move to player manager
    public void GetHit()
    {
        Debug.Log("playerGotHit");
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
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeployTotem(TotemType.healing);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DeployTotem(TotemType.prey);
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
        playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(equippedWeaponSO.HitSpeed);
        playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
        ToggleWeaponCollider(false);
        canAttack = true;
      
    }

    private void OnDestroy()
    {
        ResetAttackAction();
    }
    private void OnTriggerEnter(Collider other)
    {

        other.gameObject.GetComponent<Enemy>().GetDMG(equippedWeaponSO.maxDMG);
    }
}
