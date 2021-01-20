
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Script References
    static GameManager _instance;

    public GameManager GetInstance() {
        if (_instance == null)
        {
            _instance = new GameManager();
           
        }
        return _instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerInventory.GetInstance. PrintInventory();
        }
    }
    private void Awake()
    { 
      UiManager.GetInstance.Init();
      
    }
    private void Start()
    {
              Init();
    }

    private void Init()
    {

        UiManager.GetInstance.Init();

      var playersWeapon = ItemFactory.GetInstance().GenerateItem( 20000);
        (playersWeapon as WeaponSO).PrintWeaponSO();
      //  playersWeapon.Print();
        //Debug.ClearDeveloperConsole();
        //PlayerInventory.GetInstance.PrintInventory();
        PlayerManager.GetInstance.Init(playersWeapon as WeaponSO);
        TotemManager._instance.Init();
        for (int i = 0; i < 3; i++)
        {
            var x = ItemFactory.GetInstance().GenerateItem(10000);
            x.amount = 39;
            PlayerInventory.GetInstance.AddToInventory(x);
        }

        PlayerInventory.GetInstance.PrintInventory();


    }
}
