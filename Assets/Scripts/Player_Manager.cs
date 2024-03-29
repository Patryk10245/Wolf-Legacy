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

    public void DestroyAllPlayers()
    {
        foreach(Player player in playerList)
        {
            player.GetComponent<PlayerInput>().user.UnpairDevicesAndRemoveUser();
            Destroy(player.gameObject);
        }
    }

    public void ResetValuesToDefault()
    {
        foreach(Player player in playerList)
        {
            player.isDead = false;
        }
    }
}
