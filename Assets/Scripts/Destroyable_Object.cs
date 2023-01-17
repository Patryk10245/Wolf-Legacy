using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable_Object : MonoBehaviour
{
    public GameObject objectToSpawnOnDeath;
    public void DestroyMe()
    {
        Debug.LogWarning("Play Particle");
        if(objectToSpawnOnDeath != null)
        {
            Instantiate(objectToSpawnOnDeath, gameObject.transform.position, gameObject.transform.rotation);
        }
        Destroy(gameObject);
    }
}