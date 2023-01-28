using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_UI_Control : MonoBehaviour
{
    public List<GameObject> storyList;
    public int currentIndex;


    private void Start()
    {
        ShowStory();
    }

    public void ShowStory()
    {
        Game_State.firstRun = false;
        currentIndex = 0;
        storyList[currentIndex].SetActive(true);
    }

    public void NextStoryWindow()
    {
        storyList[currentIndex].SetActive(false);
        currentIndex++;
        storyList[currentIndex].SetActive(true);
    }
    public void EndShowingStory()
    {
        foreach(GameObject item in storyList)
        {
            item.SetActive(false);
        }

        Level_SelectedScenes.ins.LoadNextScene();
    }
}
