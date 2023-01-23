using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FireCircleObject : MonoBehaviour
{
    public Player player;
    public float damage;
    public float expirationTime = 0.5f;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= expirationTime)
        {
            Destroy(gameObject);
        }
    }

}
