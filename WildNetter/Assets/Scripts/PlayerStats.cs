using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Varaibles:
    int strengh;
    int wisdom;
    int agility;
    int stamina;
    int currentEXP;
    int expToNextLevel;
    int abilityPoints;
    int currentHealth;
    int maxHealth;
    int armorPoints;

    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:
    public int GetSetStrengh {
        get { return strengh; }
        set { strengh = value; }
    } public int GetSetWisdom
    {
        get { return wisdom; }
        set { wisdom = value; }
    } public int GetSetAgility
    {
        get { return agility; }
        set { agility = value; }
    } public int GetSetStamina
    {
        get { return stamina; }
        set { stamina = value; }
    } public int GetSetCurrentEXP
    {
        get { return currentEXP; }
        set { currentEXP = value; }
    } public int GetSetExpToNextLevel
    {
        get { return expToNextLevel; }
        set { expToNextLevel = value; }
    } public int GetSetAbilityPoints
    {
        get { return abilityPoints; }
        set { abilityPoints = value; }
    } public int GetSetCurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    } public int GetSetMaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    } public int GetSetArmorPoints
    {
        get { return armorPoints; }
        set { armorPoints = value; }
   }
    //Functions:
    public void Init()
    {
        
    }

    public void ApplyDMG(int amount) { }
    //public int DMGAfterArmour(int amount) { return 1; }  < return the dmg after the armor protection 
    public void HealPlayer(int amount) { }

}
