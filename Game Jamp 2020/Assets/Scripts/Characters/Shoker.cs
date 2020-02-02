using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoker : Enemy
{
    // Start is called before the first frame update
    public float delayAttack;
    public float auxDelayAttack;
    private bool playerScape = false;
    public enum States
    {
        Patrol,
        Chase,
        Stunt,
        Attack,
        Stay,
        Exausted,
        Count
    }
    //HAGO UN ENUM DE Eventos
    public enum Events
    {
        InTriggerPlayer,
        outTriggerplayer,
        InRangeStunt,
        outRangeStunt,
        ReadyToAttack,
        inStay,
        inExausted,
        FinishAttack,
        Count
    }
    private void Awake()
    {
        // Aca defino las relaciones de estado y le hago el new al objeto FSM
        fsm = new FSM((int)States.Count, (int)Events.Count, (int)States.Patrol);
        fsm.SetRelations((int)States.Patrol, (int)States.Chase, (int)Events.InTriggerPlayer);
        fsm.SetRelations((int)States.Chase, (int)States.Stunt, (int)Events.InRangeStunt);
        fsm.SetRelations((int)States.Stunt, (int)States.Attack, (int)Events.ReadyToAttack);
        fsm.SetRelations((int)States.Attack, (int)States.Patrol, (int)Events.FinishAttack);
        fsm.SetRelations((int)States.Stunt, (int)States.Chase, (int)Events.outRangeStunt);
        fsm.SetRelations((int)States.Chase, (int)States.Patrol, (int)Events.outTriggerplayer);


    }
    private void Start()
    {
        GameObject go = GameObject.Find("Player");
        player = go.GetComponent<Player>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        switch (fsm.GetCurrentState())
        {
            case (int)States.Patrol:
                checkDirection = true;
                if (enableMovement)
                {
                    Movement();
                }
                Patrol();
                //Debug.Log("Patrol");
                break;
            case (int)States.Chase:
                checkDirection = false;
                
                //Debug.Log("Chase");
                Chase();
                break;
            case (int)States.Stunt:
                checkDirection = false;
                Stunt();
                //Debug.Log("Stunt");
                break;
            case (int)States.Attack:
                Attack();
                checkDirection = false;
                //Debug.Log("Attack");
                break;
            case (int)States.Stay:
                checkDirection = false;
                //Debug.Log("Stay");
                break;
        }
    }
    public void Chase()
    {
        if (player.transform.position.x < transform.position.x)
        {
            LeftMovement(true);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            RightMovement(true);
        }
        if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceAttack && Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY)
        {
            fsm.SendEvent((int)Events.InRangeStunt);
        }
        else if(Mathf.Abs((transform.position.x - player.transform.position.x)) > distanceChase || Mathf.Abs((transform.position.y - player.transform.position.y)) > distaceY)
        {
            fsm.SendEvent((int)Events.outTriggerplayer);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        fsm.SendEvent((int)Events.outTriggerplayer);
    }
    public void Stunt()
    {
        if (!player.isDashing())
        {
            player.timeDelayStune = delayAttack + 1;
            player.statePlayer = Player.StatePlayer.Stunt;
            fsm.SendEvent((int)Events.ReadyToAttack);
            player.life = player.life - Damage;
            playerScape = false;
        }
       
    }
    public override void Attack()
    {

        if (delayAttack > 0)
        {
            fsm.SendEvent((int)Events.InRangeStunt);
            delayAttack = delayAttack - Time.deltaTime;
        }
        else if (delayAttack <= 0)
        {
            delayAttack = auxDelayAttack;
            if (!playerScape)
            {
                player.life = 0;
            }
            fsm.SendEvent((int)Events.FinishAttack);

        }
        if (Mathf.Abs((transform.position.x - player.transform.position.x)) > distanceChase || Mathf.Abs((transform.position.y - player.transform.position.y)) > distaceY)
        {
            playerScape = true;
        }
    }
    public void Patrol()
    {
        if (Mathf.Abs((transform.position.x - player.transform.position.x)) <= distanceChase && Mathf.Abs((transform.position.y - player.transform.position.y)) <= distaceY)
        {
            fsm.SendEvent((int)Events.InTriggerPlayer);
        }
    }
}
