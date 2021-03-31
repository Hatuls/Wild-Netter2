using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemOfHealing : TotemSO
{
    private List<GameObject> playerHealed = new List<GameObject>();
    Buffs buff;
    bool isCurrentlyInRange;
    bool toBreakOut;
    public void AssignBuff() {
        toBreakOut = false;
        SetIsCurrenltlyInRange = true;
    }
    public void DisableBuff() {
        toBreakOut = true;
       this.isCurrentlyInRange = false;
        PlayerStats._Instance.RemoveBuffRegeneration(this.buff); }
   bool SetIsCurrenltlyInRange {
        set {
            if (this.isCurrentlyInRange == value)
                return;

            this.isCurrentlyInRange = value;

            if (this.isCurrentlyInRange)
            {
                PlayerStats._Instance.AddBuffRegeneration(this.buff);
                buff.SetGetBuffActive = true;
            }
            else {
                buff.SetGetBuffActive = false;
                PlayerStats._Instance.RemoveBuffRegeneration(this.buff);
            }
        }    
    
    
    }
    public TotemOfHealing(string[] lootData, string[] totemData) : base(lootData, totemData)
    {
        buff = new Buffs
            (RegenerationType.Health ,
            (this.effectAmountPrecentage.GetValueOrDefault() * PlayerStats._Instance.GetSetMaxHealth) / 100f,
            Mathf.Infinity);
    }

    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {

        if (CheckRange(totemLocation, targetLocation, range))
        {
            this.SetIsCurrenltlyInRange = true;
            



            Debug.Log("Heal Effect: " + true);
        }
        else
        {
            this.SetIsCurrenltlyInRange = false;
            Debug.Log("Heal Effect: " + false);
        }

        
    }
    public override IEnumerator ActivateTotemEffect(Transform targetPos, GameObject totem)
    {
       
        float timeToDestroy = duration + GetCurrentTime();
        while (GetCurrentTime() < timeToDestroy)
        {

            this.DoEffect(totem.transform.position, targetPos.position);
             
            if (toBreakOut)
                break;
            
            currentRealTime = GetCurrentTime();
            yield return new WaitForSeconds(.1f);
        }
    }
    public override Buffs GetBuff() => buff;
}
