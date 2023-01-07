using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

    [SerializeField] Player_Manager playerManager;
    [SerializeField] Camera_Following cameraFollowing;
    [SerializeField] ScoreTable scoreTable;
    
    [SerializeField] public List<PlayerSelectedData> playingPlayers;
    public int numberOfPlayers;
    public bool gameAlreadySetup = false;

    [Header("Class Data")]
    [SerializeField] ClassData[] classesData;


    public void foo()
    {
        classesData[0].damage++;
    }
    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            foo();
        }
    }


    private void Start()
    {
        
    }
    private void Awake()
    {
        if(ins == null)
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }

    public void SetUpTheGame()
    {
        //Debug.Log("Setting up the game");
        playerManager = Player_Manager.ins;
        cameraFollowing = Camera_Following.ins;
        scoreTable = ScoreTable.ins;

        GameInitialization.ins.playerManager = playerManager;
        GameInitialization.ins.cameraFollowing = cameraFollowing;
        scoreTable.SetReferenceToGoldText();

        PlayerInputManager playerInputManager = playerManager.gameObject.GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playerManager.playerPrefab;
        PlayerInput newPlayer = playerInputManager.JoinPlayer();

        playerManager.playerList.Add(newPlayer.GetComponent<Player>());
        newPlayer.gameObject.transform.position = playerManager.playerSpawnPosition.position;

        //Debug.Log("number of players == " + numberOfPlayers);
        if (numberOfPlayers == 1)
        {
            
            cameraFollowing.singlePlayer = true;
            cameraFollowing.flat = false;
            cameraFollowing.smooth = false;
            //Destroy(playerManager.playerList[1].gameObject);
            //playerManager.playerList.RemoveAt(1);

            switch(playingPlayers[0].selectedClass)
            {
                case ENUM_PlayerClass.Paladin:
                    PaladinSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Barbarian:
                    BarbarianSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Ranger:
                    RangerSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Mage:
                    MageSetup(playerManager.playerList[0]);
                    break;
            }
        }
        else if(numberOfPlayers == 2)
        {
            cameraFollowing.singlePlayer = false;
            cameraFollowing.flat = true;
            cameraFollowing.smooth = false;

            switch (playingPlayers[0].selectedClass)
            {
                case ENUM_PlayerClass.Paladin:
                    PaladinSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Barbarian:
                    BarbarianSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Ranger:
                    RangerSetup(playerManager.playerList[0]);
                    break;
                case ENUM_PlayerClass.Mage:
                    MageSetup(playerManager.playerList[0]);
                    break;
            }

            switch (playingPlayers[0].selectedClass)
            {
                case ENUM_PlayerClass.Paladin:
                    PaladinSetup(playerManager.playerList[1]);
                    break;
                case ENUM_PlayerClass.Barbarian:
                    BarbarianSetup(playerManager.playerList[1]);
                    break;
                case ENUM_PlayerClass.Ranger:
                    RangerSetup(playerManager.playerList[1]);
                    break;
                case ENUM_PlayerClass.Mage:
                    MageSetup(playerManager.playerList[1]);
                    break;
            }
        }
        gameAlreadySetup = true;

    }

    void PaladinSetup(Player player)
    {
        //Debug.Log("player name = " + player.gameObject.name);
        //Debug.Log("Paladin Setup");
        ClassUpgrades upgrades = Village_Upgrades.ins.paladinUpgrades;
        ClassData paladinData = classesData[0];
        player.stats.currentHealth = paladinData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.maxHealth = paladinData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.currentEnergy = paladinData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.maxEnergy = paladinData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.damage = paladinData.damage + upgrades.damage.valueOnLevel[upgrades.damage.currentLevel];
        player.stats.energyRegenerationAmount = paladinData.energyRegenAmount + upgrades.energyRegeneration.valueOnLevel[upgrades.energyRegeneration.currentLevel];

        player.controller.moveSpeed = classesData[0].speed + upgrades.speed.valueOnLevel[upgrades.speed.currentLevel];
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
