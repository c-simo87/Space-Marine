using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHopper : EnemyPlatformer
{
    //Hopper enemy
    //Will hop in the direction of the player
    //Get the direction of the player
    //Hop in direction. Call animation events to properly set animation state of jump/fall etc.
    // Start is called before the first frame update
    public float fHopDistance;
    public float fHopHeight;
    public float fHopTimer;//Time between hops
    float fHopTimerReset;
    public override void Start()
    {
        base.Start();
        fHopTimerReset = fHopTimer;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        StartHopTimer();
        UpdateHorizontalMovement();
        CheckIfJumping();
        if (!InAttackDirection() && rb.velocity == Vector2.zero)//only face player if we are not jumping
        {
            AboutFace();
        }
    }

    public void SetHoppingState()
    {
        anim.SetBool("bIsJumping", true);
    }
    public void SetFallingState()
    {
        anim.SetBool("bIsJumping", false);
    }

    //Animatio control states ^
    public void CheckIfJumping()
    {
        if (rb.velocity.y > 0) SetHoppingState();
        else if (rb.velocity.y < 0) SetFallingState();
    }

    public void Hop()
    {
        // Determine direction based on the facing direction of the enemy
        float direction = Mathf.Sign(transform.localScale.x);
        // Set the horizontal velocity to move in the direction of the player
        rb.velocity = new Vector2(direction * fHopDistance, Mathf.Sqrt(2 * fHopHeight * Mathf.Abs(Physics2D.gravity.y)));
    }

    //This ensures that the enemy keeps moving horizontally when jumping
    private void UpdateHorizontalMovement()
    {
        if (rb.velocity.y > 0 || rb.velocity.y < 0)
        {
            float direction = Mathf.Sign(transform.localScale.x);
            rb.velocity = new Vector2(direction * fHopDistance, rb.velocity.y);
        }
    }

    public void StartHopTimer()
    {
        fHopTimer -= Time.deltaTime;
        if (fHopTimer < 0)
        {
            fHopTimer = fHopTimerReset;
            Hop();
        }
    }
}
