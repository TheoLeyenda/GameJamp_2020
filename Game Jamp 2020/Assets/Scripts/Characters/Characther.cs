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
    // Start is called before the first frame update
    void Start()
    {
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
    public virtual void Movement(){ }
}
