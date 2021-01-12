using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
 
    // Script References:
    public TotemSO relevantSO;
    // Component References:
    // Variables:
    public float currentRealTime;
    public TotemType type;
    private Transform playerTransform;
    public ParticleSystem healingParticle;
    //public ParticleSystem detectionParticle;
    public ParticleSystem preyParticle;

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
                    relevantSO.DoEffect(transform.position);
                    break;
                case TotemType.healing:
                    relevantSO.DoEffect(transform.position);
                    break;
                case TotemType.detection:
                    relevantSO.DoEffect(transform.position);
                    //detectionParticle.Play();
                    break;
                default:
                    break;
            }

            if(GetCurrentTime() > timeToDestroy)
            {
                StopCoroutine(ActivateTotemEffect(totemType));
                break;
            }
            currentRealTime = GetCurrentTime();
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
        gameObject.transform.GetChild(0).GetComponent<Transform>().localScale = new Vector3(relevantSO.range/2, relevantSO.range/2, 1);
        gameObject.SetActive(true);
        //detectionParticle = GetComponent<ParticleSystem>();
        type = relevantSO.totemType;
        if(playerTransform == null && type == TotemType.healing)
        {
            playerTransform = PlayerManager.GetInstance.GetPlayerTransform;
            healingParticle = this.transform.Find("HealingParticle").GetComponent<ParticleSystem>();
            healingParticle.Play();
        }
        if (type == TotemType.prey)
        {
            preyParticle = this.transform.Find("PreyParticle").GetComponent<ParticleSystem>();
            preyParticle.Play();
        }
        if (relevantSO.duration > 0)
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
}
