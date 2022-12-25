using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Manager : MonoBehaviour
{
    public static AI_Manager ins;
    public void Reference()
    {
        ins = this;
    }

    [SerializeField] Player player_1;
    [SerializeField] Player player_2;
    [SerializeField] List<Enemy_BaseClass> chasing_P1;
    [SerializeField] List<Enemy_BaseClass> chasing_P2;
    [SerializeField] List<Enemy_BaseClass> chasing_enemies;

    [SerializeField] float change_target_distance = 1;



    // Start is called before the first frame update
    void Start()
    {
        Reference();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWhoToChase();
        MoveTowardsPlayer();


    }

    public void ChasePlayer(Enemy_BaseClass enemy)
    {
        if (!chasing_enemies.Contains(enemy))
        {
            chasing_enemies.Add(enemy);
        }
    }
    public void StopChasing(Enemy_BaseClass enemy)
    {
        float distance_to_P1 = Vector3.Distance(player_1.transform.position, enemy.transform.position);
        float distance_to_P2 = Vector3.Distance(player_2.transform.position, enemy.transform.position);
        //Debug.Log("dist1 = " + distance_to_P1 + "\ndist2 = " + distance_to_P2);
        //Debug.Log("amount_of_players = " + enemy.player_Detection.amount_of_detected_players);
        //Debug.Log("me = " + enemy.name);

        if(enemy.player_Detection.amount_of_detected_players == 0)
        {
            //enemy.agent.SetDestination(enemy.transform.position);
            enemy.agent.velocity = Vector3.zero;

            chasing_enemies.Remove(enemy);
        }

        /*
        if (distance_to_P1 > enemy.range_To_Chase_Player && distance_to_P2 > enemy.range_To_Chase_Player)
        {
            enemy.agent.SetDestination(enemy.transform.position);
            
            chasing_enemies.Remove(enemy);
        }
        */
    }
    void CheckWhoToChase()
    {
        chasing_P1.Clear();
        chasing_P2.Clear();

        float distance_1;
        float distance_2;
        foreach (Enemy_BaseClass enemy in chasing_enemies)
        {
            distance_1 = Vector3.Distance(player_1.transform.position, enemy.transform.position);
            distance_2 = Vector3.Distance(player_2.transform.position, enemy.transform.position);


            if (distance_1 < distance_2 - change_target_distance)
            {
                //Debug.Log("blizej do 1");
                chasing_P1.Add(enemy);
                enemy.SetMoveTarget(player_1);
            }
            else if (distance_2 < distance_1 - change_target_distance)
            {
                //Debug.Log("blizej do 2");
                chasing_P2.Add(enemy);
                enemy.SetMoveTarget(player_2);
            }
            else
            {
                chasing_P1.Add(enemy);
                enemy.SetMoveTarget(player_1);
            }
        }
    }
    void MoveTowardsPlayer()
    {
        for (int i = 0; i < chasing_P1.Count; i++)
        {
            if (chasing_P1[i].distance_To_Player > chasing_P1[i].attack_Distance)
            {
                chasing_P1[i].agent.SetDestination(player_1.transform.position);
                //chasing_P1[i].SetMoveTarget(player_1);
            }
            else
            {
                chasing_P1[i].agent.velocity = Vector3.zero;
            }
            

            
            /*
            chasing_P1[i].agent.SetDestination(new Vector3(
                player_1.transform.position.x + surround_distance * Mathf.Cos(2 * Mathf.PI * i / chasing_P1.Count),
                player_1.transform.position.y + surround_distance * Mathf.Sin(2 * Mathf.PI * i / chasing_P1.Count),
                player_1.transform.position.z)
                );
            */
        }



        for (int i = 0; i < chasing_P2.Count; i++)
        {
            if (chasing_P1[i].distance_To_Player > chasing_P1[i].attack_Distance)
            {
                chasing_P2[i].agent.SetDestination(player_2.transform.position);
                //chasing_P2[i].SetMoveTarget(player_2);
            }
            else
            {
                chasing_P2[i].agent.velocity = Vector3.zero;
            }
            /*
            chasing_P2[i].agent.SetDestination(new Vector3(
                player_2.transform.position.x + surround_distance * Mathf.Cos(2 * Mathf.PI * i / chasing_P2.Count),
                player_2.transform.position.y + surround_distance * Mathf.Sin(2 * Mathf.PI * i / chasing_P2.Count),
                player_2.transform.position.z)
                );
            */
        }
    }
    public void RemoveFromChasing(Enemy_BaseClass enemy)
    {
        chasing_enemies.Remove(enemy);
        chasing_P1.Remove(enemy);
        chasing_P2.Remove(enemy);
    }
}
