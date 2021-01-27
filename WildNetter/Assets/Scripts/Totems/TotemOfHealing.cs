using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemOfHealing : TotemSO
{
    int healingPrecentage = 5;

    public TotemOfHealing(string[] lootData, string[] totemData) : base(lootData, totemData)
    {
        //healingPrecentage = goes up per level
    }

    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {
        if (CheckRange(totemLocation, targetLocation, range))
        {
            PlayerManager.GetInstance.GetPlayerStatsScript.GetSetCurrentHealth += (int)((healingPrecentage * PlayerManager.GetInstance.GetPlayerStatsScript.GetSetMaxHealth) / 100);
            Debug.Log("Healing Totem Effect:" + " " + PlayerManager.GetInstance.GetPlayerStatsScript.GetSetCurrentHealth);
        }
    }
    public override IEnumerator ActivateTotemEffect(Transform targetPos, GameObject totem)
    {
        float timeToDestroy = duration + GetCurrentTime();
        while (true)
        {
            this.DoEffect(totem.transform.position, targetPos.position);
            if (GetCurrentTime() > timeToDestroy)
            {
                break;
            }
            currentRealTime = GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }
}
