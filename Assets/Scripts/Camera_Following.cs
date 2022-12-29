using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following : MonoBehaviour
{
    public bool smooth;
    public bool flat;

    [Space(20)]

    public Vector3 middleOfPlayers;

    public Vector3 previous_pos;
    public Camera cam;
    public float dist;
    public float div_val = 3;
    public float max_dist = 30;

    public Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if (smooth) Smoothed();
        if (flat) Flated();
    }

    void Smoothed()
    {
        middleOfPlayers = Vector3.zero;
        foreach (Player player in Player_Manager.ins.playerList)
        {
            middleOfPlayers += player.transform.position;
        }

        dist = Vector3.Distance(Player_Manager.ins.playerList[0].transform.position, Player_Manager.ins.playerList[1].transform.position);
        middleOfPlayers /= Player_Manager.ins.playerList.Count;
        middleOfPlayers.z = -10;

        if (dist > max_dist)
        {
            cam.orthographicSize = (dist / div_val);
        }
        else
        {
            cam.orthographicSize = 10;
        }

        cam.transform.position = Vector3.SmoothDamp(previous_pos, middleOfPlayers, ref velocity, smoothTime);
        previous_pos = middleOfPlayers;

    }
    void Flated()
    {
        middleOfPlayers = Vector3.zero;
        foreach (Player player in Player_Manager.ins.playerList)
        {
            middleOfPlayers += player.transform.position;
        }

        dist = Vector3.Distance(Player_Manager.ins.playerList[0].transform.position, Player_Manager.ins.playerList[1].transform.position);
        middleOfPlayers /= Player_Manager.ins.playerList.Count;
        middleOfPlayers.z = -10;

        if (dist > max_dist)
        {
            cam.orthographicSize = (dist / div_val);
        }
        else
        {
            cam.orthographicSize = 10;
        }

        cam.transform.position = middleOfPlayers;
    }
}
