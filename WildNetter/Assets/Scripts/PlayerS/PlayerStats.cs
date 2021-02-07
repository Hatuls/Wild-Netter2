
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoSingleton<PlayerStats>
{


    [SerializeField] Stats playerStats;
    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:

 
    public override void Init()
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


    public float GetCurrentEXP
    {
        get { return playerStats.currentEXP; }
    }
    public float GetSetExpToNextLevel
    {
        get { return playerStats.expToNextLevel; }
        set { playerStats.expToNextLevel = value; }
    }


    public void GainEXP(float amount)
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
    public float GetSetMaxHealth
    {
        get { return playerStats.maxHealth; }
        set { playerStats.maxHealth = value; }
    }
    public float GetSetCurrentHealth
    {
        get { return playerStats.currentHealth; }
        set
        {
            if (value > playerStats.currentHealth)
                TextPopUp.Create(TextType.Healing, transform.root.position, (int)(playerStats.currentHealth - value));
            
            else
                TextPopUp.Create(TextType.CritDMG, transform.root.position, (int)(playerStats.currentHealth - value));

            


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


    public void AddHealthAmount(float amount)
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

    public bool CheckEnoughStamina(float amount)
    {
        if (amount < 0)
            amount *= -1;

        if (GetPlayerStamina - amount < 0)
            return false;

        return true;
    }
    #endregion

























    #region Regeneration Params
    const float healthRegenerationAmount = 2;
    const float staminaRegenerationAmount = 2;

    List<Buffs> RegenerationBuffs ;

    [SerializeField] bool stopStaminaRegeneration = false;
    [SerializeField] bool stopHealthRegeneration = true;
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

    private void RemoveBuffRegeneration(Buffs buff)
    {
        if (buff == null || RegenerationBuffs == null || RegenerationBuffs.Count <=0)
            return;

        if (RegenerationBuffs.Contains(buff))
            RegenerationBuffs.Remove(buff);
    }
    public void AddBuffRegeneration(Buffs buff) {
        if (buff == null)
            return;

        if (buff.GetIsOverTime == false)
        {
            switch (buff.GetRegenerationType)
            {
                case RegenerationType.Stamina:
                    AddStaminaAmount(buff.GetAmount);
                    break;
                case RegenerationType.Health:
                    AddHealthAmount(buff.GetAmount);
                    break;
                default:
                    break;
            }
            return;
        }

        if (RegenerationBuffs == null)
            RegenerationBuffs = new List<Buffs>();

        RegenerationBuffs.Add(buff);
    }
    IEnumerator Regeneration()
    {

        // check if some action happens
        if (!stopStaminaRegeneration)
            StaminaRegeneration();

        if (stopHealthRegeneration)
            HealthRegeneration();

        yield return new WaitForSeconds(1f);
        CheckBuffTimers();
        StartCoroutine(Regeneration());
    }


    private void CheckBuffTimers() {
        if (RegenerationBuffs == null || RegenerationBuffs.Count == 0)
            return;

        for (int i = 0; i < RegenerationBuffs.Count; i++)
        {
            if (!RegenerationBuffs[i].CheckIfSupposedToContinue())
             RemoveBuffRegeneration(RegenerationBuffs[i]);
        }


    }
    private void StaminaRegeneration() {
        float totalAmount = staminaRegenerationAmount;


        if (RegenerationBuffs != null && RegenerationBuffs.Count > 0)
        {
            for (int i = 0; i < RegenerationBuffs.Count; i++)
            {
                if (RegenerationBuffs[i].GetRegenerationType == RegenerationType.Stamina)
                    totalAmount += RegenerationBuffs[i].GetAmount;
            }
        }


        AddStaminaAmount(totalAmount);
    }
    private void HealthRegeneration() {
        float totalAmount = healthRegenerationAmount;

        if (RegenerationBuffs!= null && RegenerationBuffs.Count > 0)
        {
            for (int i = 0; i < RegenerationBuffs.Count; i++)
            {
                if (RegenerationBuffs[i].GetRegenerationType == RegenerationType.Health)
                    totalAmount += RegenerationBuffs[i].GetAmount;
            }
        }



        AddStaminaAmount(totalAmount);
    }

    #endregion


}
public enum RegenerationType { Stamina,Health }


public class Buffs {

  bool isOverTime;
    float amount;
    RegenerationType typeOf;
    float? endTime;
    public Buffs(RegenerationType type, float amount, float timer) {
        typeOf = type;
        isOverTime = true;
        this.amount = amount;
        endTime = Time.time + timer;
    }
    public Buffs(RegenerationType type, float amount)
    {
        this.amount = amount;
        typeOf = type;
        isOverTime = false;
    }

    public bool CheckIfSupposedToContinue() {

        if (!isOverTime || endTime > Time.time)
            return false;
        return true ;

    }
    public float GetAmount => amount;
    public RegenerationType GetRegenerationType => typeOf;
    public bool GetIsOverTime => isOverTime;

}