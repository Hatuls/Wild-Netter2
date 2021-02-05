using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemOfHealing : TotemSO
{
    public TotemOfHealing(string[] lootData, string[] totemData) : base(lootData, totemData)
    {

    }

    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {
        if (CheckRange(totemLocation, targetLocation, range))
        {
            PlayerStats._Instance.AddHealthAmount((int)((this.effectAmountPrecentage * PlayerStats._Instance.GetSetMaxHealth) / 100));
            Debug.Log("Healing Totem Effect:" + " " + PlayerStats._Instance.GetSetCurrentHealth);
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
