using System;
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
    public InputDevice device;
    public string controlScheme = "KeyBoard";
    public bool isDead;
}

public class GameSetup : MonoBehaviour
{
    public static GameSetup ins;
    void Reference()
    {
        ins = this;
    }

    Player_Manager playerManager;
    Camera_Following cameraFollowing;
    [SerializeField] ScoreTable scoreTable;
    public Village_Upgrades villageUpgrades;
    public PlayerInputManager playerInputManager;
    
    [SerializeField] public List<PlayerSelectedData> playingPlayers;
    public int numberOfPlayers;
    public bool gameAlreadySetup = false;

    [Header("Class Data")]
    [SerializeField] ClassData[] classesData;
    [SerializeField] RuntimeAnimatorController[] controllers;
    [SerializeField] RuntimeAnimatorController[] weaponControllers;
    [Header("Paladin")]
    [SerializeField] GameObject paladinAbilityTrail;
    [Header("Mage")]
    [SerializeField] GameObject mageProjectilePrefab;
    [SerializeField] GameObject fireTrailPrefab;
    [SerializeField] GameObject fireCirclePrefab;
    [Header("Archer")]
    [SerializeField] GameObject archerProjectilePrefab;
    [SerializeField] Sprite archersHandImage;



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
        newPlayer.ui_updater.energyBar = references.player1EnergyBar;
        newPlayer.id = 0;
        newPlayer.ui_updater.ability1Image = references.player1Ability1;
        newPlayer.ui_updater.ability2Image = references.player1Ability2;
        newPlayer.playerClass = playingPlayers[0].selectedClass;
    }
    void SetReferencesForPlayer2(Player newPlayer, Level_FightReferenecs references)
    {
        newPlayer.ui_updater.healthBar = references.player2HealthBar;
        newPlayer.ui_updater.energyBar = references.player2EnergyBar;
        newPlayer.id = 1;
        newPlayer.ui_updater.ability1Image = references.player2Ability1;
        newPlayer.ui_updater.ability2Image = references.player2Ability2;
        newPlayer.playerClass = playingPlayers[1].selectedClass;
    }

    public void SetUpTheGame()
    {

        Debug.Log("Setting up The Game");
        playingPlayers[0].isDead = false;

        //Debug.Log("Setting up the game");
        Level_FightReferenecs levelReferences = Level_FightReferenecs.ins;
        Game_State.ins.pausingWindow = levelReferences.pauseWindow;
        Game_State.ins.ResetValuesToDefault();
        //Debug.Log("level references = " + levelReferences.gameObject.name);

        playerManager = levelReferences.playerManager;
        playerManager.ResetValuesToDefault();
        cameraFollowing = levelReferences.cameraFollowing;
        levelReferences.resurrection.playerManager = playerManager;
        playerInputManager = levelReferences.playerInputManager;
        playerInputManager.playerPrefab = playerManager.playerPrefab;
        //Debug.Log("player manager = " + playerManager);

        //playerManager = Player_Manager.ins;
        //cameraFollowing = Camera_Following.ins;
        //scoreTable = ScoreTable.ins;


        scoreTable.TEXT_goldAmount = levelReferences.GoldTextIcon;
        //scoreTable.SetReferenceToGoldText();



        //Debug.Log("DEBUG  ||DO I SEE THIS ? = " + playerInputManager);
        //Debug.Log("number of players == " + numberOfPlayers);
        
        if (numberOfPlayers == 1)
        {
            if(playingPlayers.Count > 1)
            {
                playingPlayers.RemoveAt(1);
            }
            //PlayerInput newPlayer = playerInputManager.JoinPlayer();

            PlayerInput newPlayer = playerInputManager.JoinPlayer(0, 0, playingPlayers[0].controlScheme);
            newPlayer.gameObject.name = "player " + Time.realtimeSinceStartup;
            //Debug.Log("new player = " + newPlayer);
            playerManager.playerList.Add(newPlayer.GetComponent<Player>());
            newPlayer.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer1(playerManager.playerList[0], levelReferences);

            

            cameraFollowing.singlePlayer = true;
            cameraFollowing.flat = false;
            cameraFollowing.smooth = false;

            levelReferences.player2UI.SetActive(false);
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
            
            PlayerInput newPlayer1 = playerInputManager.JoinPlayer(0, 0, playingPlayers[0].controlScheme);
            //Debug.Log("new player = " + newPlayer1);
            playerManager.playerList.Add(newPlayer1.GetComponent<Player>());
            newPlayer1.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer1(playerManager.playerList[0], levelReferences);
            //Debug.Log("First Player Created");


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

            //Debug.Log("Player input manager set to = " + playerInputManager);
            //Debug.Log("Playing players 2 control scheme = " + playingPlayers[1].controlScheme);
            //Debug.Log("prefab = " + playerInputManager.playerPrefab.name);
            try
            {
                PlayerInput newPlayer2 = playerInputManager.JoinPlayer(1,0,playingPlayers[1].controlScheme);
                //Debug.Log("new player2 = " + newPlayer2);
                playerManager.playerList.Add(newPlayer2.gameObject.GetComponent<Player>());
                newPlayer2.gameObject.transform.position = playerManager.playerSpawnPosition.position;
                SetReferencesForPlayer2(playerManager.playerList[1], levelReferences);
                //Debug.Log("Second Player Created");
            }
            catch(Exception execption)
            {
                numberOfPlayers = 1;
                cameraFollowing.singlePlayer = true;
                cameraFollowing.flat = false;
                cameraFollowing.smooth = false;
                return;
                //Debug.Log(execption);
            }
            //Debug.Log(newPlayer2.name);
            


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
        player.controller.weaponAnimator.runtimeAnimatorController = weaponControllers[0];
        player.GetComponent<Animator>().runtimeAnimatorController = controllers[0];
        


        // Add Abilities
        Player_PaladinAttack attack = player.gameObject.AddComponent<Player_PaladinAttack>();
        player.attackScript = attack;
        attack.player = player;


        Player_Paladin_DashSkill dash = player.gameObject.AddComponent<Player_Paladin_DashSkill>();
        player.abilityBasic = dash;
        dash.player = player;


        Player_Paladin_AroundAttack around = player.gameObject.AddComponent<Player_Paladin_AroundAttack>();
        player.abilitySecondary = around;
        around.player = player;
        around.weaponHolder = player.controller.weaponHolder.gameObject;
        GameObject specialAttackTrail = Instantiate(paladinAbilityTrail);
        around.trail = specialAttackTrail;
        specialAttackTrail.transform.SetParent(player.controller.weaponCollider.transform);
        specialAttackTrail.transform.localPosition = new Vector3(0.49f, 0.07f, 0);


    }
    void BarbarianSetup(Player player)
    {

    }
    void RangerSetup(Player player)
    {
        ClassUpgrades upgrades = Village_Upgrades.ins.rangerUpgrades;
        ClassData rangerData = classesData[2];
        Debug.Log("upgrades = " + upgrades);
        Debug.Log("data = " + rangerData);
        Debug.Log("player = " + player);
        player.stats.currentHealth = rangerData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.maxHealth = rangerData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.currentEnergy = rangerData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.maxEnergy = rangerData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.damage = rangerData.damage + upgrades.damage.valueOnLevel[upgrades.damage.currentLevel];
        player.stats.energyRegenerationAmount = rangerData.energyRegenAmount + upgrades.energyRegeneration.valueOnLevel[upgrades.energyRegeneration.currentLevel];

        player.controller.moveSpeed = rangerData.speed + upgrades.speed.valueOnLevel[upgrades.speed.currentLevel];
        player.controller.weaponCollider.GetComponent<SpriteRenderer>().sprite = rangerData.weaponSprite;
        player.controller.weaponAnimator.runtimeAnimatorController = weaponControllers[2];
        player.GetComponent<Animator>().runtimeAnimatorController = controllers[2];

        Player_RangerAttack attack = player.gameObject.AddComponent<Player_RangerAttack>();
        player.attackScript = attack;
        attack.player = player;
        attack.projectilePrefab = mageProjectilePrefab;
        attack.spawnProjectilePosition = player.controller.weaponCollider.transform;
        SpriteRenderer archersHand = player.controller.weaponAnimator.gameObject.AddComponent<SpriteRenderer>();
        archersHand.sortingLayerName = "Player";
        archersHand.sortingOrder = 3;
        archersHand.sprite = archersHandImage;

        Player_ArcherDash dash = player.gameObject.AddComponent<Player_ArcherDash>();
        player.abilityBasic = dash;
        dash.player = player;

        Player_ArcherMultiShot multiShot = player.gameObject.AddComponent<Player_ArcherMultiShot>();
        player.abilitySecondary = multiShot;
        multiShot.player = player;
        multiShot.arrowPrefab = archerProjectilePrefab;
        multiShot.arrowSpawnPosiiton = player.controller.weaponCollider.gameObject;


    }
    void MageSetup(Player player)
    {
        ClassUpgrades upgrades = Village_Upgrades.ins.mageUpgrades;
        ClassData mageData = classesData[3];
        player.stats.currentHealth = mageData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.maxHealth = mageData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.currentEnergy = mageData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.maxEnergy = mageData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.damage = mageData.damage + upgrades.damage.valueOnLevel[upgrades.damage.currentLevel];
        player.stats.energyRegenerationAmount = mageData.energyRegenAmount + upgrades.energyRegeneration.valueOnLevel[upgrades.energyRegeneration.currentLevel];

        player.controller.moveSpeed = mageData.speed + upgrades.speed.valueOnLevel[upgrades.speed.currentLevel];
        player.controller.weaponCollider.GetComponent<SpriteRenderer>().sprite = mageData.weaponSprite;
        player.controller.weaponAnimator.runtimeAnimatorController = weaponControllers[3];
        player.GetComponent<Animator>().runtimeAnimatorController = controllers[3];

        Player_MageAttack attack = player.gameObject.AddComponent<Player_MageAttack>();
        player.attackScript = attack;
        attack.player = player;
        attack.projectilePrefab = mageProjectilePrefab;
        attack.spawnProjectilePosition = player.controller.weaponCollider.transform;

        Player_Mage_FireTrail fireTrail = player.gameObject.AddComponent<Player_Mage_FireTrail>();
        fireTrail.trailObject = fireTrailPrefab;
        fireTrail.player = player;
        player.abilityBasic = fireTrail;

        Player_Mage_FireCircle fireCircle = player.gameObject.AddComponent<Player_Mage_FireCircle>();
        fireCircle.circlePrefab = fireCirclePrefab;
        fireCircle.player = player;
        player.abilitySecondary = fireCircle;




    }



    public void LoadClassData()
    {
        Debug.LogWarning("LOADING CLASS DATA");

        scoreTable.gold = PlayerPrefs.GetInt("Gold", 0);

        villageUpgrades.paladinUpgrades.damage.currentLevel = PlayerPrefs.GetInt("PaladinDamage", 0);
        villageUpgrades.paladinUpgrades.health.currentLevel = PlayerPrefs.GetInt("PaladinHealth", 0);
        villageUpgrades.paladinUpgrades.energy.currentLevel = PlayerPrefs.GetInt("PaladinEnergy", 0);
        villageUpgrades.paladinUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("PaladinRegen", 0);
        villageUpgrades.paladinUpgrades.speed.currentLevel = PlayerPrefs.GetInt("PaladinSpeed", 0);

        villageUpgrades.barbarianUpgrades.damage.currentLevel = PlayerPrefs.GetInt("BarbarianDamage", 0);
        villageUpgrades.barbarianUpgrades.health.currentLevel = PlayerPrefs.GetInt("BarbarianPaladinHealth", 0);
        villageUpgrades.barbarianUpgrades.energy.currentLevel = PlayerPrefs.GetInt("BarbarianEnergy", 0);
        villageUpgrades.barbarianUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("BarbarianRegen", 0);
        villageUpgrades.barbarianUpgrades.speed.currentLevel = PlayerPrefs.GetInt("BarbarianSpeed", 0);

        villageUpgrades.rangerUpgrades.damage.currentLevel = PlayerPrefs.GetInt("RangerDamage", 0);
        villageUpgrades.rangerUpgrades.health.currentLevel = PlayerPrefs.GetInt("RangerPaladinHealth", 0);
        villageUpgrades.rangerUpgrades.energy.currentLevel = PlayerPrefs.GetInt("RangerEnergy", 0);
        villageUpgrades.rangerUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("RangerRegen", 0);
        villageUpgrades.rangerUpgrades.speed.currentLevel = PlayerPrefs.GetInt("RangerSpeed", 0);

        villageUpgrades.mageUpgrades.damage.currentLevel = PlayerPrefs.GetInt("MageDamage", 0);
        villageUpgrades.mageUpgrades.health.currentLevel = PlayerPrefs.GetInt("MageHealth", 0);
        villageUpgrades.mageUpgrades.energy.currentLevel = PlayerPrefs.GetInt("MageEnergy", 0);
        villageUpgrades.mageUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("MageRegen", 0);
        villageUpgrades.mageUpgrades.speed.currentLevel = PlayerPrefs.GetInt("MageSpeed", 0);
    }
    public void SaveClassData()
    {
        Debug.LogWarning("SAVING CLASS DATA");

        PlayerPrefs.SetInt("Gold", scoreTable.gold);

        PlayerPrefs.SetInt("PaladinDamage", villageUpgrades.paladinUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("PaladinHealth", villageUpgrades.paladinUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("PaladinEnergy", villageUpgrades.paladinUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("PaladinRegen", villageUpgrades.paladinUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("PaladinSpeed", villageUpgrades.paladinUpgrades.speed.currentLevel);

        PlayerPrefs.SetInt("BarbarianDamage", villageUpgrades.barbarianUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("BarbarianPaladinHealth", villageUpgrades.barbarianUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("BarbarianEnergy", villageUpgrades.barbarianUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("BarbarianRegen", villageUpgrades.barbarianUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("BarbarianSpeed", villageUpgrades.barbarianUpgrades.speed.currentLevel);

        PlayerPrefs.SetInt("RangerDamage", villageUpgrades.rangerUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("RangerPaladinHealth", villageUpgrades.rangerUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("RangerEnergy", villageUpgrades.rangerUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("RangerRegen", villageUpgrades.rangerUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("RangerSpeed", villageUpgrades.rangerUpgrades.speed.currentLevel);

        PlayerPrefs.SetInt("MageDamage", villageUpgrades.mageUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("MageHealth", villageUpgrades.mageUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("MageEnergy", villageUpgrades.mageUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("MageRegen", villageUpgrades.mageUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("MageSpeed", villageUpgrades.mageUpgrades.speed.currentLevel);

        PlayerPrefs.Save();
    }
}
