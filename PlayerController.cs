using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject
{
    
    public float fJumpForce = 10f;
    public bool bGrounded = true;
    Vector3 mousePosition; //= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 direction; //= mousePosition - transform.position;
    float angle;
    Camera mainCamera;
    public GameObject WeaponPos;
    //bool bFiring;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    public override void Update()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        PlayerMovement();
        Jump();
        Fire();
        UpdateMousePosition();
        UpdateAnimationSate();
    }

    public void PlayerMovement()
    {
        float fMoveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(fMoveInput * fMoveSpeed, rb.velocity.y);
        if (fMoveInput != 0)
        {
            // Flip the sprite based on the direction
            transform.localScale = new Vector3(Mathf.Sign(fMoveInput), 1, 1);
        }
    }
    public bool Firing()
    {
        return Input.GetMouseButton(0);
    }
    //Are we moving right or left
    public bool IsMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0;
    }
    public void Jump()
    {
        if (bGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, fJumpForce);
            bGrounded = false;
        }
    }
    public void Fire()
    {
        if (Input.GetMouseButton(0)) WeaponPos.GetComponent<WeaponRotation>().Fire();
    }

    public void UpdateMousePosition()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public void UpdateAnimationSate()
    {
        //Standing Still
        //pointing up
        if(angle >= 0 && angle >= 330 && !IsMoving())
        {

        }
        //45 degree
        else if(angle <= 330 && angle >= 300 && !IsMoving())
        {

        }
        //forward
        else if(IsMoving())
        {
            if (Firing()) anim.SetInteger("iState", 3);
            else anim.SetInteger("iState", 2);
            Debug.Log("Anim state " + anim.GetInteger("iState"));
        }
        //Moving and aiming
        //Standing still
        else
        {
            anim.SetInteger("iState", 0);
            Debug.Log("State idle");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            bGrounded = true;
        }
    }
}
