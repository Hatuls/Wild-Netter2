
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    

    [SerializeField] Transform playerLegs;

    [SerializeField]
    private PlayerMovement playermovement;
    public PlayerMovement getPlayerMovement => playermovement;

    [SerializeField]
    private PlayerCombat playerCombat;
    public PlayerCombat getPlayerCombat => playerCombat;

    [SerializeField]
    private InputManager inputManager;
    public InputManager getInputManager;

    [SerializeField]
    private PlayerStats playerStats;
    public PlayerStats getPlayerStats => playerStats;

    [SerializeField]
    private PlayerGFX playerGfx;
    public PlayerGFX getPlayerGfx => playerGfx;



    //EventsAndAction
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "manipulated")
        //{
        //    if (collision.gameObject.GetComponentInChildren<Transform>().position.y > playerLegs.position.y)
        //    {
        //        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //    }
        //    else if (collision.gameObject.GetComponentInChildren<Transform>().position.y <= playerLegs.position.y)
        //    {
        //        collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        //    }
        //    Debug.Log("Tree Pos: " + collision.gameObject.GetComponentInChildren<Transform>().position.y + " Player Pos: " + playerLegs.position.y);
        //}
    }
    //Getter And Setter
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "manipulated")
        {
            Transform legPos = collision.gameObject.transform.GetChild(0).gameObject.GetComponent<Transform>();
            if (legPos.position.y < playerLegs.position.y)
            {
                if(collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder != 1)
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            else if (legPos.position.y >= playerLegs.position.y)
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder != -1)
                    collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
           
        }
    }
    public Transform GetPlayerTransform { get { return transform; } }

    //Functions:
   

    public override void Init()
    {
        InitPlayerComponents();
    }

        public void InitPlayerComponents()
    {
        //order are important here
        playermovement.Init();
        playerCombat.Init();
        inputManager.Init();
        playerStats.Init();
        playerGfx.Init();
    }

    public void Respawn() { }

    public void PlayerDead() { }



}
