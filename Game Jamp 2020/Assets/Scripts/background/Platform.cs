using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : PlatformMovement
{
    private Rigidbody2D rig2D;
    public PlatformMovement PlatformMovement;
    private GameObject player;
    private GameObject platformChild;
    private float actualTime;

    [Header("Tipo de movimiento")]
    public PlatformMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        platformChild = GameObject.FindWithTag("Floor");
        actualTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(actualTime> loopTimeMovement)
        {
            actualTime++;
            movePlatformLateralLeft();
        }
        else
        {
            actualTime++;
            movePlatformLateralRight();
        };
        if (actualTime > (loopTimeMovement*2))
        {
            actualTime = 0;
        }
        

    }

    void movePlatformLateralRight()
    {
        platformChild.transform.position = platformChild.transform.position + new Vector3(speedMovement, 0, 0) * Time.deltaTime;
    }

    void movePlatformLateralLeft()
    {
        platformChild.transform.position = platformChild.transform.position - new Vector3(speedMovement, 0, 0) * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entro");
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2d(Collision2D collision)
    {
        Debug.Log("Salio");
        player.transform.parent = null;
    }
}


