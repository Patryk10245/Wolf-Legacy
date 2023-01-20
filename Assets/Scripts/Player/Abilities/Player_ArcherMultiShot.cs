using UnityEngine;

public class Player_ArcherMultiShot : Ability_2
{
    [Space(10)]
    public float damageMultiplier = 1.5f;
    public float damage = 4;
    public float arrowSpeed = 1500;
    public float timeBetweenArrows = 0.1f;

    public int arrowsToShot = 6;
    int arrowsAlreadyShot;
    public GameObject arrowPrefab;
    public GameObject arrowSpawnPosiiton;

    bool isShooting;

    private void Start()
    {
        energyCost = 10;
        rechargeTime = 6;
    }

    public override void Use()
    {
        if (isRecharching == false && player.stats.currentEnergy >= energyCost)
        {
            MultiShot();
        }
    }

    void Update()
    {
        if (isRecharching)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                isRecharching = false;
                timer = 0;
                player.ui_updater.Ability2Recharged();
            }
        }

        if (isShooting)
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenArrows)
            {
                timer -= timeBetweenArrows;
                arrowsAlreadyShot++;
                GameObject arrow = Instantiate(arrowPrefab);
                arrow.transform.position = arrowSpawnPosiiton.transform.position;
                Player_Projectile proj = arrow.GetComponent<Player_Projectile>();

                Vector3 mousepos = player.controller.mousePos;
                mousepos.z = Camera.main.nearClipPlane;
                mousepos = Camera.main.ScreenToWorldPoint(mousepos);
                Vector3 dir = (mousepos - arrowSpawnPosiiton.transform.position);
                Vector3 directionNormalized = Vector3.Normalize(dir);

                proj.rb.AddForce(directionNormalized * arrowSpeed);
                proj.flyDirection = dir;
                proj.speed = arrowSpeed;
                proj.damage = damage;
                proj.stopTimerAt = 4;
                proj.player = player;
            }

            if (arrowsAlreadyShot >= arrowsToShot)
            {
                arrowsAlreadyShot = 0;
                timer = 0;
                isShooting = false;
                isRecharching = true;
                player.ui_updater.Ability2Used();
            }
        }
    }
    void MultiShot()
    {
        isShooting = true;
        timer = 0;
        arrowsAlreadyShot = 0;
    }
}
