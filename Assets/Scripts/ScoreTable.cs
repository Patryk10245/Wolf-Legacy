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
        if(ins == null)
        {
            ins = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public int gold;
    public int kills;
    public int currentlyCollectedGold;

    public GameObject GO_goldAmount;
    public Text TEXT_goldAmount;
    // Start is called before the first frame update
    void Awake()
    {
        Reference(); 
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
        TEXT_goldAmount.text = val.ToString();
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

    public void SetReferenceToGoldText()
    {

        GameObject temp = GameObject.Find("Canvas");
        
        foreach(Transform child in temp.transform)
        {
            if(child.gameObject.name == "Gold Icon")
            {
                TEXT_goldAmount = child.gameObject.GetComponentInChildren<Text>();
                GO_goldAmount = child.gameObject;
            }
        }
    }
}
