using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotobot : EnemyPlatformer
{
    bool bFacingRight;
    float fStartingRotation = 35f;
    float fFlippedRotation = 230f;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Attack();
        CheckFacingDirection();
    }

    public void Attack()
    {
        AttackTimerCountdown();
        if (ReadyToAttack() && InAttackDistance() && InAttackDirection())
        {
            TriggerAttack();
        }
        else if(!InAttackDistance()) anim.SetTrigger("tMove");

    }

    //Set the weapon position angle accordingly
    public void CheckFacingDirection()
    {
        if (FacingDirection()) foreach (GameObject c in children) c.transform.localRotation = Quaternion.Euler(0, 0, fStartingRotation);
        else foreach (GameObject c in children) c.transform.localRotation = Quaternion.Euler(0, 0, fFlippedRotation);
    }
}
