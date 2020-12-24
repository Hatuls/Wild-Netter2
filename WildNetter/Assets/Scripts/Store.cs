using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Script References:
    public static Store _instance;
    // Component References:
    // Variables:
    public string openSentence;
    // Getter & Setters:
    // Collections:
    List<Item> itemsToSell;

    private void Awake()
    {
        _instance = this;
    }
    public void Init() { }
    public void PlayerSellObject() { }
    // public bool CheckIfHasMoney() { } <- will be used later
    // public bool PlayerBuyObject() { } <- will be used later

}
