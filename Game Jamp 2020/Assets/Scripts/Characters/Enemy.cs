using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characther
{
    // Start is called before the first frame update
    public float Damage;

    public float timeMovement;
    public float auxTimeMovement;
    public bool enableMovement = true;
    protected bool normalMovement = true;
    protected FSM fsm;
    protected Player player;
    [SerializeField]
    protected StateMovement stateMovement;
    public enum StateMovement
    {
        Left,
        Right,
        Jump,
    }
    protected virtual void Update()
    {
        if (enableMovement)
        {
            Movement();
        }
        
    }
    public override void Movement()
    {
        if (normalMovement)
        {
            switch (stateMovement)
            {
                case StateMovement.Left:
                    transform.position = transform.position + new Vector3(speedMovement, 0, 0) * Time.deltaTime;
                    break;
                case StateMovement.Right:
                    transform.position = transform.position - new Vector3(speedMovement, 0, 0) * Time.deltaTime;
                    break;
                case StateMovement.Jump:
                    if(stateMovement == StateMovement.Left)
                    {
                        float auxSpeed = speedMovement = speedMovement / 2f;
                        transform.position = transform.position + new Vector3(auxSpeed, 0, 0) * Time.deltaTime;
                        rig2D.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
                    }
                    break;
            }
            CheckDirection();
        }
    }
    public void CheckDirection()
    {
        if(timeMovement > 0)
        {
            timeMovement = timeMovement - Time.deltaTime;
        }
        else if(timeMovement <= 0)
        {
            timeMovement = auxTimeMovement;
            switch (stateMovement)
            {
                case StateMovement.Left:
                    stateMovement = StateMovement.Right;
                    break;
                case StateMovement.Right:
                    stateMovement = StateMovement.Left;
                    break;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
