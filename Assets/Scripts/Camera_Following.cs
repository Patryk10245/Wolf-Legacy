using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following : MonoBehaviour
{
    public static Camera_Following ins;
    public void Reference()
    {
        ins = this;
    }

    public bool smooth;
    public bool flat;

    public bool singlePlayer;

    [Space(20)]

    public Vector3 middleOfPlayers;

    public Vector3 previous_pos;
    public Camera cam;
    public float dist;
    public float div_val = 3;
    public float max_dist = 30;

    public Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f;



    private void Start()
    {
        if(Camera_Following.ins == null)
        {
            Reference();
            DontDestroyOnLoad(this);
        }

        
    }

    void Update()
    {
        if(singlePlayer)
        {
            Single();
        }
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
            cam.orthographicSize = (dist / div_val) ;
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
            cam.orthographicSize = (dist / div_val) + 4;
        }
        else
        {
            cam.orthographicSize = 14;
        }

        cam.transform.position = middleOfPlayers;
    }

    void Single()
    {
        Vector3 pos = Player_Manager.ins.playerList[0].transform.position;
        pos.z = -10;
        cam.transform.position = pos;
        previous_pos = middleOfPlayers;
    }
}
