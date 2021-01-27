
using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    static PlayerStats _instance;
    [SerializeField] Stats playerStats;
  [SerializeField]  float staminaBar  =0;
    float staminaQuater = 25f;
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
    public int GetSetStaminaPoints
    {
        get { return playerStats.stamina; }
        set { playerStats.stamina = value; }
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
    
    public float GetSetStaminaBar
    {
        get { return staminaBar; }

        set {


            if (staminaBar + value <= 0)
            {
                staminaBar = 0;
                return;
            }
            staminaBar = value;

            if (staminaBar > playerStats.MaxStaminaBar)
                staminaBar = playerStats.MaxStaminaBar;
            

        }
    }
    //Functions:
    public void Init()
    {
        playerStats.ResetStats();
        playerStats.MaxStaminaBar = staminaQuater * GetSetStaminaPoints;
        staminaBar = playerStats.MaxStaminaBar;
        StopCoroutine(Regeneration());
        StartCoroutine(Regeneration());
    }

  [SerializeField]  float staminaRegenerationSpeed= 4f;
    IEnumerator Regeneration() {

        // check if some action happens
        GetSetStaminaBar += staminaRegenerationSpeed;

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