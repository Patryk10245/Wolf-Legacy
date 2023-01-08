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

    

    public int current_Gold;
    public int kills;

    public GameObject GO_goldAmount;
    public Text TEXT_goldAmount;
    // Start is called before the first frame update
    void Awake()
    {
        Reference(); 
    }
    private void Start()
    {
        //SetReferenceToGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int val)
    {
        current_Gold += val;
        UpdateGold();
    }
    public void AddKill()
    {
        kills++;
    }

    public void UpdateGold()
    {
        TEXT_goldAmount.text = current_Gold.ToString();
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
