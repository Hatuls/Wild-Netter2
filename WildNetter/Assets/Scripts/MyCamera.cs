
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    //Script References:
    public static MyCamera _Instance;

    //Parameters
    float cameraZoom;
   public const float offSetTimeToFollow = 2f ;  // <- after adjustment change to private
    float distanceFromTarget;
    bool engableFog = false;
    Vector3 CameraMinOffSet;
    Vector3 CameraMaxOffSet;
    Vector3 targetVector;

    //Components References:
    [SerializeField]Camera _Camera;
    [SerializeField]Transform mouseTransform;
   
    //Ray And RayCast:
    Ray _Ray;
   public RaycastHit _HitInfo;


    private void Awake()
    {
        _Instance = this;
        Init();
    }
  
    public void Init() { 
      
        _Ray = new Ray();
        _HitInfo = new RaycastHit();
    }
    
    public void ZoomInOut(float zoomAmount) { }
     public RaycastHit GetRayHitInfo() {


        _Ray = _Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_Ray, out _HitInfo, 100f);
     

     // Debug.Log(_HitInfo.point + " " + _HitInfo.collider.gameObject.name);
        return _HitInfo;
    
    }
    private void Update()
    {
        mouseTransform.position = GetRayHitInfo().point;
      
    }

}
