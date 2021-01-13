using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TotemSO : Item
{
    // Object References:
    public Item[] itemsToBuildThis;
    // Variables:

    public TotemType totemType;
    public float duration;
    public float range;
    public int MinimumPlayerLevel;
    public int currentZone;
    public Animator _animator;

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

    public abstract void DoEffect(Vector3 totemLocation, Vector3 targetLocation);
    public abstract void DoEffect(Vector3 totemLocation);
    public bool CheckRange(Vector3 totemLocation, Vector3 targetLocation, float _range)
    {
        bool isRange = (Vector3.Distance(totemLocation, targetLocation) < _range);
        return isRange;
    }

    public virtual void PlayAnimation(string animName)
    {
        _animator.Play(animName);
    }
}

public class TotemOfHealing : TotemSO
{
    int healingPrecentage = 5;

    public TotemOfHealing(string[] lootData, string[] totemData) : base (lootData, totemData)
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

    public override void DoEffect(Vector3 totemLocation)
    {
        //throw new System.NotImplementedException();
    }
}

public class TotemOfDetection : TotemSO
{
    // ***** check if collider needs to be on the totem or in a sub class *****
    public TotemOfDetection(string[] lootData, string[] totemData) : base (lootData, totemData)
    {

    }
    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {
        throw new System.NotImplementedException();
    }

    public override void DoEffect(Vector3 totemLocation)
    {
        if (SpawnDetectedEnemy() == true)
        {
            Collider[] objectCollider;
            objectCollider = Physics.OverlapSphere(totemLocation, range, TotemManager._instance.enemiesLayer);
            foreach (Collider col in objectCollider)
            {
                if (CheckRange(totemLocation, col.transform.position, range))
                {
                    Debug.Log("Beast was found!");
                }
            }
        }
    }

    public bool SpawnDetectedEnemy()
    {
        EnemyManager _enemyManager = EnemyManager.GetInstance();
        if (Random.value > 0.5)
        {
            _enemyManager.GetBeastSettings((Difficulty)Random.Range(0,2), (Size)Random.Range(0,2), Random.Range(1,100));
                return true;
        }

        else
        {
            return false;
        }
    }
}

public class TotemOfPrey : TotemSO
{
    private bool pull;
    public TotemOfPrey(string[] lootData, string[] totemData) : base(lootData, totemData)
    {

    }
    public override void DoEffect(Vector3 totemLocation, Vector3 targetLocation)
    {
        throw new System.NotImplementedException();
    }

    public override void DoEffect(Vector3 totemLocation)
    {
        Collider[] objectCollider;
        objectCollider = Physics.OverlapSphere(totemLocation, range, TotemManager._instance.enemiesLayer);
        foreach (Collider col in objectCollider)
        {
            if (CheckRange(totemLocation, col.transform.position, range) && !pull)
            {
                pull = true;
            }

            if (pull)
            {
               
                if (CheckRange(totemLocation, col.transform.position, range / 3))
                {
                    pull = false;
                }
            }
        }
    }
}