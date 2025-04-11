using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public int iHealth;
    public float fMoveSpeed;
    float moveReset;
    public float fBlinkDuration;
    public float fBlinkInterval;
    public float fRotationSpeed;
    public Color cBlinkColor;
    private bool bBlinking;
    protected string sMyTag;
    int maxHealth;
    protected bool bInvulnerable;
    protected Animator anim;
    protected bool isInCameraView = false;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected SpriteRenderer sr;
    // Start is called before the first frame update
    public virtual void Start()
    {
        maxHealth = iHealth;
        moveReset = fMoveSpeed;
        sMyTag = this.gameObject.tag;
        if(anim == null) anim = GetComponentInChildren<Animator>();//check in the children hierarchy for the animator.
        if (anim == null) anim = GetComponent<Animator>();//check in base if not in children
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public void AddHealth(int h)
    {
        iHealth += h;
        if (iHealth > maxHealth) iHealth = maxHealth;
    }

    public void TakeDamage(int d)
    {
        if (bInvulnerable) return;
        iHealth -= d;
        if(!bBlinking)
            StartCoroutine(BlinkEffect());
        if (iHealth <= 0)
        {
            Stop();
            SetDeathAnim();
        }
    }

    public bool IsAlive()
    {
        return iHealth > 0;
    }

    public void SetInvulnerable()
    {
        bInvulnerable = true;
    }
    public void SetVulnerable()
    {
        bInvulnerable = false;
    }

    public void SetDeathAnim()
    {
        anim.SetTrigger("tDeath");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void Move()
    {
        fMoveSpeed = moveReset;
    }


    public void Stop()
    {
        fMoveSpeed = 0;
    }
    //Checks if the npc is off screen. Controls removing objects that are no longer in game camera
    public void CheckVisibility()
    {
        // Calculate the screen bounds in world units
        Camera camera = Camera.main;
        Vector3 viewportPosition = camera.WorldToViewportPoint(transform.position);
        bool currentlyInView = viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                                viewportPosition.y >= 0 && viewportPosition.y <= 1;

        if (currentlyInView && !isInCameraView)
        {
            // The enemy has just entered the camera view
            isInCameraView = true;
        }
        else if (!currentlyInView && isInCameraView)
        {
            // The enemy has just exited the camera view
            isInCameraView = false;
            Destroy(gameObject,1.0f);
        }
    }

    private IEnumerator BlinkEffect()
    {
        float elapsedTime = 0f;
        bool isBlinking = false;
        bBlinking = true;
        while (elapsedTime < fBlinkDuration)
        {
            elapsedTime += Time.deltaTime;
            isBlinking = !isBlinking;

            if (isBlinking)
            {
                sr.color = cBlinkColor;
            }
            else
            {
                sr.color = Color.white; // Reset to original color
            }

            yield return new WaitForSeconds(fBlinkInterval);
        }

        // Ensure the sprite is reset to normal
        sr.color = Color.white;
        bBlinking = false;
    }

}
