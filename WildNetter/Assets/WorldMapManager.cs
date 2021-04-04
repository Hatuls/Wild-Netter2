using UnityEngine;

public class WorldMapManager : MonoSingleton<WorldMapManager>
{
    [SerializeField] PathManager pathManager;

    [SerializeField] RectTransform playerToken;
    [SerializeField] RectTransform[] mapLocations;
    [SerializeField] bool isMovingToward;
    [SerializeField] RectTransform choosenLocation;


    public override void Init()
    {
        NoChoosenLocation();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGoToMapLocation(int index)
    {
        if (!isMovingToward)
        {
            isMovingToward = true;
            Debug.Log("Go to Location: " + index + " " + mapLocations[index].gameObject.name);

            //make playertoken go to map location that he clicked on
            choosenLocation = mapLocations[index];
            // pathManager.StartGoingOnPath();
            pathManager.CheckPath(pathManager.getStartZone, choosenLocation.GetComponent<Zone>(), playerToken.gameObject);
            //LeanTween.move(playerToken, choosenLocation.localPosition, 0.5f);
        }
    }

    public void NoChoosenLocation()
    {
        choosenLocation = null;
        isMovingToward = false;
    }

    public void ChangeToMapScene(string SceneName)
    {
        Debug.Log("Go to Location: " + SceneName);
        //when the player arrives to his destination change the scene
    }
}
