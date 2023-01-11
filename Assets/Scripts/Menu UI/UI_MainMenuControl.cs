using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UI_MainMenuControl : MonoBehaviour
{
    [Header("Windows")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject startWindow;
    [SerializeField] GameObject settingsWindow;
    [SerializeField] GameObject exitWindow;

    [Header("T2")]
    [SerializeField] GameObject settingsAudio;
    [SerializeField] GameObject settingsControls;
    [SerializeField] GameObject settingsRebindingP1;
    [SerializeField] GameObject settingsRebindingP2;

    [SerializeField] GameObject onePlayerWindow;
    [SerializeField] GameObject twoPlayersWindow;


    [Header("Reference")]
    [SerializeField] GameSetup gameSetup;
    [SerializeField] Image selectedClassImage;

    [SerializeField] GameObject player1KeyboardScheme;
    [SerializeField] GameObject player2KeyboardScheme;
    
    [SerializeField] GameObject player1GamepadScheme;
    [SerializeField] GameObject player2GamepadScheme;

    [SerializeField] GameObject player1NoneScheme;
    [SerializeField] GameObject player2NoneScheme;


    private void Start()
    {
    }


    public void Show_StartGame()
    {
        startWindow.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Show_Settings()
    {
        settingsWindow.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Show_QuitGame()
    {
        exitWindow.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void Show_Settings_Controls()
    {
        settingsControls.SetActive(true);
        settingsWindow.SetActive(false);
    }
    public void Show_Settings_Audio()
    {
        settingsAudio.SetActive(true);
        settingsWindow.SetActive(false);
    }
    public void Show_Start_1Player()
    {
        onePlayerWindow.SetActive(true);
        startWindow.SetActive(false);
        OnePlayer();

    }
    public void Show_Start_2Players()
    {
        twoPlayersWindow.SetActive(true);
        startWindow.SetActive(false);
        TwoPlayers();
    }


    public void Show_Settings_Rebinding_Player1()
    {
        settingsRebindingP1.SetActive(true);
    }
    public void Show_Settings_Rebinding_Player2()
    {
        settingsRebindingP2.SetActive(true);
    }

    public void Exit_StartGame()
    {
        startWindow.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Exit_Settings()
    {
        settingsWindow.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Exit_QuitGame()
    {
        exitWindow.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Exit_Settings_Controls()
    {
        settingsControls.SetActive(false);
        settingsWindow.SetActive(true);
    }
    public void Exit_Settings_Sounds()
    {
        settingsAudio.SetActive(false);
        settingsWindow.SetActive(true);
    }
    public void Exit_Start_ClassSelect()
    {
        onePlayerWindow.SetActive(false);
        twoPlayersWindow.SetActive(false);
        startWindow.SetActive(true);
    }
    public void Exit_SettignsRebinding()
    {
        settingsRebindingP1.SetActive(false);
        settingsRebindingP2.SetActive(false);
    }

    public void ExitGame_Yes()
    {
        Application.Quit();
    }
    public void ExitGame_No()
    {
        exitWindow.SetActive(false);
    }



    public void OnePlayer()
    {
        gameSetup.numberOfPlayers = 1;
    }
    public void TwoPlayers()
    {
        gameSetup.numberOfPlayers = 2;
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
        switch(id)
        {
            case 0:
                player1GamepadScheme.SetActive(false);
                player1KeyboardScheme.SetActive(true);
                player1NoneScheme.SetActive(false);
                break;
            case 1:
                player2GamepadScheme.SetActive(false);
                player2KeyboardScheme.SetActive(true);
                player2NoneScheme.SetActive(false);
                TwoPlayers();
                break;
                
        }    
    }
    public void ControlSchemeGamePad(int id)
    {
        gameSetup.playingPlayers[id].controlScheme = "GamePad";

        switch (id)
        {
            case 0:
                player1GamepadScheme.SetActive(true);
                player1KeyboardScheme.SetActive(false);
                player1NoneScheme.SetActive(false);
                break;
            case 1:
                player2GamepadScheme.SetActive(true);
                player2KeyboardScheme.SetActive(false);
                player2NoneScheme.SetActive(false);
                TwoPlayers();
                break;
        }
    }
    public void ControlSchemeNone(int id)
    {
        gameSetup.playingPlayers[id].controlScheme = "";

        switch (id)
        {
            case 1:
                player2GamepadScheme.SetActive(false);
                player2KeyboardScheme.SetActive(false);
                player2NoneScheme.SetActive(true);
                OnePlayer();
                break;
        }
    }

    public void KeyRebind(int x)
    {
        switch(x)
        {
            case 0:
                
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
        }
    }

    public void StartGame()
    {
        gameSetup.LoadClassData();
        Level_SelectedScenes.ins.ChangeToMap1();
    }
}
