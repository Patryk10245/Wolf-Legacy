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

    public int currentGold;
    public int killed0Enemies;
    // Start is called before the first frame update
    void Start()
    {
        Reference();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGold(int val)
    {
        currentGold += val;
    }
}
