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

    }

    public void DisableRepeatButton()
    {
        repeatMapButton.SetActive(false);
    }
    public void DisableNextButton()
    {
        repeatMapButton.SetActive(false);
    }
}
