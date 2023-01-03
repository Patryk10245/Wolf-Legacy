using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenuControl : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject playWindow;
    [SerializeField] GameObject settingsWindow;
    [SerializeField] GameObject exitWindow;

    [SerializeField] GameObject onePlayer;
    [SerializeField] GameObject twoPlayers;


    [Header("Reference")]
    [SerializeField] GameSetup gameSetup;
    [SerializeField] Image selectedClassImage;


    private void Start()
    {
        gameSetup = GetComponent<GameSetup>();
    }


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


    public void ShowOnePlayer()
    {
        HideWindows();
        onePlayer.SetActive(true);
        gameSetup.OnePlayerSetup();
    }
    public void ShowTwoPlayers()
    {
        HideWindows();
        twoPlayers.SetActive(true);
        gameSetup.TwoPlayersSetup();
    }
    public void ReturnFromPlayers()
    {
        onePlayer.SetActive(false);
        twoPlayers.SetActive(false);
        ShowPlay();
    }
}
