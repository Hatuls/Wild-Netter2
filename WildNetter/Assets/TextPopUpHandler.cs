using UnityEngine;

public class TextPopUpHandler : MonoSingleton<TextPopUpHandler>
{

    [SerializeField] private Transform TextPF;
    public override void Init()
    {
    }

    public Transform GetTextPF => TextPF;
}
