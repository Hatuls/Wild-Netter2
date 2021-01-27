
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Config parameters:
    [SerializeField] float walkingSpeed = 70f, runningSpeed = 120f, currentSpeed , maxSpeed =70f;
    float dashAmount = 20f;
    float forceLimit = 5f;
    InputManager _inputManager; 
    bool isRunning;
    [SerializeField] float rotaionSpeed;
    Vector3 direction;
    Vector3 rotationAngle;
    Vector3 input;
    static PlayerMovement _instance;
    public static PlayerMovement GetInstance
    {
        get
        {
            return _instance;
        }
    }
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

    //functions
    private void Awake()
    {
        _instance = this;
    }
    public void Init()
    {
        _inputManager = InputManager.GetInstance;
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
        

        if (!mouseOnPlayer )
        {
        rotationAngle = new Vector3(mousePos.x - transform.position.x, 0, mousePos.z - transform.position.z);
         transform.rotation = Quaternion.LookRotation(rotationAngle.normalized);
        }
        

        
       // PlayerGFX._instance._Animator.rootRotation = Quaternion.LookRotation(newrotates);

    }

    public void RotateTowardsDirection(Vector3 mousePos)
    {
        rotationAngle = new Vector3(mousePos.x, 0, mousePos.z );
        transform.rotation = Quaternion.LookRotation(rotationAngle.normalized);
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

    internal void Dash(Vector3 dashInput)
    {
        if (!PlayerStats.GetInstance.CheckEnoughStamina(dashAmount)) //
            return;
        Vector3 dashVector = dashInput.z * transform.forward +
                           dashInput.x * transform.right;


        PlayerGFX.GetInstance.SetAnimationTrigger("DoDash");

        if (dashVector.magnitude <= 0.1f)
            dashVector= transform.forward;
        

        RotateTowardsDirection(dashVector);
        float dashStrength = 25;
        dashVector *= dashStrength;

        _RB.AddForce(dashVector, ForceMode.Impulse);
        StopCoroutine(ApplyDash());
        StartCoroutine(ApplyDash());
    }
    private void MovePlayer()
    {
        if (_RB == null)
            return;

        
        Vector3 moveVector = input.z * transform.forward +
                             input.x * transform.right;

     
        if (_RB.velocity.magnitude < forceLimit)
            _RB.AddForce(moveVector , ForceMode.Force);


        PlayerGFX.GetInstance.SetAnimationFloat(GetSetPlayerSpeed, "Forward");
  
    }

    IEnumerator ApplyDash()
    {
        _inputManager.GetSetCanPlayerMove = false;
        _inputManager.GetSetCanPlayerRotate = false;
        yield return new WaitForSeconds(1f);
        _inputManager.GetSetCanPlayerRotate = true;
        _inputManager.GetSetCanPlayerMove = true;
    }

}