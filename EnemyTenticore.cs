using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The tenticore uses the patrol back and forth movement
//After the attack timer is reached, it will drop bomb at weapPos
public class EnemyTenticore : EnemyFlying
{
    public GameObject bomb;
    public GameObject weapPos;
    float fAttackReset;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        fAttackReset = fAttackTimer;
    }

    // Update is called once per frame
   public override void Update()
    {
        base.Update();
        AttackTimer();
    }

    public void Dropbomb()
    {
       GameObject b = Instantiate(bomb, weapPos.transform.position, transform.rotation);
    }
    public void AttackTimer()
    {
        fAttackTimer -= Time.deltaTime;
        if(fAttackTimer<= 0)
        {
            fAttackTimer = fAttackReset;
            anim.SetTrigger("tAttack");
        }
    }


}
