using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{


   
    // Config parameters:
    [SerializeField] float speed = 30f;

    float sprintStamina;

    [SerializeField] float rotaionSpeed;

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


    private void LateUpdate()
    {
        
        if (_RB != null )
        {
           
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RotatePlayer();
            }
           
            Move();

            
        }
    }

    void Move() {
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 targetVector = new Vector3(xInput, 0, zInput);

        direction = targetVector * GetSetPlayerSpeed;


     
        if (Input.GetKey(KeyCode.LeftShift)) { Sprint(true); } else { Sprint(false); }
        
        PlayerGFX._instance.SetAnimationFloat(velocity, "Forward");
 
    }


    bool CheckIfMouseIsOnPlayer()  => Vector3.Distance(mousePos, transform.position) > 1.5f;


    private void RotatePlayer()
    {


        if (isPlayerRotateAble&&CheckIfMouseIsOnPlayer()  )
        {
        mousePos = new Vector3(MyCamera._Instance._HitInfo.point.x , 0 , MyCamera._Instance._HitInfo.point.z);
        rotationAngle = new Vector3(mousePos.x - transform.position.x, 0, mousePos.z - transform.position.z);

         transform.rotation = Quaternion.LookRotation(rotationAngle.normalized);
        }
        

        
       // PlayerGFX._instance._Animator.rootRotation = Quaternion.LookRotation(newrotates);

    }
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
    public Vector3 GetAngleDirection() {
        if (rotationAngle == null)
            return Vector2.zero;

        return rotationAngle.normalized; 
    
    } 
    public void Init()
    {
        _RB = GetComponent<Rigidbody>();

    }

}