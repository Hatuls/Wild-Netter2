
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public ISingleton[] singletons;
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        singletons = new ISingleton[12] {

            ItemFactory._Instance,
            MyCamera._Instance,
            PlayerManager._Instance,
            PlayerStats._Instance,
            TotemManager._Instance,
        UiManager._Instance,
        InputManager._Instance,
        PlayerCombat._Instance,
        PlayerMovement._Instance,
        SoundManager._Instance,
        EnemyManager._Instance,
        EnemySpawner._Instance
        };

        //var playersWeapon = ItemFactory._instance.GenerateItem( 20000);
        //  (playersWeapon as WeaponSO).PrintWeaponSO();
        //  playersWeapon.Print();
        //  Debug.ClearDeveloperConsole();
        //  PlayerInventory.GetInstance.PrintInventory();
        //  PlayerManager._instance.Init(playersWeapon as WeaponSO);
        //  TotemManager._instance.Init();
        //  UiManager._instance.Init();

        for (int i = 0; i < singletons.Length; i++)
        {
            if (singletons[i] != null)
                singletons[i].Init();
            

        }
    }
}
