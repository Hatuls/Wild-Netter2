using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Totem", fileName = "Totem Name")]
public class TotemSO : Item
{
    // Object References:
    public Item[] itemsToBuildThis;
    // Variables:

    public TotemType totemType;
    public float duration;
    public float range;
    public int MinimumPlayerLevel;
    public int currentZone;
   // public int amount = 0;
    public TotemSO(string[] lootData, string[] totemData) : base(lootData)
    {
       
        if (totemData == null)
            return;
        if (totemData[1] != "")
        {
            int intTotemType;
            if (int.TryParse(totemData[1], out intTotemType))
            {
                switch (intTotemType)
                {
                    case 1:
                        totemType = TotemType.detection;
                        break;
                    case 2:
                        totemType = TotemType.healing;
                        break;
                    case 3:
                        totemType = TotemType.prey;
                        break;
                    default:
                        break;
                }
            }
        }
        if (totemData[2] != "")
        {
            string[] durationString = totemData[2].Split(new char[] { '-' });
            int durationType = 0;
            int.TryParse(durationString[0], out durationType);
            float.TryParse(durationString[1], out duration);   
            if(durationType == 2)
            {
                int.TryParse(durationString[1], out currentZone);
            }
        }
        if (totemData[3] != "")
        {
            float.TryParse(totemData[3], out range);
        }
        if (totemData[4] != "")
        {
            int.TryParse(totemData[4], out MinimumPlayerLevel);
        }
    }

}
