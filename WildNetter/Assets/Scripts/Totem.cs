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
    public ParticleSystem preyParticle;
    public ParticleSystem detectionParticle;
    public bool continueSpawning;

    // Getter & Setters:
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
        type = relevantSO.totemType;
        if (relevantSO.duration > 0)
        {
            StopCoroutine(TotemDuration(relevantSO.duration));
            StartCoroutine(TotemDuration(relevantSO.duration));
        }
        else 
        {
            //Active only in relevant zone
        }

        ApplyingEffect();
    }

    private void ApplyingEffect ()
    {
        switch (type)
        {
            case TotemType.prey:
                preyParticle = transform.Find("PreyParticle").GetComponent<ParticleSystem>();
                preyParticle.Play();
                StopCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                break;

            case TotemType.healing:
                if (playerTransform == null)
                {
                    playerTransform = PlayerManager.GetInstance.GetPlayerTransform;
                }
                healingParticle = transform.Find("HealingParticle").GetComponent<ParticleSystem>();
                healingParticle.Play();
                StopCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                break;

            case TotemType.detection:
                detectionParticle = transform.Find("DetectionParticle").GetComponent<ParticleSystem>();
                detectionParticle.Play();
                StopCoroutine(relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                break;

            default:
                break;
        }
    }
}
