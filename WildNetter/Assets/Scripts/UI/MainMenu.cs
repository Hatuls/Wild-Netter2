using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool mainMenuOn = true;
    private bool loadGameOn = false;
    private bool newsOn = false;
    private bool optionsOn = false;
    private void Start()
    {
        UiManager._Instance.ToggleMainMenu(mainMenuOn);
    }

    public void LoadGameMenu()
    {
        mainMenuOn = false;
        loadGameOn = true;
        UiManager._Instance.ToggleMainMenu(mainMenuOn);
        UiManager._Instance.ToggleLoadGameMenu(loadGameOn);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NewsMenu()
    {
        mainMenuOn = false;
        newsOn = true;
        UiManager._Instance.ToggleMainMenu(mainMenuOn);
        UiManager._Instance.ToggleNewsMenu(newsOn);
    }

    public void OptionsMenu()
    {
        mainMenuOn = false;
        optionsOn = true;
        UiManager._Instance.ToggleMainMenu(mainMenuOn);
        UiManager._Instance.ToggleOptionsMenu(optionsOn);
    }

    public void Back()
    {
        mainMenuOn = true;
        loadGameOn = false;
        newsOn = false;
        optionsOn = false;
        UiManager._Instance.ToggleMainMenu(mainMenuOn);
        UiManager._Instance.ToggleLoadGameMenu(loadGameOn);
        UiManager._Instance.ToggleNewsMenu(newsOn);
        UiManager._Instance.ToggleOptionsMenu(optionsOn);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
