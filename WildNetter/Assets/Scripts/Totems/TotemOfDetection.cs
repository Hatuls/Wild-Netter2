using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemOfDetection : TotemSO
{
    // ***** check if collider needs to be on the totem or in a sub class *****
    public TotemOfDetection(string[] lootData, string[] totemData) : base(lootData, totemData)
    {

    }
    public override void DoEffect(Vector3 totemLocation)
    {
        if (SpawnDetectedEnemy(totemLocation) == true)
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
    public bool SpawnDetectedEnemy(Vector3 spawnPoint)
    {
        //we will set how much time the monster take to spawn
        float randomTime = Random.Range(1, 10);
        EnemyManager _enemyManager = EnemyManager.GetInstance();
        if (Random.value > 0.5)
        {
            _enemyManager.GetBeastSettings((Difficulty)Random.Range(0, 2), (Size)Random.Range(0, 2), Random.Range(1, 100), spawnPoint, randomTime);
            Debug.Log(randomTime + "seconds to spawn");
            // notify player that totem detected an enemy
            return true;
        }

        else
        {
            return false;
        }
    }
    public override IEnumerator ActivateTotemEffect(bool toContinueSpawn, GameObject totem)
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
