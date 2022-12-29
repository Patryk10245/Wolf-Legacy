using UnityEngine;

public class Boss_Area : MonoBehaviour
{
    [SerializeField] GameObject collidersBlockingExit;
    [SerializeField] GameObject placeToTeleportPlayersTo;
    [SerializeField] Enemy_Boss boss;



    void InitializeFightWithBoss()
    {
        foreach (Player player in Player_Manager.ins.playerList)
        {
            player.transform.position = placeToTeleportPlayersTo.transform.position;
        }
        collidersBlockingExit.SetActive(true);
        boss.SetMoveTarget(Player_Manager.ins.playerList[0]);
        
        // Teleport all players to area
        // Block all exits
        // Force boss to attack one of players
        // Fight
        // Loot
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InitializeFightWithBoss();
        }
    }
}
