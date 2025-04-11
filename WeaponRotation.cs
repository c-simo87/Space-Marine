using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public bool bIsPlayer;
    public bool bCanRotate;
    public GameObject projectile;
    public float fCoolDown;
    float fResetCoolDown;
    public float maxRotation = 90f; // Maximum rotation angle in degrees
    public float fRotationSpeed;
    private Transform playerTransform;
    GameObject target;//for AI use
    // Start is called before the first frame update
    void Start()
    {
        fResetCoolDown = fCoolDown;
        playerTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        //If we are the player, use the mouse targeting mechanics, else, rotate towards our AI's target
        if (bIsPlayer) MousePosition();
        else if(target!=null && bCanRotate) RotateTowards(target);
        CoolDownTimer();
    }
    public void MousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        //direction.z = 0;     // Ensure it's on the 2D plane
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Adjust the angle based on the player's facing direction
        /*if (playerTransform.localScale.x < 0) // Player facing left
        {
            //angle+=180; // Mirror the angle horizontally
            angle = Mathf.Clamp(angle, 90f, 270f);
        }
        else angle = Mathf.Clamp(angle, -90f, 90f);*/

        // Clamp the angle to the desired range
        //angle = Mathf.Clamp(angle, -maxRotation, maxRotation);
        //Adjust the angle according to sprite position
        angle -= 90;
        /*if (angle < -90f || angle > 90f)
        {
            angle = Mathf.Clamp(angle, -90f, 90f);
        }*/
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//
        Debug.DrawLine(transform.position, transform.position + transform.up * 2f, Color.red); // Adjust the length as needed

    }


    public void Fire()
    {
        if (fCoolDown <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            fCoolDown = fResetCoolDown;
        }
    }
    public void CoolDownTimer()
    {
        if (fCoolDown > 0) fCoolDown -= Time.deltaTime;
    }

    public void RotateTowards(GameObject go)
    {
        Vector3 dir = go.transform.position - transform.position;//myRB.transform.position;//
        dir.Normalize();
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, fRotationSpeed * Time.deltaTime);
    }

    public void AttackTarget(GameObject g)
    {
        if (bCanRotate) RotateTowards(g);
        Vector3 directionToTarget = g.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget);
        if(hit.collider != null && hit.collider.gameObject == g) { Fire(); }//if the raycash is hitting its target, lets fire
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public void SetRotation()
    {

    }
}
