using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    public static ScoreTable ins;
    public void Reference()
    {
        ins = this;
    }

    public int current_Gold;
    public int kills;
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
    }
    public void AddKill()
    {
        kills++;
    }
}
