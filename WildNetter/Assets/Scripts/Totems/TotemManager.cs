﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TotemName { prey, healing, detection, stamina, shock };
public enum TotemType { baiting, buff, debuff, detection, trapping };
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

    //bool
    public bool canPlace = false;
    public bool isDetection, isHealing, isPrey, isStamina, isShock;


    public List<Transform> ActiveTotem;
    public TotemSO[] AllGameTotems = new TotemSO[5];


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

    public void DeployAtLocation(Vector3 location, TotemName type)
    {
        CanPlaceATotem(type);
        if (canPlace)
        {
            Totem t = GetActiveTotem(location).GetComponent<Totem>();
            t.Init(location, GetRelevantTotemData(type));
            SetBoolToFalse(canPlace);
        }
        //range between totems
        //can't place the same totem twice
        //totems in combat
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

    public TotemSO GetRelevantTotemData(TotemName totemSO)
    {
        switch(totemSO)
        {
            case TotemName.detection:
                return AllGameTotems[0];
            case TotemName.healing:
                return AllGameTotems[1];
            case TotemName.prey:
                return AllGameTotems[2];
            case TotemName.stamina:
                return AllGameTotems[3];
            case TotemName.shock:
                return AllGameTotems[4];
            default:
                break;                
        }
        return null;
    }

    public void LoadTotemData()
    {
        int startOfTotemID = 40000;
        // need to fix it to how many totem types there is
        for (int i = 0; i < 5; i++)
        {
            AllGameTotems[i] = ItemFactory._Instance.GenerateItem(startOfTotemID + i) as TotemSO;
        }
    }

    private void CanPlaceATotem(TotemName type)
    {
        switch (type)
        {
            case TotemName.detection:
                if (!isDetection)
                {
                    canPlace = true;
                    isDetection = true;
                }
                
                break;

            case TotemName.healing:
                if (!isHealing)
                {
                    isHealing = true;
                    canPlace = true;
                }
                break;

            case TotemName.prey:
                if (!isPrey)
                {
                    isPrey = true;
                    canPlace = true;
                }
                break;

            case TotemName.stamina:
                if (!isStamina)
                {
                    isStamina = true;
                    canPlace = true;
                }
                break;

            case TotemName.shock:
                if (!isShock)
                {
                    isShock = true;
                    canPlace = true;
                }
                break;
            default:
                break;
        }
    }

    public bool SetBoolToFalse(bool toChange)
    {
        toChange = false;
        return toChange;
    }
}
