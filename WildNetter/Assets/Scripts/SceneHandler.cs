using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
public enum PlayPhase {TotemCheckPhase, PlanningPhase, BattlePhase };
public class SceneHandler : MonoSingleton<SceneHandler>
{

    List<TriggerArea> triggers;
   [SerializeField] Transform panel;

    
    public static int currentSceneIndex;

    // Getter & Setters:


   [SerializeField] PlayPhase currentPlayPhase;

    public Vector3 spawningPoint { get; set; }
    public PlayPhase GetSetPlayPhase {
        set
        {
            if (value == currentPlayPhase)
                return;
            currentPlayPhase = value;

        }
      get  => currentPlayPhase;
    
    }

    public override void Init() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        triggers = new List<TriggerArea>();


        AssignTriggerAreasToSceneHandler();
        GetSetPlayPhase = PlayPhase.PlanningPhase;
    }



    void AssignTriggerAreasToSceneHandler() {

        var go = GameObject.FindGameObjectsWithTag("AreaTriggers");

        for (int i = 0; i < go.Length; i++)
            triggers.Add(go[i].GetComponent<TriggerArea>());

        ResetAllTriggers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TotemManager._Instance.CheckIfToSpawnBeastAtDetectionLocation(); 
        }
    }


    public void TriggerNotification(TriggerArea theTriggered) {

        switch (theTriggered.GetTriggerType)
        {
            case TriggerAreaEffect.OpenUI:
                Debug.Log("Open UI!!!!");
                // UiManager._Instance
                InputManager._Instance.GetSetCanPlayerRotate = false;
                spawningPoint = theTriggered.gameObject.transform.position;
                UiManager._Instance.ExitZone();
                break;
            case TriggerAreaEffect.GoToScene:
                Debug.Log("GoToNext Scene!!!");
                //LoadScene(theTriggered.goToScene);
                PlayerMovement._Instance.GetSetPlayerSpeed = 0;
          
                PlayerMovement._Instance.RotateTowardsDirection(panel.position - PlayerManager._Instance.GetPlayerTransform.position);
                SpawnPlayer(theTriggered.gameObject.transform.position);
                break;
            default:
                break;
        }


    }

    void SpawnPlayer(Vector3 position) {
        PlayerManager._Instance.GetPlayerTransform.position = position;
        InputManager._Instance.FreezeCoroutineForShotPeriodOfTime(3f);
        currentPlayPhase = PlayPhase.BattlePhase;
    }


  public void ResetAllTriggers() {


        for (int i = 0; i < triggers.Count; i++)
        {
            triggers[i].SetFlag = true;
        }
    }


    public void LoadScene(int sceneToLoad)
    {

        Scene SceneToLoad = SceneManager.GetSceneAt(sceneToLoad);

        SceneManager.SetActiveScene(SceneToLoad);
        TotemManager._Instance.AssignCurrentTotemDictToScene(sceneToLoad);



        TotemManager._Instance.CheckIfToSpawnBeastAtDetectionLocation();
    }

    public void SetPlayerToScene(Vector3 playerPos)
    {
        PlayerMovement._Instance.GetSetPlayerSpeed = 0;

        PlayerMovement._Instance.RotateTowardsDirection(panel.position - PlayerManager._Instance.GetPlayerTransform.position);
        SpawnPlayer(playerPos);
    }
}