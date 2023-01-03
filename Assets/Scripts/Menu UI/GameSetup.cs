using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENUM_PlayerClass
{
    Paladin,
    Barbarian,
    Ranger,
    Mage
}


[System.Serializable]
public class PlayerSelectedData
{
    public int id;
    public ENUM_PlayerClass selectedClass;
}

public class GameSetup : MonoBehaviour
{
    public static GameSetup ins;
    void Reference()
    {
        ins = this;
    }

    UI_MainMenuControl menuControl;
    [SerializeField] Player_Manager playerManager;
    [SerializeField] Camera_Following cameraFollowing;
    [SerializeField] ScoreTable scoreTable;
    
    [SerializeField]public List<PlayerSelectedData> playingPlayers;
    public int numberOfPlayers;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private void Awake()
    {
        if(ins == null)
        {
            Reference();
        }
    }

    public void SetUpTheGame()
    {
        Debug.Log("Setting up the game");
        playerManager = Player_Manager.ins;
        cameraFollowing = Camera_Following.ins;
        scoreTable = ScoreTable.ins;

        switch (numberOfPlayers)
        {
            case 1:
                cameraFollowing.singlePlayer = true;
                cameraFollowing.flat = false;
                cameraFollowing.smooth = false;
                Destroy(playerManager.playerList[1].gameObject);
                playerManager.playerList.RemoveAt(1);

                // Set Up players class
                // Add abilites

                break;
            case 2:
                cameraFollowing.singlePlayer = false;
                cameraFollowing.flat = true;
                cameraFollowing.smooth = false;


                break;
            default:
                break;
        }
        
    }


    


}
