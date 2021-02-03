using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotemType { prey, healing, detection };
public class TotemManager : MonoSingleton<TotemManager>
{
    // Script References:
    // Component References:
    // Variables:
    // Getter & Setters:

    public Vector3 totemOffset;
    public GameObject totem1;
    public LayerMask enemiesLayer;
    public Transform totemContainer;


    public List<Transform> ActiveTotem;
    public TotemSO[] AllGameTotems = new TotemSO[3];


    public override void Init()
    {
        //Debug.Log(System.DateTime.Now);
        LoadTotemData();
        for (int i = 0; i < totemContainer.childCount; i++)
        {
            ActiveTotem.Add(totemContainer.GetChild(i));
        }  
    }
    //public void ActivateTotemEffect() { }

    public void DeployAtLocation(Vector3 location, TotemType type)
    {
        Totem t = GetActiveTotem(location).GetComponent<Totem>();
        t.Init(location, GetRelevantTotemData(type));
    }

    public GameObject GetActiveTotem(Vector3 location)
    {
        foreach (Transform tran in ActiveTotem)
        {
            if(!tran.gameObject.activeSelf)
            {
                return tran.gameObject;
            }
        }

        GameObject go = Instantiate(totem1, location,Quaternion.identity, totemContainer);
        return go;
    }

    public TotemSO GetRelevantTotemData(TotemType totemSO)
    {
        switch(totemSO)
        {
            case TotemType.detection:
                return AllGameTotems[0];
            case TotemType.healing:
                return AllGameTotems[1];
            case TotemType.prey:
                return AllGameTotems[2];
            default:
                break;                
        }
        return null;
    }

    public void LoadTotemData()
    {
        int startOfTotemID = 40000;
        // need to fix it to how many totem types there is
        for (int i = 0; i < 3; i++)
        {
            AllGameTotems[i] = ItemFactory._Instance.GenerateItem(startOfTotemID + i) as TotemSO;
        }
    }
}
