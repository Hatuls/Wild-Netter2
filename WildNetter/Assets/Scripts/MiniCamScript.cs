
using UnityEngine;

public class MiniCamScript : MonoSingleton<MiniCamScript>
{
     Transform playerPos;
    Vector3 miniCamPos;


    public override void Init() {
            playerPos = PlayerManager._Instance.GetPlayerTransform;
    }

    private void FixedUpdate()
    {
        miniCamPos = new Vector3(playerPos.position.x, 10f, playerPos.position.z);
        transform.position = miniCamPos;
    }

}
