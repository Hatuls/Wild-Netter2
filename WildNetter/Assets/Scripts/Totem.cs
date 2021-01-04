using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
 
    // Script References:
    public TotemSO relevantSO;
    // Component References:
    // Variables:
    public Item[] itemsToBuildThis;
    public Collider[] enemiesInPrey;
    public Collider[] playerCollider;

    private bool pull;
    private bool isPlayer = false;
    private bool isBeast = false;
   


    public float durationTimer;
    public TotemType type;
    private Transform playerTransform;
    // Getter & Setters:
    public float GetCurrentTime()
    {
        float time = ((float)System.DateTime.Now.Hour * 3600) + ((float)System.DateTime.Now.Minute * 60) + (float)System.DateTime.Now.Second;
        return time;
    }

    IEnumerator ActivateTotemEffect(TotemType totemType) 
    {
        bool isActive = true;
        float timeToDestroy = relevantSO.duration + GetCurrentTime();
        while (isActive)
        {
            switch (totemType)
            {
                case TotemType.prey:
                    Prey();
                    break;
                case TotemType.healing:
                    Healing();
                    break;
                case TotemType.detection:
                    Detection();
                    break;
                default:
                    break;
            }

            if(GetCurrentTime() > timeToDestroy)
            {
                StopCoroutine(ActivateTotemEffect(totemType));
                break;
            }
            durationTimer = GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator TotemDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    public void Init(Vector3 location, TotemSO totemSO)
    {
        relevantSO = totemSO;
        gameObject.transform.position = location;
        Debug.Log(relevantSO.totemType);
        gameObject.SetActive(true);
        type = relevantSO.totemType;
        if(playerTransform == null && type == TotemType.healing)
        {
            playerTransform = PlayerManager.GetInstance.GetPlayerTransform;
        }
        if (relevantSO.duration != null || relevantSO.duration > 0)
        {
            StopCoroutine(TotemDuration(relevantSO.duration));
            StartCoroutine(TotemDuration(relevantSO.duration));
        }
        else 
        {
            //Active only in relevant zone
        }
        StopCoroutine(ActivateTotemEffect(relevantSO.totemType));
        StartCoroutine(ActivateTotemEffect(relevantSO.totemType));
    }

    public void Prey()
    {
        // Change later to distance vector from enemy spawnerManager
        enemiesInPrey = Physics.OverlapSphere(transform.position, 7, TotemManager._instance.enemiesLayer);
        foreach  (Collider col in enemiesInPrey)
        {
            Debug.Log(Vector3.Distance(transform.position, col.transform.parent.position));
            if(Vector3.Distance(transform.position,col.transform.parent.position) > 7 - 0.5f && !pull)
            {
                pull = true;
            }

            if(pull)
            {
                col.transform.parent.position = Vector3.MoveTowards(col.transform.parent.position, transform.position, 5 * Time.deltaTime);
                if (Vector3.Distance(transform.position, col.transform.parent.position) < 2)
                {
                    pull = false;
                }
            }
        }
    }

    public void Healing()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < relevantSO.range)
        {
            int healingPrecentage = 5;
            PlayerManager.GetInstance.GetPlayerStatsScript.GetSetCurrentHealth += (int)((healingPrecentage * PlayerManager.GetInstance.GetPlayerStatsScript.GetSetMaxHealth) / 100);
            Debug.Log("Healing Totem Effect:" + " " + PlayerManager.GetInstance.GetPlayerStatsScript.GetSetCurrentHealth);
        }
    }

    public void Detection()
    {
        enemiesInPrey = Physics.OverlapSphere(transform.position, 7, TotemManager._instance.enemiesLayer);
        foreach (Collider col in enemiesInPrey)
        {
            if (Vector3.Distance(transform.position, col.transform.parent.position) < 7)
            {
                Debug.Log("Beast was found!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 7);
    }
}
