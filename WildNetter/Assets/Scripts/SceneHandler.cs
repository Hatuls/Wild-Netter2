
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // Script References:
    public static SceneHandler _Instance;



    // Component References:


    // Variables:
    public static int currentSceneIndex;

    // Getter & Setters:


    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    public void Init() { }

    public void LoadScene(int sceneToLoad)
    {

        Scene SceneToLoad = SceneManager.GetSceneAt(sceneToLoad);

        SceneManager.SetActiveScene(SceneToLoad);
    }
}