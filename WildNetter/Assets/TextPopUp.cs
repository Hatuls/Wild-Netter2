
using TMPro;
using UnityEngine;
public enum TextType { NormalDMG, CritDMG, Health, Money }
public class TextPopUp : MonoBehaviour
{
    public static TextPopUp Create(TextType type , Vector3 Position,int Amount) {
        Transform textPopUpTransform = Instantiate(TextPopUpHandler._Instance.GetTextPF,Position,Quaternion.identity,TextPopUpHandler._Instance.transform);

        TextPopUp textPopUp = textPopUpTransform.GetComponent<TextPopUp>();
        textPopUp.Setup(type, Amount);

        return textPopUp;
    }

    TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Setup(TextType type, int amount) {

        SetTextColor(type);


        textMesh.SetText(amount.ToString());
    }

    void SetTextColor(TextType type) { 
        Color colorToSet;
        switch (type)
        {
            case TextType.NormalDMG:
                if (ColorUtility.TryParseHtmlString("#FF7200", out colorToSet))
               
                   textMesh.faceColor= colorToSet;
                break;
            case TextType.CritDMG:
                if (ColorUtility.TryParseHtmlString("#FF2D00", out colorToSet)) 
                    textMesh.faceColor = colorToSet;
                break;
            case TextType.Health:
                  if (ColorUtility.TryParseHtmlString("#57FF00", out colorToSet))
                    textMesh.faceColor = colorToSet;
                break;
            case TextType.Money:
                if (ColorUtility.TryParseHtmlString("#FFC600", out colorToSet))
                    textMesh.faceColor = colorToSet;
                break;
            default:
                textMesh.faceColor = Color.black;
                break;
        }

    }
}
