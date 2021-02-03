
using UnityEngine;
public class InputManager : MonoSingleton<InputManager>
{
   
    PlayerCombat _playerCombat;
    PlayerMovement _playerMovement;
    float playerRadius = 1.5f;
    Vector3 inputVector;
    Vector3 mousePos;
    private  bool canPlayerMove = true;
    private  bool isPlayerRotateAble = true;
    public  bool GetSetCanPlayerRotate
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
    public  bool GetSetCanPlayerMove
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
        
        _playerMovement = PlayerMovement._Instance;
        _playerCombat = PlayerCombat._Instance;
    }

    // Update is called once per frame
    void Update()
    {
        PlayersInputs();
    }
    void PlayersInputs()
    {

        Movements();
        MouseInput();
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
            _playerCombat.SetAttackType(AttackType.Totem);
          
        }
    }
    void MouseInput()
    {
        if (!GetSetCanPlayerRotate)
            return;
        
        mousePos = new Vector3(MyCamera._Instance._HitInfo.point.x, 0, MyCamera._Instance._HitInfo.point.z);

        _playerMovement.RotatePlayer(mousePos, CheckIfMouseIsOnPlayer());
    }
    bool CheckIfMouseIsOnPlayer() => Vector3.Distance(mousePos, transform.position) < playerRadius;
    void Movements()
    {

        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
            _playerMovement.Dash(inputVector);
        

        if (!GetSetCanPlayerRotate)
           return;
        
        _playerMovement.SetInput = inputVector;


        if (Input.GetButtonDown("Sprint"))
        {
            _playerMovement.Sprint(true);
        }

        if (Input.GetButtonUp("Sprint"))
        {
            _playerMovement.Sprint(false);
        }

       
    }

  

}
