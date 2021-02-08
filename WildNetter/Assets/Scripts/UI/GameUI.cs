using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private bool guiOn = true;
    private bool playerMenuOn = false;
    private void Start()
    {
        UiManager._Instance.ToggleGUIinScene(guiOn);
    }

    public void PlayerMenu()
    {
        playerMenuOn = true;
        UiManager._Instance.TogglePlayerMenu(playerMenuOn);
    }
}
