using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_GameOver : MonoBehaviour
{

    public void InitiateGameOver()
    {
        ScoreTable.ins.ReduceCollectedGoldByDeath();
        ScoreTable.ins.ApplyCollectedGold();
        Level_FightReferenecs.ins.deathScreen.SetActive(true);
        GameSetup.ins.SaveClassData();
    }
}
