using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopup : EnemyStationary
{
    /*
     * Popup enemy adds a few extra states. 
     * The enemy pops up and then moves to an attack idle state
     * it then fires when the timer is up
     */
    float fAttackReset;
    public override void Start()
    {
        base.Start();
        fAttackReset = fAttackTimer;
    }
    public override void Update()
    {
        base.Update();
        this.GetComponent<SpriteRenderer>().flipX = GetPlayer().transform.position.x > this.transform.position.x;
        if (iState == 1)
        {
            fAttackTimer -= Time.deltaTime;
            if (fAttackTimer <= 0) SetAttack();
        }
    }



    public void SetAttack()
    {
        ResetAllTriggers();
        anim.SetTrigger("tAttack");
        fAttackTimer = fAttackReset;
    }
}
