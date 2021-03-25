using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    // Config parameters:
    [SerializeField] float walkingSpeed = 70f, runningSpeed = 120f, currentSpeed , maxSpeed =70f;
    float dashAmount = 20f;
    float forceLimit = 5f;
    InputManager _inputManager; 
   [SerializeField] bool isRunning , canDash;
    [SerializeField] float rotaionSpeed;
    Vector2 input;
    [SerializeField] Transform HeadTransform;
    public bool GetSetIsRunning { get => isRunning; set { isRunning = value; } }
    // Component References:
    Rigidbody2D _RB;
    public Rigidbody2D GetPlayerRB => _RB;
    public Vector2 SetInput
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
    public bool GetSetCanDash { set
        {
            if (value != canDash)
                canDash = value;
        }
        get => canDash;
    }
    //functions
 
    public override void Init()
    {
        _inputManager = InputManager._Instance;
        _RB = GetComponent<Rigidbody2D>();
        input = Vector3.zero;
        canDash = true;
        isRunning = false;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

 
    public void RotatePlayer(Vector3 mousePos)
    { 

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
        currentSpeed = Mathf.Clamp(currentSpeed, 0.1f, maxSpeed);


        input *= GetSetPlayerSpeed;
        //input.Normalize();
        transform.Translate(new Vector2(input.x, input.y));


        //if (direction.magnitude > 1f)
        //    direction.Normalize();
    }

    public void Sprint(bool toSprint) {


        if (toSprint && !isRunning )
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

    internal void Dash(Vector2 dashInput)
    {
        if (!PlayerStats._Instance.CheckEnoughStamina(dashAmount)||!canDash)
            return;



        //Vector3 dashVector = dashInput.z * transform.forward +
        //                   dashInput.x * transform.right;

        
        GetSetCanDash = false;
        PlayerGFX._Instance.SetAnimationTrigger("DoDash");

        // if (dashVector.magnitude <= 0.1f)
        Vector2 dashVector = -transform.forward;
        

        //RotateTowardsDirection(dashVector);
        float dashStrength = 30;
        dashVector *= dashStrength;

        _RB.AddForce(dashVector, ForceMode2D.Impulse);

        StartDashCooldown(1f);
    }
    private void MovePlayer()
    {
        if (_RB == null)
            return;

        
        Vector2 moveVector = input.y * transform.forward +
                             input.x * transform.right;

     
        if (_RB.velocity.magnitude < forceLimit)
            _RB.AddForce(moveVector , ForceMode2D.Force);

        if (PlayerGFX._Instance!=null)
                PlayerGFX._Instance.SetAnimationFloat(GetSetPlayerSpeed, "Forward");





        if (PlayerStats._Instance == null)
            return;

        if (GetSetIsRunning)
            PlayerStats._Instance.AddStaminaAmount(-10f * Time.deltaTime);
        if (!PlayerStats._Instance.CheckEnoughStamina(-5f))
            Sprint(false);
        
    }

 

    bool flag;
    public void StartDashCooldown(float amount) {
        if (flag == false)
        {
            flag = true;
StopCoroutine(DashCooldown(amount));
        StartCoroutine(DashCooldown(amount));

        }
        
    
    }
    IEnumerator DashCooldown(float amount) {
        _inputManager.SetFreelyMoveAndRotate(false);
        GetSetCanDash = false;
        _inputManager.FreezeRB(false);
        yield return new WaitForSeconds(amount);
        
        InputManager._Instance.FreezeRB(false);
        _inputManager.SetFreelyMoveAndRotate(true);
        flag = false;
        GetSetCanDash = true;
    }
 
}