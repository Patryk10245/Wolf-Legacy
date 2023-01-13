using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mage_ThunderObject : MonoBehaviour
{
    public float damage;
    bool dealDamage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(dealDamage == false)
        {
            return;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
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
}
