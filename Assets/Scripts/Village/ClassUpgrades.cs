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
}
