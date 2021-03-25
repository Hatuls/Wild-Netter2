
using Cinemachine;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MyCamera : MonoSingleton<MyCamera>
{
    //Parameters
    float cameraZoom;
    public const float offSetTimeToFollow = 2f;  // <- after adjustment change to private
    float distanceFromTarget;
    bool engableFog = false;
    Vector3 lastMousePos;
    Vector3 targetVector;

    //Components References:
    [SerializeField] Camera _Camera;
    [SerializeField] Transform mouseTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform walkablePlane;
  
  //[SerializeField]  CinemachineVirtualCamera cmv;
    //Ray And RayCast:
    Ray _Ray;
    public RaycastHit _HitInfo;
    [SerializeField] float currentZoom , maxZoomIn, maxZoomOut, zoomSpeed , zoomAmount;
    public float smoothTime;
    bool mouseMoved;
    public bool SetGetMouseMove {
        get => mouseMoved;
        set
        {
            if (mouseMoved != value)
                mouseMoved = value;
        }
    }
    public override void Init() {
        //playerTransform = PlayerManager._Instance.GetPlayerTransform;
        _Ray = new Ray();
        _HitInfo = new RaycastHit();
        lastMousePos = Input.mousePosition;

          //currentZoom = cmv.m_Lens.OrthographicSize;
    
    }

    public void ZoomInOut(float zoomAmount) { }
    public RaycastHit GetRayHitInfo() {


        _Ray = _Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_Ray, out _HitInfo, 100f);


        // Debug.Log(_HitInfo.point + " " + _HitInfo.collider.gameObject.name);

        return _HitInfo;

    }
    private void FixedUpdate()
    {
       
    }
    private void Update()
    {
        //CheckIfMouseMoved();
        //mouseTransform.position = AdjustCameraFromMouse();

    }

    //private void CheckIfMouseMoved()
    //{
    //    if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
    //        SetGetMouseMove = true;
    //    else
    //        SetGetMouseMove = false;
    //}
   
  
    //Vector3 AdjustCameraFromMouse() {
    //    float clampRange =10f;

    //    _HitInfo = GetRayHitInfo();

    //    Vector3 mousePos = new Vector3(Mathf.Clamp(_HitInfo.point.x, playerTransform.position.x - clampRange, playerTransform.position.x + clampRange), _HitInfo.point.y, Mathf.Clamp(_HitInfo.point.z, playerTransform.position.z - clampRange, playerTransform.position.z + clampRange));


    //    return Vector3.Lerp(mouseTransform.position, mousePos, smoothTime); 
        
    //}



}
