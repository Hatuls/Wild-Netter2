
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;
public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] PlayerManager playerManager;
    //[SerializeField]PlayerCombat _playerCombat;
    //[SerializeField]PlayerMovement _playerMovement;
    float playerRadius = 1.5f;
    Vector3 inputVector;
    Vector3 mousePos;
   [SerializeField] private bool canPlayerMove = true;
    [SerializeField] private bool isPlayerRotateAble = true;
   public AttackType currectAttackType;
    //enum MovementState { followAndMoveTowardMouse, MoveTowardWASD}
    //MovementState movementState;
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
        //movementState = MovementState.followAndMoveTowardMouse;
        //_playerMovement = PlayerMovement._Instance;
        //_playerCombat = PlayerCombat._Instance;
        currectAttackType = AttackType.Melee;
    }

    // Update is called once per frame
    void Update()
    {
        PlayersInputs();
        if (Input.GetKeyDown( KeyCode.Q))
        {
            PlayerInventory.GetInstance.PrintInventory();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            playerManager.getPlayerCombat.GetHit(10, new Vector2(0, 0));
        }


    }
    void PlayersInputs()
    {
        //AssignMovementState();
        //MouseInput();
        Movements();
        CombatInput();
    }

    private void CombatInput()
    {
        SetAttackType();

        if (Input.GetMouseButtonDown(0))
            playerManager.getPlayerCombat.Attack();



    }


    void SetAttackType()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerManager.getPlayerCombat.SetAttackType(AttackType.Melee);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerManager.getPlayerCombat.SetAttackType(AttackType.Ranged);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currectAttackType != AttackType.Totem)
                playerManager.getPlayerCombat.SetAttackType(AttackType.Totem);
            else
                EquipTotem();
        }
    }
    int totemSlotCounter = 1;
    void EquipTotem() {
       // UiManager._Instance.HighLightNextImage();
        
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


        //mousePos = GetPointFromRayCast();



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
        //playermovement._instance.rotateplayer(getpointfromraycast(), checkifmouseisonplayer());
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
   

    //bool CheckIfMouseIsOnPlayer() => Vector3.Distance(mousePos, transform.position) < playerRadius;




    void AssignMovementState() {


        //if (!MyCamera._Instance.SetGetMouseMove && inputVector != Vector3.zero)
        //    movementState = MovementState.MoveTowardWASD;


        //else if (MyCamera._Instance.SetGetMouseMove && (inputVector != Vector3.zero || inputVector == Vector3.zero))
        //    movementState = MovementState.followAndMoveTowardMouse;


        // Debug.Log("movementState " + movementState);
    
    }





    void Movements()
    {

        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



        if (Input.GetKeyDown(KeyCode.Space))
            playerManager.getPlayerMovement.Dash(inputVector);


        if (!GetSetCanPlayerRotate)
            return;

        playerManager.getPlayerMovement.SetInput = inputVector;


        if (Input.GetButtonDown("Sprint"))
            playerManager.getPlayerMovement.Sprint(true);


        if (Input.GetButtonUp("Sprint"))
            playerManager.getPlayerMovement.Sprint(false);



    }
    public void FreezeRB(bool ToFreeAllOnRB) {

        if (ToFreeAllOnRB)
            playerManager.getPlayerMovement.GetPlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        else
            playerManager.getPlayerMovement.GetPlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

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
