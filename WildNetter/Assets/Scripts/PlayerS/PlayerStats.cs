
using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    static PlayerStats _instance;
 
    [SerializeField] Stats playerStats;
    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:

    private void Awake()
    {
        _instance = this;
    }

    public static PlayerStats GetInstance
    {
        get { return _instance; }
    }
    public int GetSetStrengh
    {
        get { return playerStats.strengh; }
        set { playerStats.strengh = value; }
    }
    public int GetSetWisdom
    {
        get { return playerStats.wisdom; }
        set { playerStats.wisdom = value; }
    }
    public int GetSetAgility
    {
        get { return playerStats.agility; }
        set { playerStats.agility = value; }
    }
    public int GetSetCurrentEXP
    {
        get { return playerStats.currentEXP; }
        set { playerStats.currentEXP = value; }
    }
    public int GetSetExpToNextLevel
    {
        get { return playerStats.expToNextLevel; }
        set { playerStats.expToNextLevel = value; }
    }
    public int GetSetAbilityPoints
    {
        get { return playerStats.abilityPoints; }
        set { playerStats.abilityPoints = value; }
    }
    public int GetSetCurrentHealth
    {
        get { return playerStats.currentHealth; }
        set
        {
            Debug.Log("Current health is: " + playerStats.currentHealth);
            playerStats.currentHealth = value;
            Debug.Log("Current health is: " + playerStats.currentHealth);

            if (playerStats.currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }

        }
    }
    public int GetSetMaxHealth
    {
        get { return playerStats.maxHealth; }
        set { playerStats.maxHealth = value; }
    }
    public int GetSetArmorPoints
    {
        get { return playerStats.armorPoints; }
        set { playerStats.armorPoints = value; }
    }

    #region Stamina 
    [SerializeField] float defaultStaminaRegenerationSpeed = 4f;
    [SerializeField] float currentStamina = 0;
    float staminaQuater = 25f;

    public float GetSetMaxStaminaBar {
        get => playerStats.MaxStaminaBar;
        set {
            
            if (value < playerStats.MaxStaminaBar)
                return;

            playerStats.MaxStaminaBar = value;
        }
    }
    public int GetSetStaminaPoints
    {
        get { return playerStats.stamina; }
        set { playerStats.stamina = value; }
    }
    public float GetSetStaminaRegenerationSpeed {
        set
        {
            defaultStaminaRegenerationSpeed = value;
            if (defaultStaminaRegenerationSpeed < 0)
            {
                defaultStaminaRegenerationSpeed = 0;
            }

        }
        get => defaultStaminaRegenerationSpeed;

    }
  public float GetSetStaminaBar
    {
        get { return currentStamina; }

        set {


            if (currentStamina + value <= 0)
            {
                currentStamina = 0;
                return;
            }
            currentStamina = value;

            if (currentStamina > playerStats.MaxStaminaBar)
                currentStamina = playerStats.MaxStaminaBar;
            

        }
    }
    #endregion

  
    //Functions:
    public void Init()
    {
        playerStats.ResetStats();
        playerStats.MaxStaminaBar = staminaQuater * GetSetStaminaPoints;
       currentStamina = playerStats.MaxStaminaBar;
        StopCoroutine(Regeneration());
        StartCoroutine(Regeneration());
    }


    IEnumerator Regeneration() {

        // check if some action happens
        GetSetStaminaBar += GetSetStaminaRegenerationSpeed;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Regeneration());
    }
    public void ApplyDMG(int amount) { }
    //public int DMGAfterArmour(int amount) { return 1; }  < return the dmg after the armor protection 
    public void HealPlayer(int amount) { }

    internal bool CheckEnoughStamina(float amount)
    {
        if (amount<0)
           amount *= -1;
        
        if (GetSetStaminaBar - amount < 0)
           return false;

        GetSetStaminaBar -= amount;
        return true;
    }
}