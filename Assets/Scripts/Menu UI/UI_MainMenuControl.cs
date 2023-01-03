using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject playWindow;
    [SerializeField] GameObject settingsWindow;
    [SerializeField] GameObject exitWindow;


    public void ShowPlay()
    {
        HideWindows();
        HideButtons();
        playWindow.SetActive(true);
    }
    public void ShowSettings()
    {
        HideWindows();
        HideButtons();
        settingsWindow.SetActive(true);
    }
    public void ShowExit()
    {
        HideWindows();
        HideButtons();
        exitWindow.SetActive(true);
    }
    public void ShowButtons()
    {
        mainButtons.SetActive(true);
        HideWindows();
    }
    public void HideButtons()
    {
        mainButtons.SetActive(false);
    }
    public void HideWindows()
    {
        playWindow.SetActive(false);
        settingsWindow.SetActive(false);
        exitWindow.SetActive(false);
    }

    public void ExitGame_Yes()
    {
        Application.Quit();
    }
    public void ExitGame_No()
    {
        HideWindows();
        ShowButtons();
    }
}
