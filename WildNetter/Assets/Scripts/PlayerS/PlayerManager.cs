
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{



    //EventsAndAction

    //Getter And Setter

    public Transform GetPlayerTransform { get { return transform; } }

    //Functions:


    public override void Init()
    {


    }

    public void Respawn() { }

    public void PlayerDead() { }



}
