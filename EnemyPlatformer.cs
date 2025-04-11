using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatformer : EnemyBase
{
    //Enemy Platformer
    /*The base script for platofmer enemies. Unique scripts to define specific enemy behaviour
     * This enemy will travel along platforms
     * Types:
     * -Patrol: Will walk back and forth on a platform. If it detects an edge move opposite
     * -Kamikaze - Will just keep moving in the spawn direction
     * */
    public PlatformType ePlatType;
    RaycastHit2D frontRay;
    RaycastHit2D downRay;
    protected bool bMovingRight = true;
    bool bRight;
    protected Vector2 direction;
    protected Vector2 downRayDirection;
    [SerializeField]
    protected float fDetectDistance;
    public LayerMask platformLayer;//What is the tile map we should be looking out for
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //We should start facing the player
        FindPlayer();
        if (!InAttackDirection()) AboutFace();//Makes sure that the enemy spawns facing the player
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (ePlatType == PlatformType.Patrol) Patrol();
        else if (ePlatType == PlatformType.Kamikaze) MoveInDirection();
    }

    public enum PlatformType
    {
        Kamikaze,
        Patrol,
    }

    //Patrol on a platform and about-face when at an edge
    public void Patrol()
    {
        MoveInDirection();
        DrawDownRay();
        DrawFrontRay();
        if (downRay.collider == null) AboutFace();
        if (frontRay.collider != null) AboutFace();
    }

    public void MoveInDirection()
    {
        rb.velocity = new Vector2(bMovingRight ? fMoveSpeed : -fMoveSpeed, rb.velocity.y);
    }

    public void DrawFrontRay()
    {
        direction = bMovingRight ? Vector2.right : Vector2.left;
        frontRay = Physics2D.Raycast(transform.position, direction, 1.00f, platformLayer);
    }
    public void DrawDownRay()
    {

        // Determine the direction for the 45-degree Raycast
        //rayDirection = bRight ? new Vector2(1, -1).normalized : new Vector2(-1, -1).normalized;
        if (transform.localScale.x > 0)
        {
            downRayDirection = new Vector2(1, -1).normalized;  // 45-degree down-right
        }
        else
        {
            downRayDirection = new Vector2(-1, -1).normalized; // 45-degree down-left
        }
        downRay = Physics2D.Raycast(transform.position, downRayDirection, 2.00f, platformLayer);
        // Debug Ray to visualize the cast
        Debug.DrawRay(transform.position, downRayDirection * 2.00f, Color.green);
    }

    public void AboutFace()
    {
        bMovingRight = !bMovingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
