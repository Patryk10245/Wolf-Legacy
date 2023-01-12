using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_LevaeArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            if(Level_FightReferenecs.ins.isLastMap)
            {
                Level_GameCompleted.ins.ShowFinishScreen();
                return;
            }
            Level_SelectedScenes.ins.ChangeToVillageScene();
        }
    }
}
