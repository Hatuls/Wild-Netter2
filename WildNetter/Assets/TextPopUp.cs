
using TMPro;
using UnityEngine;
public enum TextType { NormalDMG, CritDMG, Healing, Money }
public class TextPopUp : MonoBehaviour
{
    public static TextPopUp Create(TextType type , Vector3 Position,int Amount) {
        Transform textPopUpTransform = Instantiate(TextPopUpHandler._Instance.GetTextPF,Position,Quaternion.identity,TextPopUpHandler._Instance.transform);

        TextPopUp textPopUp = textPopUpTransform.GetComponent<TextPopUp>();
        textPopUp.Setup(type, Amount);

        return textPopUp;
    }
    private float disappearTimer;
    TextMeshPro textMesh;
    private Color color;
    static int sortingOrder = 0;
    float moveYSpeed= 2f;
    float disappearSpeed = 3f;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Setup(TextType type, int amount) {
        textMesh.sortingOrder = sortingOrder;
        sortingOrder++;
        SetTextColor(type);
        textMesh.SetText(amount.ToString());
        disappearTimer = 1f;
    }

    
    private void Update()
    {
        transform.position += (Vector3.up * moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {

            color.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = color;

            if (color.a < 0)
                Destroy(gameObject);
            
        }


    }

    void SetTextColor(TextType type) { 
        Color colorToSet;
        switch (type)
        {
            case TextType.NormalDMG:
                ColorUtility.TryParseHtmlString("#FF7200", out color);
               
           
                break;
            case TextType.CritDMG:
                ColorUtility.TryParseHtmlString("#FF2D00", out color);
                 
                break;
            case TextType.Healing:
                ColorUtility.TryParseHtmlString("#57FF00", out color);
                  
                break;
            case TextType.Money:
                ColorUtility.TryParseHtmlString("#FFC600", out color);
                  
                break;
            default:
                textMesh.faceColor = Color.black;
                break;
        }
        textMesh.faceColor = color;

    }
}
