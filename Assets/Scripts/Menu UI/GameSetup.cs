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
    public bool gameAlreadySetup = false;

    [Header("Class Data")]
    [SerializeField] ClassData[] classesData;

    [SerializeField] Transform spawningPoint;



    public GameObject test;

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

                if(playingPlayers[0].selectedClass == ENUM_PlayerClass.Paladin)
                {
                    PaladinSetup(playerManager.playerList[0]);
                }
                else if(playingPlayers[0].selectedClass == ENUM_PlayerClass.Barbarian)
                {

                }
                else if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Ranger)
                {

                }
                else if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Mage)
                {

                }

                // Set Up players class
                // Add abilites

                break;
            case 2:
                cameraFollowing.singlePlayer = false;
                cameraFollowing.flat = true;
                cameraFollowing.smooth = false;


                if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Paladin)
                {
                    PaladinSetup(playerManager.playerList[0]);
                }
                else if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Barbarian)
                {
                    BarbarianSetup(playerManager.playerList[0]);
                }
                else if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Ranger)
                {
                    RangerSetup(playerManager.playerList[0]);
                }
                else if (playingPlayers[0].selectedClass == ENUM_PlayerClass.Mage)
                {
                    MageSetup(playerManager.playerList[0]);
                }

                if (playingPlayers[1].selectedClass == ENUM_PlayerClass.Paladin)
                {
                    PaladinSetup(playerManager.playerList[1]);
                }
                else if (playingPlayers[1].selectedClass == ENUM_PlayerClass.Barbarian)
                {
                    BarbarianSetup(playerManager.playerList[1]);
                }
                else if (playingPlayers[1].selectedClass == ENUM_PlayerClass.Ranger)
                {
                    RangerSetup(playerManager.playerList[1]);
                }
                else if (playingPlayers[1].selectedClass == ENUM_PlayerClass.Mage)
                {
                    MageSetup(playerManager.playerList[1]);
                }


                break;
            default:
                break;
        }

        


        gameAlreadySetup = true;

    }

    void PaladinSetup(Player player)
    {
        Debug.Log("player name = " + player.gameObject.name);
        ClassData paladinData = classesData[0];
        //Debug.Log("Paladin Setup");
        player.stats.currentHealth = paladinData.healtPoints;
        player.stats.maxHealth = paladinData.healtPoints;
        player.stats.currentEnergy = paladinData.energyPoints;
        player.stats.maxEnergy = paladinData.energyPoints;
        player.stats.damage = paladinData.damage;
        player.stats.energyRegenerationAmount = paladinData.energyRegenAmount;

        player.controller.moveSpeed = classesData[0].speed;
        player.controller.weaponCollider.GetComponent<SpriteRenderer>().sprite = paladinData.weaponSprite;

        // Add Abilities
        Player_PaladinAttack attack = player.gameObject.AddComponent<Player_PaladinAttack>();
        player.attackScript = attack;
        attack.player = player;
        player.attackScript = attack;

        Player_DashSkill dash = player.gameObject.AddComponent<Player_DashSkill>();
        player.abilityBasic = dash;
        dash.player = player;
        dash.dashForce = paladinData.dashForce;
        dash.dashRechargeTime = paladinData.dashRechargeTime;
        dash.dashEnergyCost = paladinData.dashEnergyCost;
        dash.dashTime = paladinData.dashTime;

        



    }
    void BarbarianSetup(Player player)
    {

    }
    void RangerSetup(Player player)
    {

    }
    void MageSetup(Player player)
    {

    }


}
