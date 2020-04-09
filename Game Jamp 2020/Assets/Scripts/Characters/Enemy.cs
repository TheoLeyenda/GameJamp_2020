using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characther
{
    // Start is called before the first frame update
    public float timeDelayStune;
    public float Damage;
    public float distanceAttack;
    public float distanceChase;
    public float timeMovement;
    public float auxTimeMovement;
    public float distaceDectectedFloor;
    public bool enableMovement = true;
    protected bool normalMovement = true;
    protected bool checkDirection = true;
    public float distaceY = 0.5f;
    protected FSM fsm;
    protected Player player;
    protected Vector2 DistanceVector;
    public GameObject DropItem;
    public GameObject generatorCadaver;
    [HideInInspector]
    public StateEnemy stateEnemy;
    [SerializeField]
    protected StateMovement stateMovement;
    public enum StateMovement
    {
        Left,
        Right,
        Jump,
    }
    public enum StateEnemy
    {
        none,
        Stunt,
    }
    protected virtual void Update()
    {
        if (enableMovement && stateEnemy != StateEnemy.Stunt)
        {
            Movement();
        }
        else if(stateEnemy == StateEnemy.Stunt)
        {
            //ANIMACION STUNE
        }
    }
    
    public override void Movement()
    {
        if (normalMovement)
        {
            switch (stateMovement)
            {
                case StateMovement.Left:
                    LeftMovement(false);
                    break;
                case StateMovement.Right:
                    RightMovement(false);
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
        RaycastHit2D raycastHit2 = Physics2D.Raycast(new Vector2(transform.position.x + (transform.right.x * 1f), transform.position.y), transform.forward, 1.5f);
        if (raycastHit.collider != null )
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
        if(raycastHit2.collider != null)
        {
            if(raycastHit.collider.tag == "Floor")
            {
                ChageDirection();
            }
        }
    }

    public void CheckStateEnemy()
    {
        if (stateEnemy == StateEnemy.Stunt)
        {
            if (timeDelayStune > 0)
            {
                timeDelayStune = timeDelayStune - Time.deltaTime;
            }
            else if (timeDelayStune <= 0)
            {
                stateEnemy = StateEnemy.none;
            }
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
        }
        timeMovement = auxTimeMovement;
    }
    public void CheckDead()
    {
        if (life <= 0)
        {
            Instantiate(DropItem, new Vector3(transform.position.x, generatorCadaver.transform.position.y,transform.position.z), Quaternion.identity, null);
            Destroy(this.gameObject);
        }
        
    }
    
    
}
