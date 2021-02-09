using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TotemName {None, prey, healing, detection, stamina, shock };
public enum TotemType { baiting, buff, debuff, detection, trapping };
public class TotemManager : MonoSingleton<TotemManager>
{
    // Script References:
    // Component References:
    // Variables:
    // Getter & Setters:

    public Vector3 totemOffset;


    public  GameObject totem1;
    public LayerMask enemiesLayer;
    public  Transform TotemContainer;
    public Mesh[] totemMeshArr;
    
   // public List<Transform> ActiveTotem;
    public Dictionary<int, Totem> currentGameTotemDict;
    public Dictionary<int, Dictionary<int, Totem>> ActiveTotemsInSceneDict = new Dictionary<int, Dictionary<int, Totem>>();
    int totemID;
    public override void Init()
    {
        currentGameTotemDict = new Dictionary<int, Totem>();
        totemID = 0;
        //Debug.Log(System.DateTime.Now);
         
    }
 

  
    public Dictionary<int, Totem> SetDictionaryForScene
    {
        set {
            if (currentGameTotemDict != value)
                currentGameTotemDict = value;
        }
    }

    public void AssignCurrentTotemDictToScene(int sceneID)
    {
        if (ActiveTotemsInSceneDict.ContainsKey(sceneID))
        {
            SetDictionaryForScene = ActiveTotemsInSceneDict[sceneID];
            return;
        }
        ActiveTotemsInSceneDict.Add(sceneID, new Dictionary<int, Totem>());
        SetDictionaryForScene = ActiveTotemsInSceneDict[sceneID];
    }

    public bool TryDeployAtLocation(Vector3 location, TotemName type)
    {
        if (!CheckPlayPhaseForTotem(type))
            return false;


          TotemSO totemCache =GetRelevantTotemData(type);
        if (!CheckTotemPlacementLocation(location, totemCache))
            return false;
        totemID++;
    var ttm =    Totem.DeployTotem(totemID,location, totemCache, AssignMeshToTotem(type));
        currentGameTotemDict.Add(totemID, ttm);

            //Totem t = GetActiveTotem(location).GetComponent<Totem>();
            //t.Init(location, totemCache , AssignMeshToTotem(type));
        return true;
    }

    public void RemoveTotem(int totemID)
    {
        if (currentGameTotemDict.ContainsKey(totemID)) {
            PlayerStats._Instance.RemoveBuff(currentGameTotemDict[totemID].GetBuff);
            currentGameTotemDict[totemID].UnsubscibeBuffs();
            currentGameTotemDict.Remove(totemID);
        
        }
    }


    Mesh AssignMeshToTotem(TotemName ttmName) {

        switch (ttmName)
        {
            
            case TotemName.prey:
                return totemMeshArr[0];
            case TotemName.healing:
                return totemMeshArr[1];
            case TotemName.detection:
                return totemMeshArr[2];
            case TotemName.stamina:
                return totemMeshArr[3];
            case TotemName.shock:
                return totemMeshArr[1];
         
        }

        return null;
    }

    bool CheckTotemPlacementLocation(Vector3 position, TotemSO ttm) {
        bool canLocate= true;
        float minimumCheckRange = 40f;

        if (currentGameTotemDict.Count == 0)
            return true;

        foreach (var item in currentGameTotemDict)
        {
     
            if (!item.Value.gameObject.activeSelf||Vector3.Distance(position, item.Value.transform.position) > minimumCheckRange)
                continue;

            canLocate &= CheckRangeBetweenTotem(position, ttm, item.Value.transform.position, item.Value.relevantSO);
            if (!canLocate)
                return false;
        }

        return canLocate;
    }
    bool CheckRangeBetweenTotem(Vector3 placePosition, TotemSO fromPlayer , Vector3 totemPosition , TotemSO fromList) {

        return Vector3.Distance(placePosition,totemPosition) >  (fromPlayer.range  + fromList.range);  
    }


    private TotemSO GetRelevantTotemData(TotemName totemSO)
    {
        if (totemSO == TotemName.None)
            return null;
        int CsVID = 0;
        int startOfTotemID = 40000;
        Debug.Log(totemSO);
        switch (totemSO)
        {   
            case TotemName.prey:

                CsVID = 2;
                break;
            case TotemName.healing:

                CsVID = 1;
                break;
            case TotemName.detection:
                CsVID = 0;

                break;
            case TotemName.stamina:
                CsVID = 3;
                break;
            case TotemName.shock:
                CsVID = 4;
                break;
          
        }


        return LoadTotemData(startOfTotemID+ CsVID);
    }

    private TotemSO LoadTotemData(int id)=> ItemFactory._Instance.GenerateItem(id) as TotemSO;



    private Totem TryGetTotemFromDictionary(TotemName totemName) {
        if (totemName == TotemName.None || currentGameTotemDict == null || currentGameTotemDict.Count == 0)
            return null;

        foreach (var ttm in currentGameTotemDict)
        {
            if (ttm.Value.relevantSO.totemName == totemName)
                return ttm.Value;
        }
        return null;
    }
     bool ThereIsDetectionTotemInScene() {
        var cache = TryGetTotemFromDictionary(TotemName.detection);
        if (cache == null)
            return false;



            return true;    
    }

    public void CheckIfToSpawnBeastAtDetectionLocation() {

        if (!ThereIsDetectionTotemInScene())
            return ;


        var totem = TryGetTotemFromDictionary(TotemName.detection);
        if (totem == null)
            return;

        EnemyManager._Instance.GetBeastSettings((Difficulty)UnityEngine.Random.Range(0, 2), (Size)UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(1, 100), totem.transform.position, 5);

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
        else if (phase == PlayPhase.TotemCheckPhase)
            return true;

        return false;
    }
}
