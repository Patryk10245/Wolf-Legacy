using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_UI_Control : MonoBehaviour
{
    public List<GameObject> storyList;
    public int currentIndex;

    public void ShowStory()
    {
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
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
