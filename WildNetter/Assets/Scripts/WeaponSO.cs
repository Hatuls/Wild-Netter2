
using UnityEngine;

public class WeaponSO :ItemData
{
    public ItemStruct Data;
    public Vulnerability vulnerabilityActivator;
    public int MinimumPlayerLevel;
    public int minDMG, maxDMG;
    public int Hands;
    public float CritChance;
    public float HitSpeed;
    public int[] ItemsIDToBuildThis;
    public enum WeaponType { Melee, Range };
    
    public WeaponType weaponType;



    // the 6 first elements of the dataFiles go to item!
    public WeaponSO(string[] itemData, string[] weaponData): base(itemData)
    {

        if (weaponData != null)
        {
            // min and max attack dmg
            if (weaponData[1] != "" && weaponData[1] != null)
            {
                string[] dmgCache = weaponData[1].Split(new char[] { '-' });
                int.TryParse(dmgCache[0],out minDMG);
                int.TryParse(dmgCache[1],out maxDMG);
            }

            //set attack speed
            if (weaponData[2] != "" && weaponData[2] != null)
            {
                float.TryParse(weaponData[2], out HitSpeed);
            }

            //set weapon type range or melee
            if (weaponData[3] != ""  && weaponData[3] != null)
            {
                if (weaponData[3] == "1")
                {
                    weaponType = WeaponType.Melee;
                }
                else
                {
                    weaponType = WeaponType.Range;
                }
            }

            // set hands
            if (weaponData[4] != "" && weaponData[4] != null) {

                int.TryParse(weaponData[4], out Hands);
            
            }
            // set minimum level
            if (weaponData[5] != "" && weaponData[5] != null)
            {
                int.TryParse(weaponData[5], out MinimumPlayerLevel);
            }
            // set bonus?
            if (weaponData[6] != "" && weaponData[6] != null)
            {
               //  to be done when its filled
            }

            //    ItemsIDToBuildThis
            if (weaponData[7] != "" && weaponData[7] != null)
            {
            //    ItemsIDToBuildThis
            }
            // remarks
            if (weaponData[8] != "" && weaponData[8] != null)
            {
           // remarks
            }
            if (weaponData[9] != "" && weaponData[9] != null)
            {
                float.TryParse(weaponData[9], out CritChance);
            }
        }


      
    }


    public void PrintWeaponSO() {

        Debug.Log("MinimumPlayerLevel : " + this.MinimumPlayerLevel
            + "\n minDMG : " + this.minDMG
            + "\n maxDMG : " + this.maxDMG
            + "\n Hands : " + this.Hands
            + "\n CritChance : " + this.CritChance
            + "\n HitSpeed : " + this.HitSpeed
            + "\n weaponType : " + this.weaponType);
}



}
