using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Village_Upgrades villageUpgrades;
    
    [SerializeField] public List<PlayerSelectedData> playingPlayers;
    public int numberOfPlayers;
    public bool gameAlreadySetup = false;

    [Header("Class Data")]
    [SerializeField] ClassData[] classesData;

    int x = 0;


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

    void SetReferencesForPlayer1(Player newPlayer, Level_FightReferenecs references)
    {
        newPlayer.ui_updater.healthBar = references.player1HealthBar;
        newPlayer.id = x;
        x++;
    }
    void SetReferencesForPlayer2(Player newPlayer, Level_FightReferenecs references)
    {
        newPlayer.ui_updater.healthBar = references.player2HealthBar;
    }

    public void SetUpTheGame()
    {
        //Debug.Log("Setting up the game");
        Level_FightReferenecs levelReferences = Level_FightReferenecs.ins;
        Debug.Log("level references = " + levelReferences.gameObject.name);

        playerManager = levelReferences.playerManager;
        cameraFollowing = levelReferences.cameraFollowing;
        Debug.Log("player manager = " + playerManager);

        PlayerInputManager playerInputManager = levelReferences.playerInputManager;
        playerInputManager.playerPrefab = playerManager.playerPrefab;

        //playerManager = Player_Manager.ins;
        //cameraFollowing = Camera_Following.ins;
        //scoreTable = ScoreTable.ins;

        //GameInitialization.ins.playerManager = playerManager;
        //GameInitialization.ins.cameraFollowing = cameraFollowing;


        scoreTable.SetReferenceToGoldText();
        
        

        Debug.Log("number of players == " + numberOfPlayers);
        if (numberOfPlayers == 1)
        {

            PlayerInput newPlayer = playerInputManager.JoinPlayer();
            playerManager.playerList.Add(newPlayer.GetComponent<Player>());
            newPlayer.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer1(playerManager.playerList[0], levelReferences);

            cameraFollowing.singlePlayer = true;
            cameraFollowing.flat = false;
            cameraFollowing.smooth = false;

            levelReferences.player2HealthBar.transform.parent.gameObject.SetActive(false);
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
            PlayerInput newPlayer1 = playerInputManager.JoinPlayer();
            playerManager.playerList.Add(newPlayer1.GetComponent<Player>());
            newPlayer1.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer1(playerManager.playerList[0], levelReferences);

            
            

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

            PlayerInput newPlayer2 = playerInputManager.JoinPlayer();
            Debug.Log(newPlayer2.name);
            playerManager.playerList.Add(newPlayer2.gameObject.GetComponent<Player>());
            newPlayer2.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer2(playerManager.playerList[1], levelReferences);


            switch (playingPlayers[1].selectedClass)
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

            cameraFollowing.singlePlayer = false;
            cameraFollowing.flat = true;
            cameraFollowing.smooth = false;
        }


        

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
