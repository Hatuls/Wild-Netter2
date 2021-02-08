using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TotemName {None, prey, healing, detection, stamina, shock };
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

    public bool TryDeployAtLocation(Vector3 location, TotemName type)
    {
        if (!CheckPlayPhaseForTotem(type))
            return false;


          TotemSO totemCache =GetRelevantTotemData(type);
        if (!CheckTotemPlacementLocation(location, totemCache))
            return false;


            Totem t = GetActiveTotem(location).GetComponent<Totem>();
            t.Init(location, totemCache);
        return true;
    }

    bool CheckTotemPlacementLocation(Vector3 position, TotemSO ttm) {
        bool canLocate= true;
        float minimumCheckRange = 40f;

        if (ActiveTotem.Count == 0)
            return true;
        
        for (int i = 0; i < ActiveTotem.Count; i++)
        {
            if (!ActiveTotem[i].gameObject.activeSelf||Vector3.Distance(position, ActiveTotem[i].position) > minimumCheckRange)
                continue;

            canLocate &= CheckRangeBetweenTotem(position, ttm, ActiveTotem[i].position, ActiveTotem[i].GetComponent<Totem>().relevantSO);
            if (!canLocate)
                return false;
        }

        return canLocate;
    }
    bool CheckRangeBetweenTotem(Vector3 placePosition, TotemSO fromPlayer , Vector3 totemPosition , TotemSO fromList) {

        return Vector3.Distance(placePosition,totemPosition) >  (fromPlayer.range  + fromList.range);  
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
        if (totemSO == TotemName.None)
            return null;

        Debug.Log(totemSO);
        switch (totemSO)
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




    private bool CheckPlayPhaseForTotem(TotemName totemName) {

        if (totemName == TotemName.None)
            return false;

        PlayPhase phase = SceneHandler._Instance.GetSetPlayPhase;

        if (phase == PlayPhase.BattlePhase)
        {
            switch (totemName)
            {
                case TotemName.healing:
                case TotemName.stamina:
                case TotemName.shock:
                    return true;
            }  
        }
        else if (phase == PlayPhase.PlanningPhase)
        {
            switch (totemName)
            {
                case TotemName.prey:
                case TotemName.detection:
                    return true;
            }
        }

        return false;
    }
}
