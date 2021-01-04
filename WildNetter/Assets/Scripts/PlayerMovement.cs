using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{


   
    // Config parameters:
    [SerializeField] float speed = 30f;

    float sprintStamina;

    [SerializeField] float rotaionSpeed;


    Vector3 direction;

    public float velocity;

    bool isSprinting = false;

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

    //functions


    private void LateUpdate()
    {
        
        if (_RB != null )
            
        {
            Move();
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RotatePlayer();

            }
            

        
        }
    }

    void Move() {
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 targetVector = new Vector3(xInput, 0, zInput);

        direction = targetVector * GetSetPlayerSpeed;

        Debug.Log(direction);

     
        if (Input.GetKey(KeyCode.LeftShift)) { isSprinting = true; } else { isSprinting = false; }
        if (isSprinting)
        {
            velocity = direction.magnitude;
        }
        else
        {
            velocity = direction.magnitude /2;
        }
        
        PlayerGFX._instance.SetAnimationFloat(velocity, "Forward");
        Debug.Log(direction);
     

    }




    private void RotatePlayer()
    {
      
        Vector3 newrotates = new Vector3((MyCamera._Instance._HitInfo.point.x - transform.position.x), 0, (MyCamera._Instance._HitInfo.point.z - transform.position.z));

        transform.rotation = Quaternion.LookRotation(newrotates);
       // PlayerGFX._instance._Animator.rootRotation = Quaternion.LookRotation(newrotates);

    }
    private void Sprint(bool ableto) { }
  //  public Vector3 GetAngleDirection() { return Vector3.zero; }  < need to use later on 
    public void Init()
    {
      
        _RB = GetComponent<Rigidbody>();
    }

}