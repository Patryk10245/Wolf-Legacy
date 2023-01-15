using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village_UIMapChoosing : MonoBehaviour
{
    [SerializeField] Village_UI_Control uiControl;
    [SerializeField] GameObject mapWindow;
    [Space(10)]
    [SerializeField] GameObject repeatMapButton;
    [SerializeField] GameObject nextMapButton;
    

    public void ShowMapChoosingWindow()
    {
        mapWindow.SetActive(!mapWindow.activeSelf);
    }

    public void Choose_RepeatMap()
    {
        Level_SelectedScenes.ins.RepeatFightMap();
    }
    public void Choose_MainMenu()
    {
        Level_SelectedScenes.ins.ChangeToMainmenu();
    }
    public void Choose_NextMap()
    {
        Level_SelectedScenes.ins.LoadNextFightMap();
    }

    public void DisableRepeatButton()
    {
        repeatMapButton.SetActive(false);
    }
    public void DisableNextButton()
    {
        nextMapButton.SetActive(false);
    }
}
