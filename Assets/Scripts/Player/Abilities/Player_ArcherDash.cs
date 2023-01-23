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
        mousepos.z = Camera.main.nearClipPlane;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        mousepos.z = transform.position.z;
        Vector3 direction = (transform.position - mousepos).normalized;


        player.controller.rb.AddForce(direction * dashForce);
        isRecharching = true;
        player.ui_updater.Ability1Used();
    }


}
