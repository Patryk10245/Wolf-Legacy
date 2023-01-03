using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager ins;
    void Reference()
    {
        ins = this;
    }

    void Awake()
    {
        Reference();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        try
        {
            gameSetup = GameSetup.ins;
            gameSetup.SetUpTheGame();
        }
        catch(Exception exception)
        {
            Debug.LogWarning("Scene loaded without Main menu Scene. GameSetup Script not Initialized \n For proper game initializaton use Main Menu Scene!");
        }
        
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

    


}
