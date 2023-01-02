using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stop_Rotation : MonoBehaviour
{
    public Transform gracz;
    public NavMeshAgent agent;
    public bool follow;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

        
    void Update()
    {
        if(follow == true)
        {
            agent.SetDestination(gracz.position);
        }
        if(Input.GetKeyDown("space"))
        {
            follow = !follow;
        }
    }
}
