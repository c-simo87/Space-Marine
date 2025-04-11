using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySentryWalker : EnemyPlatformer
{
    int iAttackCount;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckAttackTime();
    }
    public void CheckAttackTime()
    {
        if (fAttackTimer > 0)
        {
            fAttackTimer -= Time.deltaTime;
            base.Update();
        }
        else if (fAttackTimer <= 0)//if we are facing the player, attack, else keep moving
        {
            if(InAttackDirection())
                TriggerAttack();
        }
    }
    public void AttackCounter()
    {
        if (iAttackCount < 3) iAttackCount++;
        else
        {
            ResetAttackTimer();
            iAttackCount = 0;
            anim.SetTrigger("tMove");
        }
    }

}
