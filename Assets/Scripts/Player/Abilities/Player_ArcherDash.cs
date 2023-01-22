using UnityEngine;

public class Player_ArcherDash : Ability_1
{
    [Space(10)]
    public float dashForce = 6000;

    private void Start()
    {
        energyCost = 15;
        rechargeTime = 3;
    }
    public override void Use()
    {
        if (isRecharching == false & player.stats.currentEnergy >= energyCost)
        {
            Dash();
        }
    }
    private void Update()
    {
        if (isRecharching)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                isRecharching = false;
                player.ui_updater.Ability1Recharged();
                timer = 0;
            }
        }
    }
    void Dash()
    {
        Vector3 mousepos = player.controller.mousePos;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        Vector3 dir = (transform.position - mousepos).normalized;

        player.controller.rb.AddForce(dir * dashForce);
        isRecharching = true;
        player.ui_updater.Ability1Used();
    }


}
