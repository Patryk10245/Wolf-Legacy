using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    public static ScoreTable ins;
    public void Reference()
    {
        ins = this;
    }

    

    public int current_Gold;
    public int kills;

    public Text goldAmount;
    // Start is called before the first frame update
    void Awake()
    {
        Reference();
        DontDestroyOnLoad(this);
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
        goldAmount.text = current_Gold.ToString();
    }
}
