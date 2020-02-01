using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoker : Enemy
{
    // Start is called before the first frame update
    public enum States
    {
        Patrol,
        Chase,
        Stunt,
        Attack,
        Count
    }
    //HAGO UN ENUM DE Eventos
    public enum Events
    {
        InTriggerPlayer,
        outTriggerplayer,
        InRangeStunt,
        outRangeStunt,
        FinishDelayAttack,
        Count
    }
    private void Awake()
    {
        // Aca defino las relaciones de estado y le hago el new al objeto FSM
        fsm = new FSM((int)States.Count, (int)Events.Count, (int)States.Patrol);

        fsm.SetRelations((int)States.Patrol, (int)States.Chase, (int)Events.InTriggerPlayer);
        fsm.SetRelations((int)States.Chase, (int)States.Stunt, (int)Events.InRangeStunt);
        fsm.SetRelations((int)States.Stunt, (int)States.Attack, (int)Events.FinishDelayAttack);
        fsm.SetRelations((int)States.Attack, (int)States.Stunt, (int)Events.InRangeStunt);
        fsm.SetRelations((int)States.Attack, (int)States.Chase, (int)Events.InTriggerPlayer);
        fsm.SetRelations((int)States.Attack, (int)States.Patrol, (int)Events.outTriggerplayer);
        fsm.SetRelations((int)States.Stunt, (int)States.Chase, (int)Events.outRangeStunt);
        fsm.SetRelations((int)States.Chase, (int)States.Patrol, (int)Events.outTriggerplayer);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        switch (fsm.GetCurrentState())
        {
            case (int)States.Patrol:
                if (enableMovement)
                {
                    Movement();
                }
                break;
            case (int)States.Chase:

                break;
            case (int)States.Stunt:
                break;
            case (int)States.Attack:
                break;
        }
    }
}
