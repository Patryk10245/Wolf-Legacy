using UnityEngine;
using UnityEngine.UI;

public class Boss_Area : MonoBehaviour
{
    [SerializeField] GameObject collidersBlockingExit;
    [SerializeField] GameObject placeToTeleportPlayersTo;
    [SerializeField] Enemy_BaseClass boss;
    [SerializeField] Collider2D areaCollider;

    [SerializeField] GameObject bossHealthBar;

    int numberOfPlayersInArea;
    public GameObject windowAskingForPlayers;
    public Animator anim;
    public Transform initalBossMovePosition;



    void InitializeFightWithBoss()
    {
        if(numberOfPlayersInArea < GameSetup.ins.numberOfPlayers)
        {
            return;
        }
        CloseWindow();
        anim.SetTrigger("CloseTheGates");

        collidersBlockingExit.SetActive(true);
        boss.agent.SetDestination(initalBossMovePosition.position);
    }
    public void BeginFightWithBoss()
    {
        boss.SetMoveTarget(Player_Manager.ins.playerList[0]);
        bossHealthBar.SetActive(true);
    }

    void ShowWindow()
    {
        windowAskingForPlayers.SetActive(true);
    }
    void CloseWindow()
    {
        windowAskingForPlayers.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowWindow();
            numberOfPlayersInArea++;

            if(GameSetup.ins.numberOfPlayers == 2 && Level_Ressurection.ins.deadPlayer != null)
            {
                numberOfPlayersInArea++;
            }

            InitializeFightWithBoss();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            numberOfPlayersInArea--;
            CloseWindow();
        }
    }
    public void DeactivateBlockades()
    {
        anim.SetTrigger("OpenTheGates");
        collidersBlockingExit.SetActive(false);
        areaCollider.enabled = false;
    }
}
