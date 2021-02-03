using System.Collections;
using System.Runtime.Remoting.Messaging;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{


    [SerializeField] Stats playerStats;
    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:

    private void Awake()
    {
        _instance = this;
    }
    static PlayerStats _instance;
    public static PlayerStats GetInstance
    {
        get { return _instance; }
    }
    public void Init()
    {
        playerStats.ResetStats();
        playerStats.MaxStamina = staminaPerLevel * GetSetStaminaPoints;
        currentStamina = playerStats.MaxStamina;



        StopCoroutine(Regeneration());
        StartCoroutine(Regeneration());
    }




    #region Player Stats
    public int playerLevel;
    public int GetSetAbilityPoints
    {
        get { return playerStats.abilityPoints; }
        set { playerStats.abilityPoints = value; }
    }
    public int GetSetStrength
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













    #endregion
    #region EXP Manager


    public int GetCurrentEXP
    {
        get { return playerStats.currentEXP; }
    }
    public int GetSetExpToNextLevel
    {
        get { return playerStats.expToNextLevel; }
        set { playerStats.expToNextLevel = value; }
    }


    public void GainEXP(int amount)
    {
        if (amount == 0)
            return;

        if (amount < 0)
            amount *= -1;

        playerStats.currentEXP += amount;

        if (GetSetExpToNextLevel - GetCurrentEXP == 0)
            LevelUp();
        else if (GetSetExpToNextLevel - GetCurrentEXP <= 0)
        {
            LevelUp();
            GainEXP(GetSetExpToNextLevel - GetCurrentEXP);
        }
    }


    int EXPtoNextLevelModifier = 2;
    void LevelUp()
    {
        playerLevel++;
        playerStats.currentEXP = 0;
        GetSetExpToNextLevel *= playerLevel / EXPtoNextLevelModifier;
    }
    #endregion

















    #region Health
    public int GetSetMaxHealth
    {
        get { return playerStats.maxHealth; }
        set { playerStats.maxHealth = value; }
    }
    public int GetSetCurrentHealth
    {
        get { return playerStats.currentHealth; }
        set
        {
       
            if (playerStats.currentHealth + value >= playerStats.maxHealth) { 
                playerStats.currentHealth = playerStats.maxHealth;
                return;
            }
 
            playerStats.currentHealth = value;
        

            if (playerStats.currentHealth <= 0)
            { 
                playerStats.currentHealth = 0;
                gameObject.SetActive(false);
            }

        }
    }
    public int GetSetArmorPoints
    {
        get { return playerStats.armorPoints; }
        set { playerStats.armorPoints = value; }
    }


    public void AddHealthAmount(int amount)
    {
        if (amount == 0)
            return;


        if (amount < 0)
            amount *= 0;

        GetSetCurrentHealth += amount;
    }
    public void ApplyDMGToPlayer(int amount)
    {
        if (amount == 0)
            return;

        if (amount < 0)
            amount *= -1;





        GetSetCurrentHealth -= DMGAfterArmour(amount);

    }
    public int DMGAfterArmour(int amount) { return amount; }//  < return the dmg after the armor protection 

    #endregion























    #region Stamina 
    [SerializeField] float defaultStaminaRegeneration = 4f;
    [SerializeField] float currentStamina = 0;
    float staminaPerLevel = 25f;

    public float GetSetMaxStamina
    {
        get => playerStats.MaxStamina;
        set
        {

            if (value < playerStats.MaxStamina)
                return;

            playerStats.MaxStamina = value;
        }
    }

    public float GetSetStaminaRegenerationAmount
    {
        set
        {
            defaultStaminaRegeneration = value;
            if (defaultStaminaRegeneration < 0)
            {
                defaultStaminaRegeneration = 0;
            }

        }
        get => defaultStaminaRegeneration;

    }
    public float GetPlayerStamina
    {
        get { return currentStamina; }

    }

    public void AddStaminaAmount(float amount)
    {

        if (amount == 0)
            return;


        if (currentStamina + amount >= GetSetMaxStamina)
        {
            currentStamina = GetSetMaxStamina;
            return;
        }

        else if (currentStamina + amount <= 0)
        {
            currentStamina = 0;
            return;
        }


        currentStamina += amount;

    }

    internal bool CheckEnoughStamina(float amount)
    {
        if (amount < 0)
            amount *= -1;

        if (GetPlayerStamina - amount < 0)
            return false;

        AddStaminaAmount(-amount);
        return true;
    }
    #endregion

























    #region Regeneration Params
    int healthRegenerationAmount = 4;
    [SerializeField] bool stopStaminaRegeneration = false;
    [SerializeField] bool stopHealthRegeneration = false;
    public bool SetStopHealthRegeneration
    {
        set
        {
            if (stopHealthRegeneration != value)
                stopHealthRegeneration = value;

        }
    }
    public bool SetStopStaminaRegeneration
    {
        set
        {
            if (stopStaminaRegeneration != value)
                stopStaminaRegeneration = value;
        }
    }
    public int GetSetHPRegenerationSpeed { set => healthRegenerationAmount = value; get => healthRegenerationAmount; }
    IEnumerator Regeneration()
    {

        // check if some action happens
        if (!stopStaminaRegeneration)
            AddStaminaAmount(GetSetStaminaRegenerationAmount);

        if (!stopHealthRegeneration)
            AddHealthAmount(healthRegenerationAmount);

        yield return new WaitForSeconds(1f);
        StartCoroutine(Regeneration());
    }
    #endregion

}