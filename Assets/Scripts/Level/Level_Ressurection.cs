using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Level_Ressurection : MonoBehaviour
{
    public static Level_Ressurection ins;
    public GameObject spawnPosition;
    public Player deadPlayer;
    public Player_Manager playerManager;
    [Space(10)]
    [SerializeField] int revivingCost = 100;
    public GameObject revivingInfoWindow;
    public Text reviveInfoCostText;
    int idAlivePlayer;
    int idDeadPlayer;
    bool following;

    bool playerInRessurectionArea;


    void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        reviveInfoCostText.text = revivingCost.ToString();
    }

    void Update()
    {
        if (following == true)
        {
            playerManager.playerList[idDeadPlayer].transform.position = playerManager.playerList[idAlivePlayer].transform.position;
        }
    }
    public void PlayerDeath(Player player)
    {
        if(playerManager.playerList.Count == 1)
        {         
            GameOver();
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
            else
            {
                player.isDead = true;
                GameOver();
            }
        }
    }
    public void PlayerRevive()
    {
        if(ScoreTable.ins.currentlyCollectedGold >= revivingCost)
        {
            playerManager.playerList[idDeadPlayer].transform.position = spawnPosition.transform.position;
            deadPlayer.isDead = false;
            deadPlayer.gameObject.SetActive(true);
            following = false;
            deadPlayer.stats.currentHealth = deadPlayer.stats.maxHealth;
            deadPlayer.stats.currentEnergy = deadPlayer.stats.maxEnergy;
            deadPlayer.isInvulnerable = false;
            deadPlayer = null;
            ScoreTable.ins.AddGold(-revivingCost);
            revivingInfoWindow.SetActive(false);
        }
        
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

    void GameOver()
    {
        Game_State.levelLost = true;
        Level_PlayerUI_Control.ins.InitiateGameOver();
    }

    public void PlayerRessurection(InputAction.CallbackContext context)
    {
        PlayerRevive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(deadPlayer != null)
            {
                playerInRessurectionArea = true;
                Player player = collision.gameObject.GetComponent<Player>();
                player.controller.playerInput.currentActionMap.FindAction("Attack").started += PlayerRessurection;
                revivingInfoWindow.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRessurectionArea = false;
            Player player = collision.gameObject.GetComponent<Player>();
            player.controller.playerInput.currentActionMap.FindAction("Attack").started -= PlayerRessurection;
            revivingInfoWindow.SetActive(false);
        }
    }
}
