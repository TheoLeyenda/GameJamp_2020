using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characther
{
    // Start is called before the first frame update
    public int Damage;
    public float RangeOfVision;
    public float distanceAttack;
    public float distanceChase;
    public float timeMovement;
    public float auxTimeMovement;
    public float distaceDectectedFloor;
    public bool enableMovement = true;
    protected bool normalMovement = true;
    protected bool checkDirection = true;
    protected float distaceY = 0.5f;
    protected FSM fsm;
    protected Player player;
    protected Vector2 DistanceVector;

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
    private void Start()
    {
        GameObject go = GameObject.Find("Player");
        player = go.GetComponent<Player>();
    }
    public override void Movement()
    {
        if (normalMovement)
        {
            switch (stateMovement)
            {
                case StateMovement.Left:
                    LeftMovement();
                    break;
                case StateMovement.Right:
                    RightMovement();
                    break;
                case StateMovement.Jump:
                    if (stateMovement == StateMovement.Left)
                    {
                        float auxSpeed = speedMovement = speedMovement / 2f;
                        transform.position = transform.position + new Vector3(auxSpeed, 0, 0) * Time.deltaTime;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        rig2D.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
                    }
                    else if (stateMovement == StateMovement.Right)
                    {
                        float auxSpeed = speedMovement = speedMovement / 2f;
                        transform.position = transform.position - new Vector3(auxSpeed, 0, 0) * Time.deltaTime;
                        rig2D.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    break;
            }
            if (checkDirection)
            {
                CheckDirection();
            }
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
            ChageDirection();
        }
        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(transform.position.x + (transform.right.x * 1.5f), transform.position.y), Vector2.down, distaceDectectedFloor);
        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.tag != "Floor")
            {
                ChageDirection();
            }
        }
        else
        {
            ChageDirection();
        }
    }
    public void ChageDirection()
    {
        switch (stateMovement)
        {
            case StateMovement.Left:
                stateMovement = StateMovement.Right;
                break;
            case StateMovement.Right:
                stateMovement = StateMovement.Left;
                break;
            player.life--;
        }
        timeMovement = auxTimeMovement;
    }
    
    
}
