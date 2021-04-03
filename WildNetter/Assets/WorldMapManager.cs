using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField] RectTransform playerToken;
    [SerializeField] RectTransform[] mapLocations;
    [SerializeField] bool isMovingToward;
    [SerializeField]RectTransform choosenLocation
    {
        set
        {
            if (value != null)
            {
                isMovingToward = true;
            }
            else
            {
                isMovingToward = false;
            }
            choosenLocation = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToward)
            GoTowardLocation();
    }

    void GoTowardLocation()
    {
       
    }

    public void OnClickGoToMapLocation(int index)
    {
        //make playertoken go to map location that he clicked on
        choosenLocation = mapLocations[index];
    }

    void NoChoosenLocation()
    {
        choosenLocation = null;
    }

    void ChangeToMapScene(string SceneName)
    {
        Debug.Log("Go to Location: " + SceneName);
        //when the player arrives to his destination change the scene
    }
}
