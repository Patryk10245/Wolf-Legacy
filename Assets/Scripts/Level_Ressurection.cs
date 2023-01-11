using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Ressurection : MonoBehaviour
{
    public static Level_Ressurection ins;
    public GameObject spawnPosition;

    public Player deadPlayer;
    public Player_Manager playerManager;
    int idAlivePlayer;
    int idDeadPlayer;
    bool following;
    // Start is called before the first frame update
    void Awake()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            PlayerDeath(playerManager.playerList[1]);
        }
        if (Input.GetKeyDown("o"))
        {
            PlayerRevive();
        }


        if (following == true)
        {
            playerManager.playerList[idDeadPlayer].transform.position = playerManager.playerList[idAlivePlayer].transform.position;
        }
    }
    public void PlayerDeath(Player player)
    {
        Debug.Log("Player death " + player);
        if(playerManager.playerList.Count == 1)
        {
            Debug.LogWarning("GAME OVER SCREEN");
            // Game Over Screen
        }
        else
        {
            if(deadPlayer == null)
            {
                deadPlayer = player;
                deadPlayer.isDead = true;
                deadPlayer.gameObject.SetActive(false);
                AssignIds();
                following = true;
            }
        }
    }
    public void PlayerRevive()
    {
        Debug.Log("Player revive ");
        playerManager.playerList[idDeadPlayer].transform.position = spawnPosition.transform.position;
        deadPlayer.isDead = false;
        deadPlayer.gameObject.SetActive(true);
        following = false;
        deadPlayer = null;
        deadPlayer.stats.currentHealth = deadPlayer.stats.maxHealth;
        deadPlayer.stats.currentEnergy = deadPlayer.stats.maxEnergy;
    }

    void AssignIds()
    {
        if (deadPlayer.id == 1)
        {
            idDeadPlayer = 1;
            idAlivePlayer = 0;
        }
        else
        {
            idDeadPlayer = 0;
            idAlivePlayer = 1;
        }
    }
}
