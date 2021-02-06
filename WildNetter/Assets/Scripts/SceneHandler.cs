
using Boo.Lang;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoSingleton<SceneHandler>
{

    List<TriggerArea> triggers;








    public static int currentSceneIndex;

    // Getter & Setters:

    public override void Init() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        triggers = new List<TriggerArea>();


        AssignTriggerAreasToSceneHandler();

    }



    void AssignTriggerAreasToSceneHandler() {

        var go = GameObject.FindGameObjectsWithTag("AreaTriggers");

        for (int i = 0; i < go.Length; i++)
            triggers.Add(go[i].GetComponent<TriggerArea>());

        ResetAllTriggers();
    }




    public void TriggerNotification(TriggerArea theTriggered) {

        switch (theTriggered.GetTriggerType)
        {
            case TriggerAreaEffect.OpenUI:
                Debug.Log("Open UI!!!!");  
               // UiManager._Instance

                break;
            case TriggerAreaEffect.GoToScene:
                Debug.Log("GoToNext Scene!!!");
                //LoadScene(theTriggered.goToScene);

                break;
            default:
                break;
        }


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
    }
}