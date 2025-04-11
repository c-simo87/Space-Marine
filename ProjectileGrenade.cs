using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGrenade : Projectile
{
    [SerializeField] protected float fLaunchForce,fLaunchHeight;
    bool bHasLaunched;
    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Start();
        LaunchGrenade();
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateMovement();
    }

    /*void LaunchGrenade()
    {
        // Determine direction based on the facing direction of the enemy
        float direction = Mathf.Sign(transform.localScale.x);
        // Get the angle of the gameobject (assuming Z-axis rotation for 2D)

        // Set the horizontal velocity to move in the direction of the player
        rb.velocity = new Vector2(direction * fLaunchForce, Mathf.Sqrt(2 * fLaunchHeight * Mathf.Abs(Physics2D.gravity.y)));

        // Calculate the velocity components using the angle and the launch force
        //float velocityX = Mathf.Cos(angle) * fLaunchForce * direction;
        //float velocityY = Mathf.Sin(angle) * fLaunchHeight;
        bHasLaunched = true;
    }*/

    private void LaunchGrenade()
    {
        // Calculate the launch direction based on the GameObject's rotation
        Vector2 launchDirection = transform.right;  // Use transform.right for 2D side scrollers
                                                    // Use transform.up if you want to fire upwards

        // Apply the launch velocity using the direction from the rotation
        rb.velocity = launchDirection * fLaunchForce + Vector2.up * fLaunchHeight;

        bHasLaunched = true;
    }

   /* private void UpdateMovement() {
        if (bHasLaunched && rb.velocity.y <= 0)
        {
            // After the launch, only update the horizontal movement, let gravity handle the vertical
            float direction = Mathf.Sign(transform.localScale.x);
            rb.velocity = new Vector2(direction * fLaunchForce, rb.velocity.y);
        }
    }*/
}
