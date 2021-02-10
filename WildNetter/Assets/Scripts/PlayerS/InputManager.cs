
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;
public class InputManager : MonoSingleton<InputManager>
{

    PlayerCombat _playerCombat;
    PlayerMovement _playerMovement;
    float playerRadius = 1.5f;
    Vector3 inputVector;
    Vector3 mousePos;
   [SerializeField] private bool canPlayerMove = true;
    [SerializeField] private bool isPlayerRotateAble = true;
   public AttackType currectAttackType;
    enum MovementState { followAndMoveTowardMouse, MoveTowardWASD}
    MovementState movementState;
    public bool GetSetCanPlayerRotate
    {
        get => isPlayerRotateAble;
        set
        {
            if (isPlayerRotateAble != value)
            {
                isPlayerRotateAble = value;
            }
        }
    }
    public bool GetSetCanPlayerMove
    {
        set
        {
            if (canPlayerMove != value)
            {
                canPlayerMove = value;
            }
        }
        get
        {
            return canPlayerMove;
        }


    }

    public override void Init() {
        movementState = MovementState.followAndMoveTowardMouse;
        _playerMovement = PlayerMovement._Instance;
        _playerCombat = PlayerCombat._Instance;
        currectAttackType = AttackType.Melee;
    }

    // Update is called once per frame
    void Update()
    {
        PlayersInputs();
    }
    void PlayersInputs()
    {
        AssignMovementState();
        MouseInput();
        Movements();
        CombatInput();
    }

    private void CombatInput()
    {
        SetAttackType();

        if (Input.GetMouseButtonDown(0))
            _playerCombat.Attack();



    }


    void SetAttackType()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerCombat.SetAttackType(AttackType.Melee);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerCombat.SetAttackType(AttackType.Ranged);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currectAttackType != AttackType.Totem)
            _playerCombat.SetAttackType(AttackType.Totem);
            else
                EquipTotem();
            
            

        }
    }
    int totemSlotCounter = 1;
    void EquipTotem() {
        UiManager._Instance.HighLightNextImage();
        
        Debug.Log(totemSlotCounter);
        totemSlotCounter++;

        if (totemSlotCounter > 5)
            totemSlotCounter = 1;

    }
    void MouseInput()
    {
        if (!GetSetCanPlayerRotate)
            return;
        //if (Input.GetKey(KeyCode.W))
        //    _playerMovement.RotatePlayer(GetDirectionTowardTheMouse(), CheckIfMouseIsOnPlayer());


        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        //           SetLookState();


        mousePos = GetPointFromRayCast();



        //if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) <0.1f && Mathf.Abs(Input.GetAxis("Horizontal")) <0.1f)
        //{
        //    mousePos = GetDirectionTowardTheMouse();
        // //   _playerMovement.RotatePlayer(mousePos, CheckIfMouseIsOnPlayer());
        //}
        //else if (Input.GetAxis("Horizontal") < 0 && Mathf.Abs(Input.GetAxis("Vertical")) <= 0.1f)
        //{
        //    if (!flag)
        //        return;
        //    flag = false;
        // //   mousePos = GetDirectionTowardTheMouse() + Vector3.up * 90f;
        //    _playerMovement.RotateTowardsDirection(transform.right);
        //    return;
        //}
        //else if (Input.GetAxis("Horizontal") > 0 && Mathf.Abs(Input.GetAxis("Vertical")) <= 0.1f)
        //{
        //    mousePos = transform.forward;
        //}
        //_playerMovement.RotateTowardsDirection(transform.forward);
        ////_playerMovement.RotatePlayer(transform.forward, CheckIfMouseIsOnPlayer());
           PlayerMovement._Instance.RotatePlayer(GetPointFromRayCast(), CheckIfMouseIsOnPlayer());
    }



    enum LookState { Forward, Backward, LookLeft, LookForwardLeft, LookRight, LookForwardRight, Default };
    LookState lookingToward;
    Vector3 dirtoMouse;
    Vector3 GetPointFromRayCast() {
        dirtoMouse = Vector3.zero;
        if (MyCamera._Instance != null)
            dirtoMouse=  new Vector3(MyCamera._Instance._HitInfo.point.x, 0, MyCamera._Instance._HitInfo.point.z);

        return dirtoMouse;
    }
    void SetLookState() {
        LookState lookingNowToward = LookState.Default;
        float xInput = inputVector.x
            , zInput = inputVector.z;

        //

        if (xInput == 0 && zInput == 0)
            lookingNowToward = LookState.Default;
        else if (zInput != 0)
            lookingNowToward = LookState.Forward;
        else if (zInput == 0 && xInput > 0)
            lookingNowToward = LookState.LookRight;
        else if (zInput == 0 && xInput < 0)
            lookingNowToward = LookState.LookLeft;


        //if (lookingNowToward == lookingToward)
        //      return;
        switch (lookingNowToward)
            {
                case LookState.Default:
                case LookState.LookForwardLeft:
                case LookState.LookForwardRight:
                case LookState.Forward:
                    _playerMovement.RotatePlayer(mousePos, CheckIfMouseIsOnPlayer());
                    break;
                case LookState.Backward:
                    break;
                case LookState.LookLeft:
                    _playerMovement.RotateTowardsDirection(-transform.right);
                    break;


                case LookState.LookRight:
                    _playerMovement.RotateTowardsDirection(transform.right);
                    break;
            }

    }

    bool CheckIfMouseIsOnPlayer() => Vector3.Distance(mousePos, transform.position) < playerRadius;




    void AssignMovementState() {


        if (!MyCamera._Instance.SetGetMouseMove && inputVector != Vector3.zero)
            movementState = MovementState.MoveTowardWASD;


        else if (MyCamera._Instance.SetGetMouseMove && (inputVector != Vector3.zero || inputVector == Vector3.zero))
            movementState = MovementState.followAndMoveTowardMouse;


        // Debug.Log("movementState " + movementState);
    
    }





    void Movements()
    {

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));



        if (Input.GetKeyDown(KeyCode.Space))
            _playerMovement.Dash(inputVector);


        if (!GetSetCanPlayerRotate)
            return;

        _playerMovement.SetInput = inputVector;


        if (Input.GetButtonDown("Sprint"))
            _playerMovement.Sprint(true);


        if (Input.GetButtonUp("Sprint"))
            _playerMovement.Sprint(false);



    }
    public void FreezeRB(bool ToFreeAllOnRB) { 

            if (ToFreeAllOnRB)
                PlayerMovement._Instance.GetPlayerRB.constraints = RigidbodyConstraints.FreezeAll;
            else
                PlayerMovement._Instance.GetPlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
        
    }
    public void SetFreelyMoveAndRotate(bool CanFreelyMove)
    {

            GetSetCanPlayerRotate = CanFreelyMove;

            GetSetCanPlayerMove = CanFreelyMove;

    }
    IEnumerator FreezeCoroutine(float time) {
        FreezeRB(true);
        SetFreelyMoveAndRotate(false);
        yield return new WaitForSeconds(time);
        SetFreelyMoveAndRotate(true);
        FreezeRB(false);
    }
    public void FreezeCoroutineForShotPeriodOfTime(float time) {
        StopAllCoroutines();
        StartCoroutine(FreezeCoroutine(time));
    }

    public void ResetInputManager()
    {
        SetFreelyMoveAndRotate(true);
        FreezeRB(false);
        PlayerMovement._Instance.GetSetIsRunning = false;
    }
}
