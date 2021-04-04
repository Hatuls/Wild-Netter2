using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    Zone startZone;
    public Zone getStartZone => startZone;

    //[SerializeField]
    //Zone targetZone;

    [SerializeField]
    List<Transform> listPath = new List<Transform>();

    [SerializeField]
    int pathIndex;

    public void CheckPath(Zone start, Zone target, GameObject playerToken)
    {
        //targetZone = target;
        startZone = start;
        if (start.getUpNeighbor == target)
        {
            Debug.Log("up neighbor");
            for (int i = 0; i < start.getUpPath.getPaths.Length; i++)
            {
                listPath.Add(start.getUpPath.getPaths[i]);
            }
        }
        else if (start.getLeftNeighbor == target)
        {
            Debug.Log("Left neighbor");
            for (int i = 0; i < start.getLeftPath.getPaths.Length; i++)
            {
                listPath.Add(start.getLeftPath.getPaths[i]);
            }
        }
        else if (start.getRightNeighbor == target)
        {
            Debug.Log("right neighbor");
            for (int i = 0; i < start.getRightPath.getPaths.Length; i++)
            {
                listPath.Add(start.getRightPath.getPaths[i]);
            }
        }
        else if (start.getDownNeighbor == target)
        {
            Debug.Log("Down neighbor");
            for (int i = 0; i < start.getDownPath.getPaths.Length; i++)
            {
                listPath.Add(start.getDownPath.getPaths[i]);
            }
        }
        else
        {
            //targetZone = null;
            Debug.Log("those zones are not neighbors, Starting to calculate path");
            WorldMapManager._Instance.NoChoosenLocation();


        }
        if (listPath.Count > 0)
        {
            StartCoroutine(StartGoingOnPath(playerToken, target));
        }
    }

    public IEnumerator StartGoingOnPath(GameObject playerToken, Zone target)
    {

        Debug.Log(pathIndex);



        LeanTween.move(playerToken, listPath[pathIndex], 0.5f);



        yield return new WaitForSeconds(0.5f);
        if (pathIndex < listPath.Count - 1)
        {
            pathIndex++;
            //Mathf.Clamp(pathIndex, 0, listPath.Count);
            StartCoroutine(StartGoingOnPath(playerToken, target));
        }
        else
        {
            pathIndex = 0;
            listPath.Clear();
            startZone = target;
            WorldMapManager._Instance.NoChoosenLocation();
            //targetZone = null;
        }
    }
}





[Serializable]
public class path
{
    [SerializeField]
    Transform[] paths;
    public Transform[] getPaths => paths;
}