using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Totem : MonoBehaviour
{
 
    // Script References:
    public TotemSO relevantSO;
    // Component References:
    // Variables:
    public float currentRealTime;
    public TotemName totemName;
    private Transform playerTransform;
    public VisualEffect healVFX;
    public VisualEffect preyVFX;
    public VisualEffect detectionVFX;
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
        totemName = relevantSO.totemName;
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
        switch (totemName)
        {
            case TotemName.prey:
                // preyVFX = transform.Find("PreyVFX").GetComponent<VisualEffect>();
                //preyVFX.gameObject.SetActive(true);
                preyVFX.Play();

                StopCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                break;

            case TotemName.healing:
                if (playerTransform == null)
                {
                    playerTransform = PlayerManager._Instance.GetPlayerTransform;
                }
                //  healVFX = transform.Find("HealVFX").GetComponent<VisualEffect>();
                // healVFX.gameObject.SetActive(true);
                healVFX.Play();

                StopCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                break;

            case TotemName.detection:
                // detectionVFX = transform.Find("DetectionVFX").GetComponent<VisualEffect>();
                //detectionVFX.gameObject.SetActive(true);
                detectionVFX.Play();

                StopCoroutine(relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                StartCoroutine(relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                break;

            case TotemName.stamina:
                if (playerTransform == null)
                {
                    playerTransform = PlayerManager.GetInstance.GetPlayerTransform;
                }
                StartCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                StopCoroutine(relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                break;

            case TotemName.shock:
                StartCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                StopCoroutine(relevantSO.ActivateTotemEffect(this.gameObject));
                break;

            default:
                break;
        }
    }
}
