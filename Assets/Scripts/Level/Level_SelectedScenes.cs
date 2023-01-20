using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Level_SelectedScenes : MonoBehaviour
{
    public static Level_SelectedScenes ins;
    public int currentFightScene = -1;

    public string[] selectedScenes;
    public void Reference()
    {
        ins = this;
    }

    private void Awake()
    {
        if(ins == null)
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        if(ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Reference();
            DontDestroyOnLoad(this);
        }
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {     
        if(scene == SceneManager.GetSceneByName("Village_Scene"))
        {
            AudioManager.ins.Play_VillageMusic();
            Village_UI_Control.ins.SetReference();
            Village_Upgrades.ins.UpdateClassesUIUpgrades();
            ScoreTable.ins.ApplyCollectedGold();
        }
        else if(scene == SceneManager.GetSceneByName("Main_Menu_Scene"))
        {
            AudioManager.ins.Play_MenuMusic();
        }
        else
        {
            AudioManager.ins.Play_GameMusic();
            GameSetup.ins.SetUpTheGame();
        }
    }
    public void ChangeToVillageScene()
    {
        foreach(Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }

        SceneManager.LoadScene("Village_Scene");
        
    }
    public void ChangeToMainmenu()
    {
        foreach (Player player in Player_Manager.ins.playerList)
        {
            player.controller.RemoveListeningOnEvents();
        }
        SceneManager.LoadScene("Main_Menu_Scene");
    }
    public void RepeatFightMap()
    {
        SceneManager.LoadScene(selectedScenes[currentFightScene]);
    }
    public void LoadNextScene()
    {
        currentFightScene++;
        if(currentFightScene >= selectedScenes.Length)
        {
            ChangeToMainmenu();
            return;
        }

        SceneManager.LoadScene(selectedScenes[currentFightScene]);
    }

}
