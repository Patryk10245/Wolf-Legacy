using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability_1 : MonoBehaviour
{
    public Player player;
    public float energyCost;
    public float rechargeTime;
    public float timer;
    public bool isRecharching;
    public abstract void Use();
}
