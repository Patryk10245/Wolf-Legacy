using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy_BaseClass
{


    public override void SetMoveTarget(Player target)
    {
        move_target = target.transform;
    }

    public override void TakeDamage(float val)
    {
        stats.TakeDamage(val);
    }

    protected override void AttackPlayer()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
