using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rig2D;
    [Header("Tipo de movimiento")]
    private GameObject player;
    private float actualTime;
    public float loopTimeMovment;
    public float speedMovement;
    public MovementType movementType;


    public enum MovementType
    {
        Horizontal, 
        Vertical,
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        actualTime = 0;
}

    // Update is called once per frame
    void Update()
    {
        switch (movementType)
        {
            case MovementType.Horizontal:
                MoveHorizontal();
                break;
            case MovementType.Vertical:
                MoveVertical();
                break;
        }

    }


    private void MoveHorizontal()
    {
        if (actualTime > loopTimeMovment)
        {
            actualTime++;
            movePlatformLateralLeft();
        }
        else
        {
            actualTime++;
            movePlatformLateralRight();
        };
        if (actualTime > (loopTimeMovment * 2))
        {
            actualTime = 0;
        }
    }


    private void MoveVertical()
    {
        if (actualTime > loopTimeMovment)
        {
            actualTime++;
            movePlataformUp();
        }
        else
        {
            actualTime++;
            movePlatformDown();
        };
        if (actualTime > (loopTimeMovment * 2))
        {
            actualTime = 0;
        }
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

    void movePlatformLateralRight()
    {
        this.transform.position = this.transform.position + new Vector3(speedMovement, 0, 0) * Time.deltaTime;
    }

    void movePlatformLateralLeft()
    {
        this.transform.position = this.transform.position - new Vector3(speedMovement, 0, 0) * Time.deltaTime;
    }


    void movePlataformUp()
    {
        this.transform.position = this.transform.position + new Vector3(0, speedMovement, 0) * Time.deltaTime;
    }

    void movePlatformDown()
    {
        this.transform.position = this.transform.position - new Vector3(0, speedMovement, 0) * Time.deltaTime;
    }
}


