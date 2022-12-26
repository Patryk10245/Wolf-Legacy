using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashSkill : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    [SerializeField] float dash_force;
    [SerializeField] float dash_time;
    float dash_timer;
    bool is_Dashing = false;
    [Space(15)]
    [SerializeField] float recharge_Time;
    float recharge_timer;
    bool is_recharching = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        AttemptToDash();
        Dash();
        DashRecharge();
    }

    void DashRecharge()
    {
        if (is_recharching == true)
        {
            recharge_timer += Time.deltaTime;
            if (recharge_timer >= recharge_Time)
            {
                is_recharching = false;
                recharge_timer = 0;
            }
        }
    }
    void AttemptToDash()
    {
        if (Input.GetKeyDown("l"))
        {
            if (is_recharching == false)
            {
                is_Dashing = true;
                player.controller.can_Input = false;
            }
        }
    }
    void Dash()
    {
        if (is_Dashing == true)
        {
            dash_timer += Time.deltaTime;
            if (dash_timer >= dash_time)
            {
                dash_timer = 0;
                is_Dashing = false;
                player.controller.can_Input = true;
                is_recharching = true;
            }

            rb.AddForce(player.controller.moveInput * dash_force);
        }
    }
}