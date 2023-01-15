using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mage_ThunderObject : MonoBehaviour
{
    public float damage;
    bool dealDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player hit");
            Player player = collision.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }

    public void AnimationEvent_EnableDamage() // ENABLE FROM ANIMATION
    {
        dealDamage = true;
    }
    public void AnimationEvent_DisableDamage()// DISABLE FROM ANIMATION
    {
        dealDamage = false;
    }
    public void AnimationEvent_EndAnimation()
    {
        Destroy(gameObject);
    }
}
