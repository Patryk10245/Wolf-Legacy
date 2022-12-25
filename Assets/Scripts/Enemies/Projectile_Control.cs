using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Control : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public Vector3 initial_pos;
    public float max_Fly_Distance;

    private void Update()
    {
        transform.Translate(dir * speed);

        if(Vector3.Distance(initial_pos, transform.position) > max_Fly_Distance)
        {
            Destroy(gameObject);
        }
    }
}
