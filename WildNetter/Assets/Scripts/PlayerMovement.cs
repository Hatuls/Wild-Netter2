
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{



    // Config parameters:
    [SerializeField] float walkingSpeed = 70f, runningSpeed = 120f, currentSpeed , maxSpeed =70f;
    float forceLimit = 5f;
    float sprintStamina;
    bool isRunning;
    [SerializeField] float rotaionSpeed;
    Vector3 direction;
    Vector3 rotationAngle;
    Vector3 input;

    [SerializeField] static bool isPlayerRotateAble = true;

    // Component References:
     Rigidbody _RB;
    public Rigidbody GetPlayerRB => _RB;
    public Vector3 SetInput
    {
        set
        {
            input = value;
            InputIntoMovement();
        }
    }
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
        input = Vector3.zero;
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
    public void RotatePlayer(Vector3 mousePos , bool mouseOnPlayer)
    {
        

        if (isPlayerRotateAble && !mouseOnPlayer )
        {
        rotationAngle = new Vector3(mousePos.x - transform.position.x, 0, mousePos.z - transform.position.z);
         transform.rotation = Quaternion.LookRotation(rotationAngle.normalized);
        }
        

        
       // PlayerGFX._instance._Animator.rootRotation = Quaternion.LookRotation(newrotates);

    }
   


   private void InputIntoMovement()
    {

        if (input.magnitude <= .1f)
        {
            currentSpeed -= 200f * Time.deltaTime;
        }
        else
        {
            currentSpeed += 200f * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 1, maxSpeed);


        input *= GetSetPlayerSpeed;


        if (direction.magnitude > 1f)
            direction.Normalize();
    }

    public void Sprint(bool toSprint) {


        if (toSprint && !isRunning)
        {
            isRunning = true;
            maxSpeed = runningSpeed;

            forceLimit = 10f;

        }

        if (!toSprint)
        {
            isRunning = false;
            maxSpeed = walkingSpeed;
            forceLimit = 6f;
        }

    }

   
    private void MovePlayer()
    {
        if (_RB == null)
            return;

        
        Vector3 moveVector = input.z * transform.forward +
                             input.x * transform.right;

     
        if (_RB.velocity.magnitude < forceLimit)
            _RB.AddForce(moveVector , ForceMode.Force);


        
        PlayerGFX._instance.SetAnimationFloat(GetSetPlayerSpeed, "Forward");
  
    }
}