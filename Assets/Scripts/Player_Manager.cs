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
        Debug.Log("Player manager Setting Reference To self");
        ins = this;
    }
    void Awake()
    {
        Reference();
    }

    public GameSetup gameSetup;

    public GameObject playerPrefab;
    public List<Player> playerList;
    public Transform playerSpawnPosition;



    private void Update()
    {
    }




}
