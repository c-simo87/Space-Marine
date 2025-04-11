using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float fMoveSpeed = 5f;
    public float fLiveTime = 10f;
    public int iDamage;
    public bool bHoming;
    public float fRotationSpeed;
    string sMyTag;
    string myLayer;
    int layerMask;
    public float fCollisionTimer = 0.10f;//Used to prevent the projectile immediately colliding with platforms after fired.
    protected Rigidbody2D rb;
    GameObject target;//used for homing weapons
    // Start is called before the first frame update
    public virtual void Start()
    {
        sMyTag = this.gameObject.tag;
        layerMask = gameObject.layer;
        myLayer = LayerMask.LayerToName(layerMask);
    }

    // Update is called once per frame
    void Update()
    {
        ReduceTimer();
        Move();
        if (bHoming) HomeIn();
    }
    public void Move()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, fMoveSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
    public void ReduceTimer()
    {
        fLiveTime -= Time.deltaTime;
        if (fCollisionTimer >= 0) fCollisionTimer -= Time.deltaTime;
        if (fLiveTime <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fCollisionTimer >= 0 && collision.gameObject.layer == LayerMask.NameToLayer("Platform")) return;//wait for the timer if hitting the platform
        //Not ourselves or friendlies
        if (this.gameObject.layer == collision.gameObject.layer) return;
        if (sMyTag != collision.gameObject.tag)
        {
            if (sMyTag == "Ally" && collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<BaseObject>().TakeDamage(iDamage);
                Debug.Log("Called destroyed in Ally collided with Enemy");
                Destroy(gameObject);
            }
            else if (sMyTag == "Enemy" && (collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Player")))
            {
                collision.GetComponent<BaseObject>().TakeDamage(iDamage);
                Debug.Log("Called destroyed in Enemy collided with Ally or Player");
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Platform"))
            {
                //destroy self, hit a platform
                Debug.Log("Called destroyed in collided with Platform");
                Destroy(gameObject);
            }
        }
        else return;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (fCollisionTimer >= 0 && collision.gameObject.layer == LayerMask.NameToLayer("Platform")) return;//wait for the timer if hitting the platform
        //Not ourselves or friendlies
        if (this.gameObject.layer == collision.gameObject.layer) return;
        if (sMyTag != collision.gameObject.tag)
        {
            if (sMyTag == "Ally" && collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<BaseObject>().TakeDamage(iDamage);
                Debug.Log("Called destroyed in Ally collided with Enemy");
                Destroy(gameObject);
            }
            else if (sMyTag == "Enemy" && (collision.gameObject.CompareTag("Ally") || collision.gameObject.CompareTag("Player")))
            {
                collision.GetComponent<BaseObject>().TakeDamage(iDamage);
                Debug.Log("Called destroyed in Enemy collided with Ally or Player");
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Platform"))
            {
                //destroy self, hit a platform
                Debug.Log("Called destroyed in collided with Platform");
                Destroy(gameObject);
            }
        }
        else return;

    }

    public void HomeIn()
    {
        Vector3 dir = target.transform.position - transform.position;//myRB.transform.position;//
        dir.Normalize();
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, fRotationSpeed * Time.deltaTime);
    }

    public void SetTarget(GameObject t) { target = t; }
}
