
using System;
using System.Collections;

using UnityEngine;


public class PlayerStats : MonoBehaviour
{


    [SerializeField] Stats playerStats;
    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:


    public void Init()
    {
        Debug.Log("PlayerStat Init");
        playerStats.ResetStats();
        playerStats.MaxStamina = staminaPerLevel * GetSetStaminaPoints;
        currentStamina = playerStats.MaxStamina;
        UiManager._Instance.SetMaxHealth(GetSetMaxHealth);
        UiManager._Instance.SetMaxStamina(GetSetMaxStamina);

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
        set 
        { 
            playerStats.maxHealth = value;
        }
    }


    public float GetSetCurrentHealth
    {
        get { return playerStats.currentHealth; }
        set
        {
        
            if (value< playerStats.currentHealth)
                TextPopUp.Create(TextType.CritDMG, transform.root.position, (int)(playerStats.currentHealth - value));




            if (playerStats.currentHealth + (value - playerStats.currentHealth) >= playerStats.maxHealth) {
               
                playerStats.currentHealth = playerStats.maxHealth;
                UiManager._Instance.SetHealth(playerStats.currentHealth);
                return;
            }
            if (value >playerStats.currentHealth )
                 TextPopUp.Create(TextType.Healing, transform.root.position, (int)(playerStats.currentHealth - value) * -1);
            
            playerStats.currentHealth = value;
            UiManager._Instance.SetHealth(playerStats.currentHealth);
       

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
            amount *= -1;

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
        Debug.Log("Using Stamina");
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
        UiManager._Instance.SetStamina(currentStamina);

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

















    #region BuffArrayFunctions
   [SerializeField] Buffs[] buffsArr;
     int capacityTree =5;
    int counter = 0;
    void AddBuff(Buffs f) {
        if (buffsArr == null)
            buffsArr = new Buffs[5];

           AddCapacity();

        for (int i = 0; i < buffsArr.Length; i++)
        {
            if (buffsArr[i] == null)
            {
                counter++;
                buffsArr[i] = f;
                return;
            }
        }

    }
    void AddCapacity() {
        if (counter == capacityTree -1)
        {
            Buffs[] newArray = new Buffs[capacityTree * 2];
            Array.Copy(buffsArr, newArray, capacityTree);
            capacityTree *= 2;
            buffsArr = newArray;
        }

    }

   public void RemoveBuff(Buffs f) {
        if (counter == 0 || f == null)
            return;
        int resetFrom = buffsArr.Length;
        for (int i = 0; i < buffsArr.Length; i++)
        {
            if (buffsArr[i] == null)
                return;

            if (buffsArr[i] == f)
            {
                buffsArr[i] = null;
                resetFrom = i;
                counter--;
                SortArray(resetFrom);
                return;
            }
        }
        
}

    void SortArray(int resetFrom) {
        if (resetFrom == buffsArr.Length)
            return;


        for (int i = resetFrom; i < buffsArr.Length; i++)
        {
            if (i + 1 >= buffsArr.Length - 1)
                break;

            buffsArr[i] = buffsArr[i + 1];
        }
        if (buffsArr[buffsArr.Length - 1] == buffsArr[buffsArr.Length - 2])
            buffsArr[buffsArr.Length - 1] = null;

    }

    
    #endregion




    #region Regeneration Params
    const float healthRegenerationAmount = 1;
    const float staminaRegenerationAmount = 5;
   // List<Buffs> RegenerationBuffs ;

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

    public void RemoveBuffRegeneration(Buffs buff)
    {
        if (buff == null )
            return;

 


        RemoveBuff(buff);


    }
    public void AddBuffRegeneration(Buffs buff) {
        if (buff == null)
            return;

        AddInstantBuff(buff);

       
        AddBuff(buff);
    }


    public void AddInstantBuff(Buffs instantBuff) {
        if (instantBuff == null || instantBuff.GetIsOverTime == true)
            return;
        switch (instantBuff.GetRegenerationType)
        {
            case RegenerationType.Stamina:
                AddStaminaAmount(instantBuff.GetAmount);
                break;
            case RegenerationType.Health:
                Debug.Log("Instant");
                AddHealthAmount(instantBuff.GetAmount);
                break;
            default:
                break;
        }
    }
    IEnumerator Regeneration()
    {

        // check if some action happens
        if (!stopStaminaRegeneration)
            StaminaRegeneration();

        if (!stopHealthRegeneration)
            HealthRegeneration();
      
        yield return new WaitForSeconds(1f);
        CheckBuffTimers();
        StartCoroutine(Regeneration());
    }


    private void CheckBuffTimers() {
     

        if (counter < 1)
            return;

        for (int i = 0; i < buffsArr.Length; i++)
        {
            if (buffsArr[i] == null)
                return;

            if (buffsArr[i].CheckIfSupposedToContinue() == false) { 
                RemoveBuff(buffsArr[i]);
                i--;
            }
        }

    }
    private void StaminaRegeneration() {
        float totalAmount = staminaRegenerationAmount;


        Debug.Log("Stamina");

            if (buffsArr != null &&buffsArr.Length > 0 && counter > 0)
            {
                for (int i = 0; i < buffsArr.Length; i++)
                {
                    if (buffsArr[i] == null)
                        break;
                    if (buffsArr[i].GetRegenerationType == RegenerationType.Stamina && buffsArr[i].SetGetBuffActive)
                        totalAmount += buffsArr[i].GetAmount;
                }
            }
        

       // Debug.Log("!" +totalAmount);
        AddStaminaAmount(totalAmount);
    }
    private void HealthRegeneration() {
        float totalAmount = healthRegenerationAmount;
        Debug.Log("REgeneration");
        if (buffsArr == null)
            return;
        if (buffsArr.Length > 0 && counter > 0)
        {
            for (int i = 0; i < buffsArr.Length; i++)
            {
                if (buffsArr[i] == null)
                    break;
                if (buffsArr[i].GetRegenerationType == RegenerationType.Health && buffsArr[i].SetGetBuffActive) {
                   
                    totalAmount += buffsArr[i].GetAmount;
                } 
            
            }
        }

        AddHealthAmount(totalAmount);
    }

    #endregion


}
public enum RegenerationType { Stamina,Health }


public class Buffs {
    bool isActive;
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

        if (!isOverTime || endTime < Time.time)
            return false;
        return true ;

    }
    public bool SetGetBuffActive {
        set => isActive = value;
        get => isActive;
    }
    public float GetAmount => amount;
    public RegenerationType GetRegenerationType => typeOf;
    public bool GetIsOverTime => isOverTime;

}
