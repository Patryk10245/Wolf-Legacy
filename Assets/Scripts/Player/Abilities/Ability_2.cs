using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability_2 : MonoBehaviour
{
    public Player player;
    public float energyCost = 8;
    public float rechargeTime = 4;
    public float timer;
    public bool isRecharching;
    public abstract void Use();
}
