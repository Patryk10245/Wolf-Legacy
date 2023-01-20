using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTable : MonoBehaviour
{
    public static ScoreTable ins;
    public void Reference()
    {
        ins = this;
    }
    public int gold;
    public int kills;
    public int currentlyCollectedGold;

    public GameObject GO_goldAmount;
    public Text TEXT_goldAmount;

    [Header("Gold Animation")]
    public float changeSpeed = 0.02f;
    bool animateGold;
    int lastValue;

    void Awake()
    {
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

    public void AddGold(int val)
    {
        currentlyCollectedGold += val;
        UpdateGold();
    }
    public void AddKill()
    {
        kills++;
    }

    public void UpdateGold()
    {
        int val = gold + currentlyCollectedGold;
        TEXT_goldAmount.text = currentlyCollectedGold.ToString();
    }

    public void ApplyCollectedGold()
    {
        gold += currentlyCollectedGold;
        currentlyCollectedGold = 0;
    }
    public void ReduceCollectedGoldByDeath()
    {
        currentlyCollectedGold /= 3;
    }
}
