using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    private Camera cam;
    private Vector3 offset; //
    //private Boss1Controller bossScr;
    //bool camSwitch = false;
    private float zoom = 20f;
    private float maxZoom = 45f;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.orthographicSize = zoom;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleZoom();
        //if(Input.GetKeyDown(KeyCode.P))
        //{

        //    if (camSwitch == false)
        //        camSwitch = true;
        //    else camSwitch = false;
        //    Debug.Log("The button was pressed and is now " + camSwitch);
        //
        if (player != null)
        {
            //transform.position = player.transform.position + offset;//as we move the player, the camera will move aligned with player will not spin
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        if (player == null)
        {
            player = FindPlayer();
            //GameObject r = GameObject.FindGameObjectWithTag("Respawn");
            //MoveToRespawn(r);
        }
        //if(camSwitch == true)
        //{
        //    bossScr = Object.FindObjectOfType<Boss1Controller>();
        //    if(bossScr != null)
        //    {
        //        if (bossScr.GetComponent<Boss1Controller>() == null)
        //        {
        //            bossScr = null;
        //            camSwitch = false;
        //        }
        //        else
        //        {
        //            transform.position = bossScr.transform.position + offset;
        //        }
        //    }
        //}
    }

    private void HandleZoom()
    {
        float changeAmount = 20f;
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            if (zoom > 0)
            {
                zoom -= changeAmount * Time.deltaTime;
                Camera.main.orthographicSize = zoom;
            }
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            if (zoom <= maxZoom)
            {
                zoom += changeAmount * Time.deltaTime;
                Camera.main.orthographicSize = zoom;
            }
        }
    }

    public void MoveToRespawn(GameObject g)
    {
        transform.position = Vector3.MoveTowards(transform.position, g.transform.position, 15 * Time.deltaTime);
    }

    public void SetFreeMovement()
    {

    }

    public GameObject FindPlayer()
    {
        return GameObject.Find("Player");
    }
}
