using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
