using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GUARDATRES

public class Characther : MonoBehaviour
{
    public float speedMovement;
    protected float auxSpeedMovement;
    public float speedJump;
    public float life;
    [HideInInspector]
    public bool inFloor;
    protected bool die;
    [HideInInspector]
    public Rigidbody2D rig2D;
    public float RunVelocity;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        inFloor = false;
        die = false;
        auxSpeedMovement = speedMovement;
    }
    public virtual void CheckDead()
    {
        if (life <= 0)
        {
            die = true;
        }
    }
    public void CheckLife(float maxLife)
    {
        if(life >= maxLife)
        {
            life = maxLife;
        }
    }
    public void LeftMovement(bool runMovement)
    {
        float speed = speedMovement;
        if (runMovement)
        {
            speed = RunVelocity;
        }
        transform.position = transform.position - new Vector3(speed, 0, 0) * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
    public void RightMovement(bool runMovement)
    {
        float speed = speedMovement;
        if (runMovement)
        {
            speed = RunVelocity;
        }
        transform.position = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public Vector3 getPosition()
    {
        return this.transform.position;
    }
    public virtual void Attack(){ }
    public virtual void Movement(){ }
}
