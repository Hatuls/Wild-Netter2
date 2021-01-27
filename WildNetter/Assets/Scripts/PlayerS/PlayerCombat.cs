using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine;

public enum AttackType {Melee , Ranged, Totem };
public class PlayerCombat : MonoBehaviour
{
    // Script References:
    WeaponSO _equippedWeaponSO;
    PlayerMovement _playerMovement;
    PlayerStats _playerStats;
    // Component References:
    
   [SerializeField] Collider _weaponCollider;
    [SerializeField]GameObject _weaponGO;
    static PlayerCombat _instance;
    public static PlayerCombat GetInstance { get { return _instance; } }
    // Variables:
    bool canAttack;
     int  attackDMG;
    public string currentWeaponName;
    event Action AttackAction;


    // Getter & Setters:
    public WeaponSO GetSetWeaponSO {
        get { return _equippedWeaponSO; }
        set { 
            
            _equippedWeaponSO = value;
            currentWeaponName = _equippedWeaponSO.Name;
        }

    }
    public int GetSetAttackDMG { 
        get {
            System.Random rnd = new System.Random();
            
            attackDMG = rnd.Next(GetSetWeaponSO.minDMG, GetSetWeaponSO.maxDMG);

            return attackDMG;
        } 
   
    }
    private void Awake()
    {
        _instance = this;
    }
    public void Init(WeaponSO startingWeapon)
    {
        ToggleWeaponCollider(false);
        _playerStats = PlayerStats.GetInstance;
        _playerMovement = PlayerMovement.GetInstance;
        Debug.Log(startingWeapon.GetType());
        canAttack = true;
        GetSetWeaponSO = startingWeapon;
        ResetAttackAction();
        AttackAction += MeleeAttack;
    }
    private void Update()
    {
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

    //move to player manager
    public void GetHit(int RecieveDMG, Vector3 Source)
    {
        _playerStats.GetSetCurrentHealth += -RecieveDMG;
        _playerMovement.GetPlayerRB.AddExplosionForce(100 * 15, new Vector3(Source.x, 0, Source.z), 4);
    }
    public void Attack() {  
        if (canAttack && !EventSystem.current.IsPointerOverGameObject())
        {
            AttackAction?.Invoke();
            PlayerGFX.GetInstance.SetAnimationTrigger("Attack");
        }
    }
     void MeleeAttack() {
        Debug.Log("SWord AttacK");
        StartCoroutine(MeleeAttackCoroutine());
    // apply GFX Anim, sound
    }   
     void RangeAttack() {
      
        Debug.Log("Range AttacK");

    }
     void DeployTotem(TotemType type)
    {

        PlayerGFX.GetInstance.SetAnimationTrigger("PlaceTotem");
        TotemManager._instance.DeployAtLocation((transform.position + _playerMovement.GetAngleDirection()*2f), type);
    }
    public void SetAttackType(AttackType type) {
    
     ResetAttackAction();

        switch (type)
        {
            case AttackType.Melee:
                AttackAction += MeleeAttack; Debug.Log("MeleeAttack");
                break;
            case AttackType.Ranged:
                AttackAction += RangeAttack;
                Debug.Log("Range");
                break;
            case AttackType.Totem:
                //AttackAction += DeployTotem;
                Debug.Log("Deploy Totem");
                break;
            default:
                break;
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
        _playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(duration);
        _playerMovement.GetPlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void OnDestroy()
    {
        ResetAttackAction();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_weaponCollider.enabled)
        {
            CalculateDMGToEnemy(other.gameObject.GetComponent<EnemyPart>());
        }
    }
    void CalculateDMGToEnemy(EnemyPart enemy) {
        int finalDmg = GetSetAttackDMG;
        finalDmg += Convert.ToInt32(  finalDmg * (_playerStats.GetSetStrength*.1f));//- enemy.armor
       enemy.GetDamage(finalDmg, transform.position, GetSetWeaponSO.vulnerabilityActivator);
    }
}
