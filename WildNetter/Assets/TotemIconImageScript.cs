
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;


public class TotemIconImageScript : Slot
{
   [SerializeField] TotemName thisTotem;
 [SerializeField]  PlayPhase phaseToUnlock;
    public TotemName GetTotemImageSlot => thisTotem;
    [SerializeField] Sprite[] totemSprites;


     bool isDark = false;
    public bool GetIsDark => isDark;

   
    void SetSprite(int i) {
        if(this.img != null && totemSprites!= null)
        img.sprite = totemSprites[i];
        {

        if (i == 0)
            isDark = true;
        else
            isDark = false;
        }
    }

    public void SetMySprite(PlayPhase currentPhase)
    {

        if (currentPhase == phaseToUnlock || currentPhase == PlayPhase.TotemCheckPhase)
           SetSprite(1);
        
        else
            SetSprite(0);
        
    }
    public void HighLightImage(bool toHighLight)
    {
        if (isDark)
            SetSprite(0);
        else if
            (toHighLight)
            SetSprite(2);
        else
            SetSprite(1);

    }



}
public abstract class Slot : MonoBehaviour{
   public Image img;
    public int GetSlotID;
     Button btn;
    public Image insideIMG;
    private void Awake()
    {
        //img = GetComponent<Image>();
        //TryGetComponent<Button>(out btn);
        //insideImg= transform.GetChild(0).GetComponent<Image>();
          img = GetComponent<Image>();
        TryGetComponent<Button>(out btn);
        insideIMG = this.transform.GetChild(0).GetComponent<Image>();

    }

    public Button GetBtn => btn;
  
}