using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characther : MonoBehaviour
{
    public float speedMovement;
    protected float auxSpeedMovement;
    public float speedJump;
    public int life;
    protected bool inFloor;
    protected bool die;
    [HideInInspector]
    public Rigidbody2D rig2D;
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        inFloor = false;
        die = false;
        auxSpeedMovement = speedMovement;
    }
    public void CheckDead()
    {
        if (life <= 0)
        {
            die = true;
        }
    }
    public void LeftMovement()
    {
        transform.position = transform.position - new Vector3(speedMovement, 0, 0) * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public void RightMovement()
    {
        transform.position = transform.position + new Vector3(speedMovement, 0, 0) * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public virtual void Attack(){ }
    public virtual void Movement(){ }
}
