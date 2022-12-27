using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Area : MonoBehaviour
{
    [SerializeField] GameObject collidersBlockingExit;
    [SerializeField] GameObject placeToTeleportPlayersTo;


    void InitializeFightWithBoss()
    {
        // Teleport all players to area
        // Block all exits
        // Force boss to attack one of players
        // Fight
        // Loot
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            InitializeFightWithBoss();
        }
    }
}
