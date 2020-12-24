using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    // Script References:
    // Component References:
    // Variables:
    // Getter & Setters:
    public static TotemManager _instance;

    enum TotemType { };
    public Totem[] ActiveTotem;

    private void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        
    }
    public void ActivateTotemEffect() { }

}
