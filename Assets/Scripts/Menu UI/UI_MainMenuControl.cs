using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        OnePlayerSetup();
    }
    public void ShowTwoPlayers()
    {
        HideWindows();
        twoPlayers.SetActive(true);
        TwoPlayersSetup();
    }
    public void ReturnFromPlayers()
    {
        onePlayer.SetActive(false);
        twoPlayers.SetActive(false);
        ShowPlay();
    }


    public void OnePlayerSetup()
    {
        gameSetup.playingPlayers.Clear();

        gameSetup.numberOfPlayers = 1;
        PlayerSelectedData player = new PlayerSelectedData();
        player.id = 0;
        gameSetup.playingPlayers.Add(player);

        PlayerSelectedData player2 = new PlayerSelectedData();
        player2.id = 1;
        gameSetup.playingPlayers.Add(player2);
    }
    public void TwoPlayersSetup()
    {
        gameSetup.playingPlayers.Clear();


        gameSetup.numberOfPlayers = 2;
        PlayerSelectedData player1 = new PlayerSelectedData();
        player1.id = 0;
        PlayerSelectedData player2 = new PlayerSelectedData();
        player2.id = 1;

        gameSetup.playingPlayers.Add(player1);
        gameSetup.playingPlayers.Add(player2);
    }


    public void ChoosePaladin(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Paladin;
        //Debug.Log("choose paladin");
    }
    public void ChooseBarbarian(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Barbarian;
        //Debug.Log("choose barbarian");
    }
    public void ChooseRanger(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Ranger;
        //Debug.Log("choose ranger");
    }
    public void ChooseMage(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Mage;
        //Debug.Log("choose mage");
    }

    public void ControlSchemeKeyBoard(int id)
    {
        gameSetup.playingPlayers[id].controlScheme = "KeyBoard";
    }
    public void ControlSchemeGamePad(int id)
    {
        gameSetup.playingPlayers[id].controlScheme = "GamePad";
    }

    public void StartGame()
    {
        GameSetup.ins.LoadClassData();
        Level_SelectedScenes.ins.ChangeToMap1();
    }
}
