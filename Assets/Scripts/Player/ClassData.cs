using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="classData", menuName = "Custom/ClassData")]
public class ClassData : ScriptableObject
{
    public float healtPoints;
    public float energyPoints;
    public float energyRegenAmount;
    public float damage;
    public float speed;

    public Sprite playerSprite;
    public Sprite weaponSprite;

    public float dashForce;
    public float dashRechargeTime;
    public float dashEnergyCost;
    public float dashTime;
}
