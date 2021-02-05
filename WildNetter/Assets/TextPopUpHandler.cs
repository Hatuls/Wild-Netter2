using UnityEngine;

public class TextPopUpHandler : MonoSingleton<TextPopUpHandler>
{
    private static TextPopUpHandler _instance;
    [SerializeField] private Transform TextPF;
    public override void Init()
    {
        _instance = this as TextPopUpHandler;
    }
    public static TextPopUpHandler GetInstance => _instance;
    public Transform GetTextPF => TextPF;
}
