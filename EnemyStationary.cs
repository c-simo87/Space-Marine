using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationary : EnemyBase
{
    /*
     * Stationary enemies do not move (obviously)
     * Stationary enemies will have an idle state, where they render their idle sprite
     * When enemy gets close -> Set awake trigger
     * when attack distance threshold is hit -> fire
     * When the target leaves attack distance -> trigger close state
     * */
    protected int iState;
    protected int iPrevState = -1;//ensure to trigger the animation only once
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Check attack distance. If player is in range, awake the target and vice versa
    public override void Update()
    {
        base.Update();
        switch (iState)
        {
            case 0://Waiting for player
                {
                    SetSleep();
                    if (InAttackDistance()) iState = 1;
                    break;
                }
            case 1://Awake state
                {
                    SetAwake();
                    if (!InAttackDistance()) iState = 0;
                    break;
                }
            default: { break; }
        }
        
    }

    public void SetAwake()
    {
        ResetAllTriggers();
        anim.SetTrigger("tAwake");
    }
    public void SetSleep()
    {
        ResetAllTriggers();
        anim.SetTrigger("tSleep");
    }

    public void ResetAllTriggers()
    {
        anim.ResetTrigger("tAwake");
        anim.ResetTrigger("tSleep");
        anim.ResetTrigger("tAttack");
    }

}
