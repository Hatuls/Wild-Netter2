using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] string sceneName;

    //private 
    [SerializeField] Zone upNeighbor;
    [SerializeField] Zone leftNeighbor;
    [SerializeField] Zone rightNeighbor;
    [SerializeField] Zone downNeighbor;

    [SerializeField] path upPath;
    [SerializeField] path leftPath;
    [SerializeField] path rightPath;
    [SerializeField] path downPath;
    //get
    public Zone getUpNeighbor => upNeighbor;
    public Zone getLeftNeighbor => leftNeighbor;
    public Zone getRightNeighbor => rightNeighbor;
    public Zone getDownNeighbor => downNeighbor;

    public path getUpPath => upPath;
    public path getLeftPath => leftPath;
    public path getRightPath => rightPath;
    public path getDownPath => downPath;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Test");
        if (collision.tag == "PlayerToken")
        {
            WorldMapManager._Instance.NoChoosenLocation();
            WorldMapManager._Instance.ChangeToMapScene(sceneName);
        }
    }



}
