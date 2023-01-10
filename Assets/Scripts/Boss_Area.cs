using UnityEngine;
using UnityEngine.UI;

public class Boss_Area : MonoBehaviour
{
    [SerializeField] GameObject collidersBlockingExit;
    [SerializeField] GameObject placeToTeleportPlayersTo;
    [SerializeField] Enemy_BaseClass boss;

    [SerializeField] GameObject bossHealthBar;



    void InitializeFightWithBoss()
    {
        foreach (Player player in Player_Manager.ins.playerList)
        {
            player.transform.position = placeToTeleportPlayersTo.transform.position;
        }
        collidersBlockingExit.SetActive(true);
        boss.SetMoveTarget(Player_Manager.ins.playerList[0]);
        bossHealthBar.SetActive(true);


        
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
    public void DeactivateBlockades()
    {
        collidersBlockingExit.SetActive(false);
    }
}
