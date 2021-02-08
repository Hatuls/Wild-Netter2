using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private bool guiOn = true;
    private bool playerMenuOn = false;
    private bool inGamePopUpOn = false;
    private bool inventoryMenuOn = false;
    private bool pauseMenuOn = false;
    private bool pausePopUpOn = false;
    private bool inGameSettingsOn = false;
    private bool inGameSoundSettings = false;
    private bool inGameGraphicsSettings = false;
    private bool inGameControlsSettings = false;
    private void Start()
    {
        UiManager._Instance.ToggleGUIinScene(guiOn);
    }

    public void PlayerMenu()
    {
        playerMenuOn = true;
        inGamePopUpOn = true;
        inventoryMenuOn = false;
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
        UiManager._Instance.ToggleInGamePopUp(inGamePopUpOn);
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);
    }

    public void InventoryMenu()
    {
        playerMenuOn = false;
        inGamePopUpOn = true;
        inventoryMenuOn = true;
        UiManager._Instance.ToggleInGamePopUp(inGamePopUpOn);
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
    }

    public void PauseMenu()
    {
        pausePopUpOn = true;
        pauseMenuOn = true;
        inGameSettingsOn = false;
        UiManager._Instance.TogglePauseMenuPopUp(pausePopUpOn);
        UiManager._Instance.TogglePauseMenu(pauseMenuOn);
        UiManager._Instance.ToggleInGameOptionsMenu(inGameSettingsOn);
    }
    public void PauseMenuSettings()
    {
        pausePopUpOn = true;
        pauseMenuOn = false;
        inGameSettingsOn = true;
        inGameSoundSettings = true;
        UiManager._Instance.ToggleInGameOptionsMenu(inGameSettingsOn);
        UiManager._Instance.TogglePauseMenuPopUp(pausePopUpOn);
        UiManager._Instance.TogglePauseMenu(pauseMenuOn);
        UiManager._Instance.ToggleSoundSettings(inGameSoundSettings);
    }

    public void SoundSettings()
    {
        inGameControlsSettings = false;
        inGameGraphicsSettings = false;
        inGameSoundSettings = true;
        UiManager._Instance.ToggleControlsSettings(inGameControlsSettings);
        UiManager._Instance.ToggleGraphicsSettings(inGameGraphicsSettings);
        UiManager._Instance.ToggleSoundSettings(inGameSoundSettings);
    }

    public void Graphicsettings()
    {
        inGameControlsSettings = false;
        inGameGraphicsSettings = true;
        inGameSoundSettings = false;
        UiManager._Instance.ToggleControlsSettings(inGameControlsSettings);
        UiManager._Instance.ToggleGraphicsSettings(inGameGraphicsSettings);
        UiManager._Instance.ToggleSoundSettings(inGameSoundSettings);
    }

    public void ControlsSettings()
    {
        inGameControlsSettings = true;
        inGameGraphicsSettings = false;
        inGameSoundSettings = false;
        UiManager._Instance.ToggleControlsSettings(inGameControlsSettings);
        UiManager._Instance.ToggleGraphicsSettings(inGameGraphicsSettings);
        UiManager._Instance.ToggleSoundSettings(inGameSoundSettings);
    }

    public void BackFromSettings()
    {
        PauseMenu();
    }

    public void Continue()
    {
        pausePopUpOn = false;
        UiManager._Instance.TogglePauseMenuPopUp(pausePopUpOn);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
