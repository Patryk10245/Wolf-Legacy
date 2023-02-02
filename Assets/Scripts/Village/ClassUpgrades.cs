using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeLevel
{
    public float[] valueOnLevel;
    public int[] cost;
    public int currentLevel;
}

[CreateAssetMenu(fileName = "CharacterUpgrades", menuName = "Custom/CharacterUpgrade")]
[System.Serializable]
public class ClassUpgrades : ScriptableObject
{
    public UpgradeLevel damage;
    public UpgradeLevel health;
    public UpgradeLevel energy;
    public UpgradeLevel energyRegeneration;
    public UpgradeLevel speed;

    public void ClearData()
    {
        damage.currentLevel = 0;
        health.currentLevel = 0;
        energy.currentLevel = 0;
        energyRegeneration.currentLevel = 0;
        speed.currentLevel = 0;
    }
}
