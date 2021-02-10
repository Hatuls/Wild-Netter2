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
    private bool mapMenuOn = false;
    private bool zoneMapOn = false;
    private bool worldMapOn = false;
    private void Start()
    {
        UiManager._Instance.ToggleGUIinScene(guiOn);
    }

    public void PlayerMenu()
    {
        mapMenuOn = false;
        playerMenuOn = true;
        inGamePopUpOn = true;
        inventoryMenuOn = false;
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
        UiManager._Instance.ToggleInGamePopUp(inGamePopUpOn);
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);
        UiManager._Instance.ToggleMapUI(mapMenuOn);
    }

    public void MapMenu()
    {
        playerMenuOn = false;
        inventoryMenuOn = false;
        mapMenuOn = true;
        zoneMapOn = true;
        UiManager._Instance.ToggleMapUI(mapMenuOn);
        UiManager._Instance.ToggleZoneMap(zoneMapOn);
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
    }

    public void ZoneMap()
    {
        zoneMapOn = true;
        worldMapOn = false;
        UiManager._Instance.ToggleZoneMap(zoneMapOn);
        UiManager._Instance.ToggleWorldMap(worldMapOn);
    }

    public void WorldMap()
    {
        zoneMapOn = false;
        worldMapOn = true;
        UiManager._Instance.ToggleWorldMap(worldMapOn);
        UiManager._Instance.ToggleZoneMap(zoneMapOn);
    }

    public void InventoryMenu()
    {
        mapMenuOn = false;
        playerMenuOn = false;
        inGamePopUpOn = true;
        inventoryMenuOn = true;
        UiManager._Instance.ToggleInGamePopUp(inGamePopUpOn);
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
        UiManager._Instance.ToggleMapUI(mapMenuOn);

        Time.timeScale = 0f;
    }

    public void PauseMenu()
    {
        pausePopUpOn = true;
        pauseMenuOn = true;
        inGameSettingsOn = false;
        UiManager._Instance.TogglePauseMenuPopUp(pausePopUpOn);
        UiManager._Instance.TogglePauseMenu(pauseMenuOn);
        UiManager._Instance.ToggleInGameOptionsMenu(inGameSettingsOn);
        Time.timeScale = 0f;
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
        Time.timeScale = 1f;
    }

    public void StartOver()
    {
        UiManager._Instance.LoadLevel(1);
    }

    public void MainMenu()
    {
        UiManager._Instance.LoadLevel(0);

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ESCinventory()
    {
        inventoryMenuOn = false;
        UiManager._Instance.ToggleInventoryMenu(inventoryMenuOn);

        Time.timeScale = 1f;
    }
}
