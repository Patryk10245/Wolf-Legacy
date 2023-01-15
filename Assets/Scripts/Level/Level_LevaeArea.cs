using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_LevaeArea : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ScoreTable.ins.BeginAnimatingGold();

            Level_GameCompleted.ins.ShowLevelCompletedScreen();
        }
    }


    
}
