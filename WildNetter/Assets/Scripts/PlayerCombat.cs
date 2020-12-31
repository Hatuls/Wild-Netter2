using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Script References:
    WeaponSO _equippedWeapon;
    // Component References:
    
   [SerializeField] Collider _weaponCollider;
    [SerializeField]GameObject _weaponGO;

    // Variables:
    bool canAttack;
     int  attackDMG;
    public string currentWeaponName;
    event Action AttackAction;

    public void GetHit()
    {
        Debug.Log("playerGotHit");
    }
    // Getter & Setters:
    public WeaponSO GetSetWeaponSO {
        get { return _equippedWeapon; }
        set { 
            
            _equippedWeapon = value;
            currentWeaponName = _equippedWeapon.Name;
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
        TotemManager._instance.DeployAtLocation(transform.position + TotemManager._instance.totemOffset, type);
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

    private void ToggleWeaponCollider(bool state) => _weaponGO.SetActive(state);





    // ienumerators:
    IEnumerator  MeleeAttackCoroutine() {
        canAttack = false;
        ToggleWeaponCollider(true);

        yield return new WaitForSeconds(_equippedWeapon.HitSpeed);
        ToggleWeaponCollider(false);
        canAttack = true;
    }

    private void OnDestroy()
    {
        ResetAttackAction();
    }
}
