using UnityEngine;
using UnityEngine.UI;

public class Boss_Area : MonoBehaviour
{
    [SerializeField] GameObject collidersBlockingExit;
    [SerializeField] GameObject placeToTeleportPlayersTo;
    [SerializeField] Enemy_BaseClass boss;
    [SerializeField] Collider2D areaCollider;

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
        areaCollider.enabled = false;
    }
}
