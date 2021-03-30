using System.Collections;

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
   
    public VisualEffect VfxPref;
    public bool continueSpawning;
 
    // Getter & Setters:
    Transform rangeField;
    int totemID;
    public int GetMyTotemID {
        get => totemID;
    
    }
    IEnumerator TotemDuration(float duration)
    {
        yield return new WaitForSeconds(duration);        //StopAllCoroutines();
        //relevantSO.StopCoroutines();
        TotemManager._Instance.RemoveTotem(totemID);
    }
    void NeedToAssignComponents() {

        if (!rangeField )
            rangeField = gameObject.transform.GetChild(0).GetComponent<Transform>();

    }

    public static Totem DeployTotem(int totemID ,Vector2 location, TotemSO totemSO, GameObject GO,Transform playerTrans) {
        Debug.Log("Deploying at: " + location);
        var trnsfrm = Instantiate(GO, new Vector3(location.x,location.y,0), GO.transform.rotation, TotemManager._Instance.TotemContainer);
        var totem = trnsfrm.GetComponent<Totem>();
       
        //Debug.Log(totem.transform.position);
        totem.Init(totemID, totemSO, playerTrans);
        return totem; 
     }



    public void Init(int id, TotemSO totemSO,Transform playertrans)
    {
        NeedToAssignComponents();
       

        relevantSO = totemSO;
        totemID = id;
      
        rangeField.localScale = new Vector2(relevantSO.range / 2, relevantSO.range / 2);
        gameObject.SetActive(true);
        totemName = relevantSO.totemName;
        if (relevantSO.duration > 0)
        {
            StopCoroutine(TotemDuration(this.relevantSO.duration));
            StartCoroutine(TotemDuration(this.relevantSO.duration));
        }
        else
        {
            //Active only in relevant zone
        }

        ApplyingEffect(playertrans);
    }
    public Buffs GetBuff => relevantSO.GetBuff();

    
    
    private void ApplyingEffect (Transform playerTrans)
    {
        switch (totemName)
        {
            case TotemName.prey:
                // preyVFX = transform.Find("PreyVFX").GetComponent<VisualEffect>();
                //preyVFX.gameObject.SetActive(true);
              //  VfxPref.Play();

                StopCoroutine(this.relevantSO.ActivateTotemEffect(this.gameObject));
                StartCoroutine(this.relevantSO.ActivateTotemEffect(this.gameObject));
                break;

            case TotemName.healing:
                if (playerTransform == null)
                    playerTransform = playerTrans;

                //healVFX = transform.Find("HealVFX").GetComponent<VisualEffect>();
                //healVFX.gameObject.SetActive(true);
                VfxPref.Play();

                StopCoroutine(this.relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                StartCoroutine(this.relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                break;

            case TotemName.detection:
                // detectionVFX = transform.Find("DetectionVFX").GetComponent<VisualEffect>();
                //detectionVFX.gameObject.SetActive(true);
                VfxPref.Play();

                StopCoroutine(this.relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                StartCoroutine(this.relevantSO.ActivateTotemEffect(continueSpawning, this.gameObject));
                break;

            case TotemName.stamina:
                //if (playerTransform == null)
                //    playerTransform = PlayerManager._Instance.GetPlayerTransform;
              //  VfxPref.Play();
                StopCoroutine(this.relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                StartCoroutine(this.relevantSO.ActivateTotemEffect(playerTransform, this.gameObject));
                break;

            case TotemName.shock:
                VfxPref.Play();
                StopCoroutine(this.relevantSO.ActivateTotemEffect(this.gameObject));
                StartCoroutine(this.relevantSO.ActivateTotemEffect(this.gameObject));
                Debug.Log("Activate Shock");
                break;

            default:
                break;
        }
        
    }
   public void UnsubscibeBuffs() {

        switch (totemName)
        {            case TotemName.healing:
                (this.relevantSO as TotemOfHealing).DisableBuff();
                break;
            case TotemName.stamina:
                (this.relevantSO as TotemOfStamina).DisableBuff();
                break;
        }


        Destroy(this.gameObject);
    }
}
