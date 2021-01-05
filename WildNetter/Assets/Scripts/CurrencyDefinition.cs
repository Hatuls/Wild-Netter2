
using UnityEngine;

    public enum CurrencyType { Copper,Silver,Gold};
public class CurrencyDefinition
{
    public CurrencyType currency = CurrencyType.Copper;
    public int Price = 0;


    public void SetCurrencyType(int type)
    {

        switch (type)
        {
            case 1:
                currency = CurrencyType.Copper;
                break;
            case 2:
                currency = CurrencyType.Silver;
                break;
            case 3:
                currency = CurrencyType.Gold;
                break;
            default:
                Debug.Log("Didnt Find The Right Type Of Currency Check Data Table");
                break;

        }

    }
}
