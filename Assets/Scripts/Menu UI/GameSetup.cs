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
    UI_MainMenuControl menuControl;
    [SerializeField]public List<PlayerSelectedData> playingPlayers;
    int numberOfPlayers;

    private void Start()
    {
        menuControl = GetComponent<UI_MainMenuControl>();
    }
    public void OnePlayerSetup()
    {
        playingPlayers.Clear();

        numberOfPlayers = 1;
        PlayerSelectedData player = new PlayerSelectedData();
        playingPlayers.Add(player);
        player.id = 0;
    }
    public void TwoPlayersSetup()
    {
        playingPlayers.Clear();


        numberOfPlayers = 2;
        PlayerSelectedData player1 = new PlayerSelectedData();
        player1.id = 0;
        PlayerSelectedData player2 = new PlayerSelectedData();
        player2.id = 1;

        playingPlayers.Add(player1);
        playingPlayers.Add(player2);
    }
    

    public void ChoosePaladin(int id)
    {
        playingPlayers[id].selectedClass = ENUM_PlayerClass.Paladin;
        Debug.Log("choose paladin");
    }
    public void ChooseBarbarian(int id)
    {
        playingPlayers[id].selectedClass = ENUM_PlayerClass.Barbarian;
        Debug.Log("choose barbarian");
    }
    public void ChooseRanger(int id)
    {
        playingPlayers[id].selectedClass = ENUM_PlayerClass.Ranger;
        Debug.Log("choose ranger");
    }
    public void ChooseMage(int id)
    {
        playingPlayers[id].selectedClass = ENUM_PlayerClass.Mage;
        Debug.Log("choose mage");
    }

    public void StartGame()
    {
        Debug.Log("start game");
        
    }

}
