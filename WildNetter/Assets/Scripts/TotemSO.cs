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
    public float currentRealTime;
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

    public virtual void DoEffect(Vector3 totemLocation, Vector3 targetLocation) { }
    public virtual void DoEffect(Vector3 totemLocation) { }
    public virtual void DoEffect(Vector3 totemLocation, GameObject totem) { }
    public bool CheckRange(Vector3 totemLocation, Vector3 targetLocation, float _range)
    {
        bool isRange = (Vector3.Distance(totemLocation, targetLocation) < _range);
        return isRange;
    }

    public float GetCurrentTime()
    {
        float time = ((float)System.DateTime.Now.Hour * 3600) + ((float)System.DateTime.Now.Minute * 60) + (float)System.DateTime.Now.Second;
        return time;
    }

   public virtual IEnumerator ActivateTotemEffect(GameObject totem) { yield return null; }

    public virtual IEnumerator ActivateTotemEffect(Transform targetPos, GameObject totem) { yield return null; }

    public virtual IEnumerator ActivateTotemEffect(bool toContinuteSpawning, GameObject totem) { yield return null; }



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

public class TotemOfDetection : TotemSO
{
    // ***** check if collider needs to be on the totem or in a sub class *****
    public TotemOfDetection(string[] lootData, string[] totemData) : base (lootData, totemData)
    {

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
            // give enemy spawner totem location and tell enemyspawner to spawn enemy in that location
            // notify player that totem detected an enemy
                return true;
        }

        else
        {
            return false;
        }
    }
    public override IEnumerator ActivateTotemEffect(bool toContinueSpawn ,GameObject totem)
    {
        float timeToDestroy = duration + GetCurrentTime();
        if (toContinueSpawn == true)
        {
            while (true)
            {
                this.DoEffect(totem.transform.position);
                if (GetCurrentTime() > timeToDestroy)
                {
                    break;
                }
                currentRealTime = GetCurrentTime();
                yield return new WaitForSeconds(1);
            }
        }

        else
        {
            this.DoEffect(totem.transform.position);
            yield return null;
        }
    }
}

public class TotemOfPrey : TotemSO
{
    private List<GameObject> enemyCatched = new List<GameObject>();
    public TotemOfPrey(string[] lootData, string[] totemData) : base(lootData, totemData)
    {

    }
    public override void DoEffect(Vector3 totemLocation, GameObject totem)
    {
        Collider[] objectCollider;
        objectCollider = Physics.OverlapSphere(totemLocation, range, TotemManager._instance.enemiesLayer);
        Debug.Log(objectCollider.Length);
        foreach (Collider col in objectCollider)
        {
            if (CheckRange(totemLocation, col.transform.position, range))
            {
                if(enemyCatched.Contains(col.gameObject))
                {
                    continue;
                }

                else
                {
                    enemyCatched.Add(col.gameObject);
                    col.GetComponent<Enemy>().TotemEffect(TotemType.prey, totem);
                }
            }


            /*if (pull)
            {
                //remove from here when enemy is done
                col.GetComponent<Enemy>().TotemEffect(TotemType.prey, totem);
                if (currentRealTime > GetCurrentTime())
                {
                    pull = false;
                    col.GetComponent<Enemy>().CancelEffect();
                }
            }*/
        }
    }
    public override IEnumerator ActivateTotemEffect(GameObject totem)
    {
        float timeToDestroy = duration + GetCurrentTime();
        while (true)
        {
            this.DoEffect(totem.transform.position, totem);
            if (GetCurrentTime() > timeToDestroy)
            {
                foreach(GameObject go in enemyCatched)
                {
                    go.GetComponent<Enemy>().CancelEffect();
                }
                break;
            }
            currentRealTime = GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }
}