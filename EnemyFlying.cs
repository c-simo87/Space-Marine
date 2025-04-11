using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : EnemyBase
{
    /*Flying enemy
     * Handles the base spawning behaviour of a flying enemy
     * When the enemy spawns, switch its direction to where the player is.
     * Behaviour states. 
     * 1-Spawn once - Enemy spawns, flys in direction and then is removed after off screen
     * 2.Patrol - Enemy will move left and right, while attacking
     * 3.Kamikaze - Enemy will fly towards player
     */
    public FlyingType eFlyingType;
    [SerializeField]
    protected Vector2 moveDirection;//Positive x -> left, Positive y ->up
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        switch (eFlyingType)
        {
            case FlyingType.SpawnOnce://Enemy spawns and moves in 'movedirection'. Once off screen, remove
                {
                    MoveInDirection();
                    CheckVisibility();
                    break;
                }
            case FlyingType.Patrol:
                {
                    Patrol();
                    break;
                }
            case FlyingType.Kamikaze:
                {
                    MoveTowardsTarget();
                    break;
                }
        }
    }
    public enum FlyingType
    {
        SpawnOnce,
        Patrol,
        Kamikaze
    }

    public void MoveInDirection()
    {
        Vector3 moveDirection3D = new Vector3(moveDirection.x, moveDirection.y, 0);

        // Move the NPC in the current direction
        transform.Translate(moveDirection3D * fMoveSpeed * Time.deltaTime);
    }

    public void Patrol()
    {
        MoveInDirection();
        fPatrolTime -= Time.deltaTime;
        if (fPatrolTime <= 0) {
            FlipSprite();
            moveDirection = new Vector3(-moveDirection.x, -moveDirection.y, 0);
            fPatrolTime = patrolReset;
        }
    }

}
