
using UnityEngine;
using UnityEngine.UI;


public class TotemIconImageScript : MonoBehaviour
{
   [SerializeField] TotemName thisTotem;
 [SerializeField]  PlayPhase phaseToUnlock;
    public TotemName GetTotemImageSlot => thisTotem;
    [SerializeField] Sprite[] totemSprites;
    Image img;
  
     bool isDark = false;
    public bool GetIsDark => isDark;

    private void Start()
    {
        img = GetComponent<Image>();
    }
    void SetSprite(int i) {
        if(img!= null && totemSprites!= null)
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
