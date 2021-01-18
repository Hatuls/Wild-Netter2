using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{


   
    // Config parameters:
    [SerializeField] float speed = 30f;

    float sprintStamina;

    [SerializeField] float rotaionSpeed;
    Vector3 inputVector;
    Vector3 mousePos;
    Vector3 direction;
    Vector3 rotationAngle;
    public float velocity;

    [SerializeField] static bool isPlayerRotateAble = true;

    // Component References:
    Rigidbody _RB;
   

    //Getter And Setters:
    public float GetSetPlayerSpeed
    {
        get { return speed; }

        set { speed = value; }
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

    void GetInput() {

         inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        if (direction.magnitude > 1f)
                   direction.Normalize();

        inputVector = inputVector * GetSetPlayerSpeed;
        

<<<<<<< Updated upstream
        Sprint(Input.GetButton("Sprint"));
        
        PlayerGFX._instance.SetAnimationFloat(velocity, "Forward");
 
=======
>>>>>>> Stashed changes
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_RB == null)
            return;

        Vector3 moveVector = inputVector.z * transform.forward + 
                             inputVector.x * transform.right;
        float forceLimit;
        if (Input.GetButton("Sprint"))
            forceLimit = 7f;
        else
            forceLimit = 5f;

        if (_RB.velocity.magnitude < forceLimit)
            _RB.AddForce(moveVector , ForceMode.Force);
        
        PlayerGFX._instance.SetAnimationFloat(_RB.velocity.magnitude, "Forward");
        Debug.Log("Velocity : " + _RB.velocity.magnitude);
    }

<<<<<<< Updated upstream
    private void Sprint(bool _isSprinting) {
        if(_isSprinting)
        {
            velocity = direction.magnitude;
        }
        else
        {
            velocity = direction.magnitude / 2;
        }

    }
=======
>>>>>>> Stashed changes
    public Vector3 GetAngleDirection() {
        if (rotationAngle == null)
            return Vector2.zero;

        return rotationAngle.normalized; 
    
    } 

    bool CheckIfMouseIsOnPlayer()  => Vector3.Distance(mousePos, transform.position) > 1.8f;


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

}