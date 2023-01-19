using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable_Object : MonoBehaviour
{
    public GameObject objectToSpawnOnDeath;
    public GameObject particle;
    public void DestroyMe()
    {
        Debug.LogWarning("Play Particle");
        if(objectToSpawnOnDeath != null)
        {
            Instantiate(objectToSpawnOnDeath, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(particle, gameObject.transform.position, gameObject.transform.rotation);
        }
        Destroy(gameObject);
    }
}
