using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTreadcore : EnemyPlatformer
{
    // Start is called before the first frame update
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
            if (InAttackDirection())
                TriggerAttack();
            else ResetAttackTimer();
        }
    }

}
