using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BaseObject
{
    protected GameObject oPlayer;
    public float fAttackDistance;//How close the enemy is to attack
    [SerializeField]
    protected float fAttackTimer;//used for customary attack timing
    float fAttackTimeReset;
    public CollisionBehaviour collisionType;
    public int iDamageOnContact;
    [SerializeField] protected List<GameObject> children = new List<GameObject>();
    [SerializeField] protected float fPatrolTime;//Controls direction switch if on patrol mode
    protected float patrolReset;
    protected bool bReadyToAttack;//Animation states will determine when the enemy is ready to attack
    [SerializeField] protected float fLifetTime; //used in scenarios where we dont want the enemy alive too long,used for rolling bombs really
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetChildrenWeapons();
        patrolReset = fPatrolTime;
        fAttackTimeReset = fAttackTimer;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (oPlayer == null) FindPlayer();
        CheckAttack();
        CheckLifeTimer();
    }

    //Where is the player and set the weapons to target the player
    public void FindPlayer()
    {
        oPlayer = GameObject.Find("Player");
        if (oPlayer != null) foreach (GameObject c in children) if (c.GetComponent<WeaponRotation>() != null) c.GetComponent<WeaponRotation>().SetTarget(oPlayer);
    }

    public void SetChildrenWeapons()
    {
        foreach(Transform child in this.gameObject.transform)
        {
            children.Add(child.gameObject);
        }
    }

    //Animation will call this normally. When the awake anim is complete, start attack and set the sprite accordingly
    public void SetReadyToAttack()
    {
        bReadyToAttack = true;
    }

    public void SetNotReadyToAttack()
    {
        bReadyToAttack = false;
    }

    //Use the weapon position objects timer to fire
    public void CheckAttack()
    {
        if (bReadyToAttack) foreach (GameObject c in children) if(c.GetComponent<WeaponRotation>() != null)c.GetComponent<WeaponRotation>().Fire();
    }

    public bool InAttackDistance()
    {
        return Vector2.Distance(transform.position,oPlayer.transform.position) < fAttackDistance;
    }

    public bool InAttackDirection()
    {
        Vector2 direction = GetPlayer().transform.position - transform.position;
        bool bRight = transform.localScale.x > 0;
        if (bRight && direction.x > 0) return true;
        else if (!bRight && direction.x < 0) return true;
        return false;
    }

    public bool InAttackAngle()
    {
        // Get the direction to the player
        Vector2 directionToPlayer = (GetPlayer().transform.position - transform.position).normalized;

        // Determine the enemy's facing direction (right or left)
        Vector2 facingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Calculate the angle between the facing direction and direction to the player
        float angle = Vector2.Angle(facingDirection, directionToPlayer);

        // Define the maximum field of view (FOV) for the enemy, e.g., 180 degrees
        float maxFOV = 180f;

        // Check if the player is within the field of view (half the FOV in either direction)
        return angle <= maxFOV / 2;
    }

    //Return true if we are facing right
    public bool FacingDirection()
    {
        bool bFacingRight = false;
        if (transform.localScale.x > 0) bFacingRight = true;
        else if (transform.localScale.x < 0) bFacingRight = false;
        return bFacingRight;
    }

    public GameObject GetPlayer()
    {
        return oPlayer;
    }

    //Called from animation event
    public void Fire()
    {
        foreach(GameObject c in children)
            c.GetComponentInChildren<WeaponRotation>().Fire();
    }

    //Will move without rotation
    public void MoveTowardsTarget()
    {
        if (oPlayer == null)
            return;
        // Calculate the direction vector from the NPC to the target
        Vector3 direction = (oPlayer.transform.position - transform.position).normalized;

        // Move the NPC towards the target
        Vector3 moveAmount = direction * fMoveSpeed * Time.deltaTime;
        transform.position += moveAmount;
    }

    // Flip the sprite along the x-axis. Make sure the move direction is set accordingly to prevent incorrect sprite direction
    public void FlipSprite()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public enum CollisionBehaviour
    {
        Push,//No damage, but keep moving
        Bomb,//blows up and damages collision object
        Hurt,//hurt the player and keep moving
        HurtAndWait//hurt the player, wait and resume.
    }
    public void TriggerAttack()
    {
        anim.SetTrigger("tAttack");
    }
    public void CheckLifeTimer()
    {
        if(fLifetTime > 0)
        {
            fLifetTime -= Time.deltaTime;
            if(fLifetTime <= 0)
            {
                SetDeathAnim();
            }
        }
    }

    public void AttackTimerCountdown()
    {
        if (fAttackTimer > 0) fAttackTimer -= Time.deltaTime;
    }
    public bool ReadyToAttack()
    {
        return fAttackTimer <= 0;
    }
    public void ResetAttackTimer()
    {
        fAttackTimer = fAttackTimeReset;
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == sMyTag || collision.gameObject.layer == LayerMask.NameToLayer("Platform")) return;
        switch (collisionType)
        {
            case CollisionBehaviour.Hurt:
                {
                    collision.gameObject.GetComponent<BaseObject>().TakeDamage(iDamageOnContact);
                    break;
                }
            case CollisionBehaviour.Bomb:
                {
                    collision.gameObject.GetComponent<BaseObject>().TakeDamage(iDamageOnContact);
                    anim.SetTrigger("tDeath");
                    break;
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
