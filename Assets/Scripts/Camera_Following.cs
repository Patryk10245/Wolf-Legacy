using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following : MonoBehaviour
{
    public Vector3 middleOfPlayers;
    public Camera cam;
    // Update is called once per frame
    void Update()
    {
        middleOfPlayers = Vector3.zero;
        foreach(Player player in Player_Manager.ins.playerList)
        {
            middleOfPlayers += player.transform.position;
        }
        middleOfPlayers /= Player_Manager.ins.playerList.Count;
        middleOfPlayers.z = -10;
        cam.transform.position = middleOfPlayers;


    }
}
