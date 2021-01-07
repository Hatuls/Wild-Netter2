
using Cinemachine;
using System;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    //Script References:
    public static MyCamera _Instance;

    //Parameters
    float cameraZoom;
    public const float offSetTimeToFollow = 2f;  // <- after adjustment change to private
    float distanceFromTarget;
    bool engableFog = false;

    Vector3 targetVector;

    //Components References:
    [SerializeField] Camera _Camera;
    [SerializeField] Transform mouseTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform walkablePlane;
    [SerializeField] CinemachineTargetGroup groupCamera;
  [SerializeField]  CinemachineVirtualCamera cmv;
    //Ray And RayCast:
    Ray _Ray;
    public RaycastHit _HitInfo;
    [SerializeField] float currentZoom , maxZoomIn, maxZoomOut, zoomSpeed , zoomAmount;
    public float smoothTime;
    private void Awake()
    {
        _Instance = this;
        Init();
    }

    public void Init() {
        playerTransform = PlayerManager.GetInstance.GetPlayerTransform;
        _Ray = new Ray();
        _HitInfo = new RaycastHit();
      

        currentZoom = cmv.m_Lens.OrthographicSize;
    
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
        mouseTransform.position = AdjustCameraFromMouse();
       MakeCameraInRange();

        ZoomFunction();
    }
    void ZoomFunction() {
        if (Input.mouseScrollDelta.y != 0)
        {
            currentZoom = cmv.m_Lens.OrthographicSize;
        currentZoom -= Input.mouseScrollDelta.y* zoomAmount;
           
        currentZoom = Mathf.Clamp(currentZoom,maxZoomIn, maxZoomOut);

            cmv.m_Lens.OrthographicSize = Mathf.Lerp(cmv.m_Lens.OrthographicSize, currentZoom,Time.deltaTime * zoomSpeed);

        }
    }
    private void MakeCameraInRange()
    {




        Vector3 MiddlePoint = new Vector3((mouseTransform.position.x + playerTransform.position.x) / 2, 0, (mouseTransform.position.z + playerTransform.position.z) / 2);

        float amount = 0 ;
        if (Mathf.Abs(playerTransform.position.x)>130f || Mathf.Abs(playerTransform.position.z) > 65f)
        {
            amount = .3f;

        }
        else if (Mathf.Abs(playerTransform.position.x) > 110f|| Mathf.Abs(playerTransform.position.z) > 50f)
        {
            amount= 0.2f;

        }
        if (Mathf.Abs(playerTransform.position.z) > 80f)
        {
            if (playerTransform.position.z< 0 )
            {
                amount = .8f;
            }else
            amount =1f;
        }
        else if (Mathf.Abs(playerTransform.position.z) > 70.5f)
        {
            amount = 0.6f;
        }

        groupCamera.m_Targets[2].weight = Mathf.Lerp(groupCamera.m_Targets[2].weight, amount, Time.deltaTime * 2f);
     //   Debug.Log(Mathf.Abs(mouseTransform.position.x + playerTransform.position.x) / 2);


    }

    Vector3 AdjustCameraFromMouse() {
        float clampRange =10f;

        _HitInfo = GetRayHitInfo();

        Vector3 mousePos = new Vector3(Mathf.Clamp(_HitInfo.point.x, playerTransform.position.x - clampRange, playerTransform.position.x + clampRange), _HitInfo.point.y, Mathf.Clamp(_HitInfo.point.z, playerTransform.position.z - clampRange, playerTransform.position.z + clampRange));


        return Vector3.Lerp(mouseTransform.position, mousePos, smoothTime); ;
    }
}
