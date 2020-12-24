
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

    private void Awake()
    { 
      UiManager._instance.Init();
      
    }
    private void Start()
    {
              Init();
    }

    private void Init()
    {
       


      var playersWeapon = ItemFactory.GetInstance().GenerateItem( 20000);
        (playersWeapon as WeaponSO).PrintWeaponSO();
        playersWeapon.Print();

        PlayerManager.GetInstance().Init(playersWeapon as WeaponSO);


  
       
        
        
   
    }
}
