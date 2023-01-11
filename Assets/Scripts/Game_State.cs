using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State : MonoBehaviour
{
    public static Game_State ins;

    public static bool gamePaused;
    public static GameObject pausingWindow;

    public Player deadPlayer;
    bool following;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(following == true)
        {
            deadPlayer.transform.position = Player_Manager.ins.playerList[deadPlayer.id].transform.position;
        }
    }
    public static void PauseGame()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Time.timeScale = 0;
            pausingWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pausingWindow.SetActive(false);
        }
    }

    public void PlayerDeath(Player player)
    {
        if(GameSetup.ins.numberOfPlayers == 1)
        {

        }
        else
        {
            if(deadPlayer == null)
            {
                deadPlayer = player;
                following = true;
            }
            else
            {

            }
        }
    }
    public void PlayerRevive()
    {

    }

    
}
