
using System.Collections;
using UnityEngine;


public class TotemOfStamina : TotemSO
{

    Buffs buff;
    bool isCurrentlyInRange;
    bool toStopCoroutine;

    public void AssignBuff() {
        toStopCoroutine = false;

        SetIsCurrenltlyInRange = true;
    }
    public void DisableBuff() {
        toStopCoroutine = true;
        isCurrentlyInRange = false;
        PlayerStats._Instance.RemoveBuffRegeneration(buff);
    }
    bool SetIsCurrenltlyInRange
    {
        set
        {
            if (isCurrentlyInRange == value)
                return;

            isCurrentlyInRange = value;

            if (isCurrentlyInRange)
                PlayerStats._Instance.AddBuffRegeneration(buff);
            else
                PlayerStats._Instance.RemoveBuffRegeneration(buff);
        }
    }

    public TotemOfStamina(string[] lootData, string[] totemData) : base(lootData, totemData)
    {
      buff = new Buffs(RegenerationType.Stamina,
          (this.effectAmountPrecentage.GetValueOrDefault() * PlayerStats._Instance.GetSetMaxStamina / 100)
          ,Mathf.Infinity );
    }

    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {
        if (CheckRange(totemLocation, targetLocation, range))
            SetIsCurrenltlyInRange = true;
        else
            SetIsCurrenltlyInRange = false;
    }
    public override IEnumerator ActivateTotemEffect(Transform targetPos, GameObject totem)
    {
      
        float timeToDestroy = duration + GetCurrentTime();
        while (GetCurrentTime() < timeToDestroy)
        {
            if (toStopCoroutine)
                break;
            this.DoEffect(totem.transform.position, targetPos.position);
            currentRealTime = GetCurrentTime();
            yield return new WaitForSeconds(1f);
        }
    }

    public override Buffs GetBuff() => buff;
    
}
