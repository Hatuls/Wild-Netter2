using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Stats playerStats;

    //Component References:
    //Script References:
    // Collections:
    //Getters & Setters:
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
    public int GetSetStamina
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

            playerStats.currentHealth = value;

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
    //Functions:
    public void Init()
    {

    }

    public void ApplyDMG(int amount) { }
    //public int DMGAfterArmour(int amount) { return 1; }  < return the dmg after the armor protection 
    public void HealPlayer(int amount) { }

}