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

    public void RandomizeMaps()
    {
        List<int> temp_list = new List<int>();
        temp_list.Add(1);
        temp_list.Add(2);
        temp_list.Add(3);
        int map1, map2, map3;

        int rand1 = Random.Range(0, 3);
        map1 = temp_list[rand1];
        temp_list.Remove(map1);

        int rand2 = Random.Range(0, 2);
        map2 = temp_list[rand2];
        temp_list.Remove(map2);

        map3 = temp_list[0];
        selectedScenes[0] = "Level " + map1.ToString();
        selectedScenes[2] = "Level " + map2.ToString();
        selectedScenes[4] = "Level " + map3.ToString();
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        if(scene == SceneManager.GetSceneByName("Village_Scene"))
        {
            AudioManager.ins.Play_VillageMusic();
            Village_UI_Control.ins.SetReference();
            Village_Upgrades.ins.UpdateClassesUIUpgrades();
            ScoreTable.ins.ApplyCollectedGold();

            if(Game_State.ins.CheckIfGameCompleted() == true)
            {
                Village_UI_Control.ins.ShowEndingScreen();
            }

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
