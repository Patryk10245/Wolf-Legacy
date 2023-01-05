using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager ins;
    void Reference()
    {
        ins = this;
        GameInitialization.ins.playerManager = this;
    }
    void Awake()
    {
        Reference();
        DontDestroyOnLoad(this);
        GameInitialization.ins.playerManager = this;
    }
    private void Start()
    {
        
        try
        {
            gameSetup = GameSetup.ins;
            if(gameSetup.gameAlreadySetup == true)
            {
                return;
            }
            gameSetup.SetUpTheGame();
            ScoreTable.ins.SetReferenceToGoldText();
        }
        catch(Exception exception)
        {
            Debug.LogError(exception.Message +"\n" + exception.StackTrace);
            Debug.LogError(exception.Data);
            Debug.LogWarning("Scene loaded without Main menu Scene. GameSetup Script not Initialized \n For proper game initializaton use Main Menu Scene!");
        }

        //Debug.Log("sprite = " + playerList[0].GetComponentInChildren<SpriteRenderer>().sprite.name);
        
    }

    public GameSetup gameSetup;

    public GameObject playerPrefab;
    public List<Player> playerList;

    public void GameMode1Player()
    {
        GameObject temp = Instantiate(playerPrefab);
        playerList.Add(temp.GetComponent<Player>());

    }
    public void GameMode2Players()
    {
        GameObject temp;

        temp = Instantiate(playerPrefab);
        playerList.Add(temp.GetComponent<Player>());

        temp = Instantiate(playerPrefab);
        playerList.Add(temp.GetComponent<Player>());
    }


    private void Update()
    {
        /*
        if(Input.GetKeyDown("q"))
        {
            playerList[0].GetComponent<PlayerInput>().SwitchCurrentControlScheme("My_Scheme", Joystick.current);
        }
        if (Input.GetKeyDown("r"))
        {
            playerList[0].GetComponent<PlayerInput>().SwitchCurrentControlScheme("KeyBoard", Keyboard.current, Mouse.current);
        }
        */
    }




}
