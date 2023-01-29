using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UI_MainMenuControl : MonoBehaviour
{
    public static UI_MainMenuControl ins;
    public void Reference()
    {
        ins = this;
    }

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
    [SerializeField] GameObject deleteProgress;

    [SerializeField] GameObject onePlayerWindow;
    [SerializeField] GameObject twoPlayersWindow;
    [Header("T3")]
    [SerializeField] GameObject p2Window;
    [SerializeField] GameObject p1Paladin;
    [SerializeField] GameObject p1Barbarian;
    [SerializeField] GameObject p1Archer;
    [SerializeField] GameObject p1Mage;
    [SerializeField] GameObject p2Paladin;
    [SerializeField] GameObject p2Barbarian;
    [SerializeField] GameObject p2Archer;
    [SerializeField] GameObject p2Mage;


    [Header("Reference")]
    [SerializeField] GameSetup gameSetup;
    [SerializeField] Image selectedClassImage;

    [SerializeField] GameObject player1KeyboardScheme;
    [SerializeField] GameObject player2KeyboardScheme;
    
    [SerializeField] GameObject player1GamepadScheme;
    [SerializeField] GameObject player2GamepadScheme;

    [SerializeField] GameObject player1NoneScheme;
    [SerializeField] GameObject player2NoneScheme;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundsSlider;


    private void Awake()
    {
        Reference();
    }

    private void Start()
    {
        gameSetup = GameSetup.ins;
        HideAllClassesForPlayer1();
        HideAllClassesForPlayer2();
        ShowActiveClass(0);
        ShowActiveClass(1);
    }


    public void Show_StartGame()
    {
        if(gameSetup.numberOfPlayers == 1)
        {
            p2Window.SetActive(false);
        }
        else
        {
            p2Window.SetActive(true);
        }
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
        musicSlider.value = AudioManager.ins.GetMusicVolume();
        soundsSlider.value = AudioManager.ins.GetSoundVolume();
    }
    public void Show_DeleteProgress()
    {
        deleteProgress.SetActive(true);
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
        startWindow.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Exit_DeleteProgress()
    {
        deleteProgress.SetActive(false);
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
        mainMenu.SetActive(true);
    }


    public void DeleteCurrentGameProgress()
    {
        PlayerPrefs.DeleteAll();
        Game_State.firstRun = true;
        Exit_Settings();
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
    }
    public void ChooseBarbarian(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Barbarian;
    }
    public void ChooseRanger(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Ranger;
    }
    public void ChooseMage(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Mage;
    }


    public void ShowPaladin(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Paladin;
        switch(id)
        {
            case 0:
                p1Mage.SetActive(false);
                p1Paladin.SetActive(true);
                break;
            case 1:
                p2Mage.SetActive(false);
                p2Paladin.SetActive(true);
                break;
        }
    }
    public void ShowBarbarian(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Barbarian;
        switch(id)
        {
            case 0:
                p1Paladin.SetActive(false);
                p1Barbarian.SetActive(true);
                break;
            case 1:
                p2Paladin.SetActive(false);
                p2Barbarian.SetActive(true);
                break;
        }
    }
    public void ShowArcher(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Ranger;
        switch (id)
        {
            case 0:
                p1Barbarian.SetActive(false);
                p1Archer.SetActive(true);
                break;
            case 1:
                p2Barbarian.SetActive(false);
                p2Archer.SetActive(true);
                break;
        }
    }
    public void ShowMage(int id)
    {
        gameSetup.playingPlayers[id].selectedClass = ENUM_PlayerClass.Mage;
        switch (id)
        {
            case 0:
                p1Archer.SetActive(false);
                p1Mage.SetActive(true);
                break;
            case 1:
                p2Archer.SetActive(false);
                p2Mage.SetActive(true);
                break;
        }
    }

    void HideAllClassesForPlayer1()
    {
        p1Paladin.SetActive(false);
        p1Barbarian.SetActive(false);
        p1Archer.SetActive(false);
        p1Mage.SetActive(false);
    }
    void HideAllClassesForPlayer2()
    {
        p2Paladin.SetActive(false);
        p2Barbarian.SetActive(false);
        p2Archer.SetActive(false);
        p2Mage.SetActive(false);
    }
    public void ShowActiveClass(int id)
    {
        switch(gameSetup.playingPlayers[id].selectedClass)
        {
            case ENUM_PlayerClass.Paladin:
                ShowPaladin(id);
                break;
            case ENUM_PlayerClass.Barbarian:
                ShowBarbarian(id);
                break;
            case ENUM_PlayerClass.Ranger:
                ShowArcher(id);
                break;
            case ENUM_PlayerClass.Mage:
                ShowMage(id);
                break;
        }
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

    public void StartGame()
    {
        gameSetup.LoadClassData();
        Level_SelectedScenes.ins.RandomizeMaps();
        Level_SelectedScenes.ins.currentFightScene = -1;

        if (Game_State.firstRun == true)
        {
            Level_SelectedScenes.ins.LoadStoryScene();
            return;
        }

        Level_SelectedScenes.ins.LoadNextScene();
        Game_State.lastBossDefeated = false;
    }

    public void ChangeMasterVolume()
    {
        AudioManager.ins.ChangeMusicVolume(musicSlider.value);
    }
    public void ChangeSoundVolume()
    {
        AudioManager.ins.ChangeSoundVolume(soundsSlider.value);
    }
}
