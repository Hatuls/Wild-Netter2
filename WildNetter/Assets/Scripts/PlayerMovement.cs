using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{



    // Config parameters:
    [SerializeField] float walkingSpeed = 70f, runningSpeed = 120f, currentSpeed , maxSpeed =70f;
    float forceLimit = 5f;
    float sprintStamina;
    bool isRunning;
    [SerializeField] float rotaionSpeed;
    Vector3 inputVector;
    Vector3 mousePos;
    Vector3 direction;
    Vector3 rotationAngle;


    [SerializeField] static bool isPlayerRotateAble = true;

    // Component References:
     Rigidbody _RB;
    public Rigidbody GetPlayerRB => _RB;

    //Getter And Setters:
    public float GetSetPlayerSpeed
    {
        get { return currentSpeed; }

        set { currentSpeed = value; }
    }
    public float GetSetSprintStamina
    {
        get { return sprintStamina; }

        set { sprintStamina = value; }
    }
    public static bool SetPlayerRotateAble {
        set { isPlayerRotateAble = value; }
    }

    //functions
    public void Init()
    {
        _RB = GetComponent<Rigidbody>();

    }


    private void Update()
    {
        
        if (_RB != null )
        {
            if (Input.GetKeyDown(KeyCode.W) || !EventSystem.current.IsPointerOverGameObject())
            {
                RotatePlayer();
            }
           
            GetInput();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public Vector3 GetAngleDirection() {
        if (rotationAngle == null)
            return Vector2.zero;

        return rotationAngle.normalized; 
    
    } 
    private void RotatePlayer()
    {
        




        mousePos = new Vector3(MyCamera._Instance._HitInfo.point.x , 0 , MyCamera._Instance._HitInfo.point.z);

        if (isPlayerRotateAble&& CheckIfMouseIsOnPlayer()  )
        {

        rotationAngle = new Vector3(mousePos.x - transform.position.x, 0, mousePos.z - transform.position.z);
         transform.rotation = Quaternion.LookRotation(rotationAngle.normalized);
        }
        

        
       // PlayerGFX._instance._Animator.rootRotation = Quaternion.LookRotation(newrotates);

    }
    bool CheckIfMouseIsOnPlayer()  => Vector3.Distance(mousePos, transform.position) > 1.8f;


    void GetInput() {

         inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        if (inputVector.magnitude <= .1f) {
            currentSpeed -= 200f * Time.deltaTime;
        }
        else {
            currentSpeed += 200f * Time.deltaTime;
        }
            currentSpeed = Mathf.Clamp(currentSpeed ,1 , maxSpeed);


          inputVector = inputVector * GetSetPlayerSpeed;
        

        if (direction.magnitude > 1f)
                   direction.Normalize();

    

        if (Input.GetButtonDown("Sprint") && !isRunning)
        {
            isRunning = true;
            maxSpeed = runningSpeed;

            forceLimit = 10f;

        }

        if (Input.GetButtonUp("Sprint"))
        {
            isRunning = false;
            maxSpeed = walkingSpeed;
        }
    }
    private void MovePlayer()
    {
        if (_RB == null)
            return;

        
        Vector3 moveVector = inputVector.z * transform.forward + 
                             inputVector.x * transform.right;

     
        if (_RB.velocity.magnitude < forceLimit)
            _RB.AddForce(moveVector , ForceMode.Force);


        
        PlayerGFX._instance.SetAnimationFloat(GetSetPlayerSpeed, "Forward");
  
    }
}