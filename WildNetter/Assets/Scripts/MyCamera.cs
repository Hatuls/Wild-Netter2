
using Cinemachine;
using System;
using System.Collections;
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
        ZoomFunction();
    }
    void ZoomFunction() {
        if (Input.mouseScrollDelta.y != 0)
        {
            StopCoroutine(ZoomTween());
            StartCoroutine(ZoomTween());
        }
    }
    IEnumerator ZoomTween() {
        currentZoom = cmv.m_Lens.OrthographicSize;
        currentZoom -= Input.mouseScrollDelta.y * zoomAmount;

        currentZoom = Mathf.Clamp(currentZoom, maxZoomIn, maxZoomOut);
        yield return new WaitForSeconds(smoothTime);
        cmv.m_Lens.OrthographicSize = Mathf.Lerp(cmv.m_Lens.OrthographicSize, currentZoom, Time.deltaTime * zoomSpeed);



    }
    Vector3 AdjustCameraFromMouse() {
        float clampRange =10f;

        _HitInfo = GetRayHitInfo();

        Vector3 mousePos = new Vector3(Mathf.Clamp(_HitInfo.point.x, playerTransform.position.x - clampRange, playerTransform.position.x + clampRange), _HitInfo.point.y, Mathf.Clamp(_HitInfo.point.z, playerTransform.position.z - clampRange, playerTransform.position.z + clampRange));


        return Vector3.Lerp(mouseTransform.position, mousePos, smoothTime); ;
    }
}
