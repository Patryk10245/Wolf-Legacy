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
    bool isDead;
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
    public AudioManager audioManager;
    
    [SerializeField] public List<PlayerSelectedData> playingPlayers;
    public int numberOfPlayers;
    public bool gameAlreadySetup = false;

    [Header("Class Data")]
    [SerializeField] ClassData[] classesData;
    [SerializeField] RuntimeAnimatorController[] controllers;
    [SerializeField] RuntimeAnimatorController[] weaponControllers;
    [Header("Paladin")]
    [SerializeField] GameObject paladinAbilityTrail;
    [Header("Barbarian")]
    [SerializeField] BoxCollider2D weaponCollider;
    [Header("Mage")]
    [SerializeField] GameObject mageProjectilePrefab;
    [SerializeField] GameObject fireTrailPrefab;
    [SerializeField] GameObject fireCirclePrefab;
    [Header("Archer")]
    [SerializeField] GameObject archerProjectilePrefab;
    [SerializeField] Sprite archersHandImage;


    private void Awake()
    {
        if(ins == null)
        {
            Reference();
            DontDestroyOnLoad(this);
        }

        if (ins != null && ins != this)
        {

            Destroy(gameObject);
        }
        else
        {
            Reference();
            DontDestroyOnLoad(this);
        }

    }

    void SetReferencesForPlayer1(Player newPlayer, Level_FightReferenecs references)
    {
        newPlayer.ui_updater.healthBar = references.playerUIControl.player1HealthBar;
        newPlayer.ui_updater.energyBar = references.playerUIControl.player1EnergyBar;
        newPlayer.id = 0;
        newPlayer.ui_updater.ability1Image = references.playerUIControl.player1Ability1;
        newPlayer.ui_updater.ability2Image = references.playerUIControl.player1Ability2;
        newPlayer.playerClass = playingPlayers[0].selectedClass;
    }
    void SetReferencesForPlayer2(Player newPlayer, Level_FightReferenecs references)
    {
        newPlayer.ui_updater.healthBar = references.playerUIControl.player2HealthBar;
        newPlayer.ui_updater.energyBar = references.playerUIControl.player2EnergyBar;
        newPlayer.id = 1;
        newPlayer.ui_updater.ability1Image = references.playerUIControl.player2Ability1;
        newPlayer.ui_updater.ability2Image = references.playerUIControl.player2Ability2;
        newPlayer.playerClass = playingPlayers[1].selectedClass;
    }

    public void SetUpTheGame()
    {
        Level_FightReferenecs levelReferences = Level_FightReferenecs.ins;
        Game_State.ins.pausingWindow = levelReferences.playerUIControl.pauseWindow;
        Game_State.ins.ResetValuesToDefault();



        playerManager = levelReferences.playerManager;
        playerManager.ResetValuesToDefault();
        cameraFollowing = levelReferences.cameraFollowing;
        levelReferences.resurrection.playerManager = playerManager;
        playerInputManager = levelReferences.playerInputManager;
        playerInputManager.playerPrefab = playerManager.playerPrefab;
        scoreTable.TEXT_goldAmount = levelReferences.playerUIControl.pauseGameGoldText;

        if (numberOfPlayers == 1)
        {
            PlayerInput newPlayer = playerInputManager.JoinPlayer(0, 0, playingPlayers[0].controlScheme);
            playerManager.playerList.Add(newPlayer.GetComponent<Player>());
            newPlayer.gameObject.transform.position = playerManager.playerSpawnPosition.position;
            SetReferencesForPlayer1(playerManager.playerList[0], levelReferences);
     
            cameraFollowing.singlePlayer = true;
            cameraFollowing.flat = false;
            levelReferences.playerUIControl.player2UI.SetActive(false);

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
            newPlayer.gameObject.transform.position = playerManager.playerSpawnPosition.position;

        }
        else if(numberOfPlayers == 2)
        {
            PlayerInput newPlayer1 = playerInputManager.JoinPlayer(0, 0, playingPlayers[0].controlScheme);
            playerManager.playerList.Add(newPlayer1.GetComponent<Player>());   
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

            newPlayer1.gameObject.transform.position = playerManager.playerSpawnPosition.position + new Vector3(-0.5f,0,0);

            try
            {
                PlayerInput newPlayer2 = playerInputManager.JoinPlayer(1,0,playingPlayers[1].controlScheme);
                playerManager.playerList.Add(newPlayer2.gameObject.GetComponent<Player>());               
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
                newPlayer2.gameObject.transform.position = playerManager.playerSpawnPosition.position + new Vector3(0.5f, 0, 0);
            }
            catch(Exception execption)
            {
                numberOfPlayers = 1;
                cameraFollowing.singlePlayer = true;
                cameraFollowing.flat = false;
                levelReferences.playerUIControl.player2UI.SetActive(false);
                return;
            }         

            cameraFollowing.singlePlayer = false;
            cameraFollowing.flat = true;
        }
    }

    void PaladinSetup(Player player)
    {
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
        player.controller.shield.enabled = true;

        foreach(Transform child in player.controller.weaponAnimator.transform)
        {
            if(child.name == "Arm")
            {
                Destroy(child.gameObject);
            }
        }

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
        around.baseDamage = player.stats.damage;
    }
    void BarbarianSetup(Player player)
    {
        ClassUpgrades upgrades = Village_Upgrades.ins.barbarianUpgrades;
        ClassData barbarianData = classesData[1];
        player.stats.currentHealth = barbarianData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.maxHealth = barbarianData.healtPoints + upgrades.health.valueOnLevel[upgrades.health.currentLevel];
        player.stats.currentEnergy = barbarianData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.maxEnergy = barbarianData.energyPoints + upgrades.energy.valueOnLevel[upgrades.energy.currentLevel];
        player.stats.damage = barbarianData.damage + upgrades.damage.valueOnLevel[upgrades.damage.currentLevel];
        player.stats.energyRegenerationAmount = barbarianData.energyRegenAmount + upgrades.energyRegeneration.valueOnLevel[upgrades.energyRegeneration.currentLevel];

        player.controller.moveSpeed = classesData[0].speed + upgrades.speed.valueOnLevel[upgrades.speed.currentLevel];
        player.controller.weaponCollider.GetComponent<SpriteRenderer>().sprite = barbarianData.weaponSprite;
        player.controller.weaponAnimator.runtimeAnimatorController = weaponControllers[1];
        player.GetComponent<Animator>().runtimeAnimatorController = controllers[1];

        BoxCollider2D weaponCollider = player.controller.weaponCollider.GetComponent<BoxCollider2D>();
        weaponCollider.size = new Vector2(1.2f, 1.80f);
        weaponCollider.offset = new Vector2(0, 0);

        foreach (Transform child in player.controller.weaponAnimator.transform)
        {
            if (child.name == "Arm")
            {
                Destroy(child.gameObject);
            }
        }

        Player_BarbarianAttack attack = player.gameObject.AddComponent<Player_BarbarianAttack>();
        attack.player = player;
        player.attackScript = attack;

        Player_Barbarian_Leap leap = player.gameObject.AddComponent<Player_Barbarian_Leap>();
        player.abilityBasic = leap;
        leap.player = player;
        leap.leapDamage = player.stats.damage * leap.damageMultiplier;

        Player_BarbarianDamageIncrease increase = player.gameObject.AddComponent<Player_BarbarianDamageIncrease>();
        increase.player = player;
        player.abilitySecondary = increase;
        increase.basePlayerDamage = player.stats.damage;
    }

        void RangerSetup(Player player)
    {
        ClassUpgrades upgrades = Village_Upgrades.ins.archerUpgrades;
        ClassData rangerData = classesData[2];
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

        foreach (Transform child in player.controller.weaponAnimator.transform)
        {
            if (child.name == "Arm")
            {
                Destroy(child.gameObject);
            }
        }


        Player_RangerAttack attack = player.gameObject.AddComponent<Player_RangerAttack>();
        player.attackScript = attack;
        attack.player = player;
        attack.projectilePrefab = archerProjectilePrefab;
        attack.spawnProjectilePosition = player.controller.weaponCollider.transform;
        SpriteRenderer archersHand = player.controller.weaponAnimator.gameObject.GetComponent<SpriteRenderer>();
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
        multiShot.damage = player.stats.damage * multiShot.damageMultiplier;


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

        foreach (Transform child in player.controller.weaponAnimator.transform)
        {
            if (child.name == "Arm")
            {
                Destroy(child.gameObject);
            }
        }

        Player_MageAttack attack = player.gameObject.AddComponent<Player_MageAttack>();
        player.attackScript = attack;
        attack.player = player;
        attack.projectilePrefab = mageProjectilePrefab;
        attack.spawnProjectilePosition = player.controller.weaponCollider.transform;

        Player_Mage_FireTrail fireTrail = player.gameObject.AddComponent<Player_Mage_FireTrail>();
        fireTrail.trailObject = fireTrailPrefab;
        fireTrail.player = player;
        player.abilityBasic = fireTrail;
        fireTrail.trailDamage = player.stats.damage * fireTrail.damageMultiplier;

        Player_Mage_FireCircle fireCircle = player.gameObject.AddComponent<Player_Mage_FireCircle>();
        fireCircle.player = player;
        player.abilitySecondary = fireCircle;
        fireCircle.circleDamage = player.stats.damage * fireCircle.damageMultiplier;
        fireCircle.prefabObject = fireCirclePrefab;
    }



    public void LoadClassData()
    {
        scoreTable.gold = PlayerPrefs.GetInt("Gold", 0);
        Game_State.firstRun = Convert.ToBoolean(PlayerPrefs.GetInt("FirstRun", 1));

        villageUpgrades.paladinBuildingBought = Convert.ToBoolean(PlayerPrefs.GetInt("PaladinBought", 0));
        villageUpgrades.barbarianBuildingBought = Convert.ToBoolean(PlayerPrefs.GetInt("BarbarianBought", 0));
        villageUpgrades.archerBuildingBought = Convert.ToBoolean(PlayerPrefs.GetInt("ArcherBought", 0));
        villageUpgrades.mageBuildingBought = Convert.ToBoolean(PlayerPrefs.GetInt("MageBought", 0));

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

        villageUpgrades.archerUpgrades.damage.currentLevel = PlayerPrefs.GetInt("RangerDamage", 0);
        villageUpgrades.archerUpgrades.health.currentLevel = PlayerPrefs.GetInt("RangerPaladinHealth", 0);
        villageUpgrades.archerUpgrades.energy.currentLevel = PlayerPrefs.GetInt("RangerEnergy", 0);
        villageUpgrades.archerUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("RangerRegen", 0);
        villageUpgrades.archerUpgrades.speed.currentLevel = PlayerPrefs.GetInt("RangerSpeed", 0);

        villageUpgrades.mageUpgrades.damage.currentLevel = PlayerPrefs.GetInt("MageDamage", 0);
        villageUpgrades.mageUpgrades.health.currentLevel = PlayerPrefs.GetInt("MageHealth", 0);
        villageUpgrades.mageUpgrades.energy.currentLevel = PlayerPrefs.GetInt("MageEnergy", 0);
        villageUpgrades.mageUpgrades.energyRegeneration.currentLevel = PlayerPrefs.GetInt("MageRegen", 0);
        villageUpgrades.mageUpgrades.speed.currentLevel = PlayerPrefs.GetInt("MageSpeed", 0);
    }
    public void SaveClassData()
    {
        PlayerPrefs.SetInt("Gold", scoreTable.gold);
        PlayerPrefs.SetInt("FirstRun", Convert.ToInt32(Game_State.firstRun));

        PlayerPrefs.SetInt("PaladinBought", Convert.ToInt32(villageUpgrades.paladinBuildingBought));
        PlayerPrefs.SetInt("BarbarianBought", Convert.ToInt32(villageUpgrades.barbarianBuildingBought));
        PlayerPrefs.SetInt("ArcherBought", Convert.ToInt32(villageUpgrades.archerBuildingBought));
        PlayerPrefs.SetInt("MageBought", Convert.ToInt32(villageUpgrades.mageBuildingBought));

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

        PlayerPrefs.SetInt("RangerDamage", villageUpgrades.archerUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("RangerPaladinHealth", villageUpgrades.archerUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("RangerEnergy", villageUpgrades.archerUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("RangerRegen", villageUpgrades.archerUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("RangerSpeed", villageUpgrades.archerUpgrades.speed.currentLevel);

        PlayerPrefs.SetInt("MageDamage", villageUpgrades.mageUpgrades.damage.currentLevel);
        PlayerPrefs.SetInt("MageHealth", villageUpgrades.mageUpgrades.health.currentLevel);
        PlayerPrefs.SetInt("MageEnergy", villageUpgrades.mageUpgrades.energy.currentLevel);
        PlayerPrefs.SetInt("MageRegen", villageUpgrades.mageUpgrades.energyRegeneration.currentLevel);
        PlayerPrefs.SetInt("MageSpeed", villageUpgrades.mageUpgrades.speed.currentLevel);

        PlayerPrefs.Save();
    }
}
