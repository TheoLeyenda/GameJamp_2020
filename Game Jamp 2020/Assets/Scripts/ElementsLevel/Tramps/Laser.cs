using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Enemy
{
    public enum states
    {
        patrol,
        deactivate,
        alarm,
        count
    }
    public enum events
    {
        unresponsive,
        detect,
        count
    }
    private void Awake()
    {
        fsm = new FSM((int)states.count,(int)events.count,(int)states.patrol);
        fsm.SetRelations((int)states.patrol, (int)states.deactivate, (int)events.unresponsive);
        fsm.SetRelations((int)states.alarm,(int)states.alarm,(int)events.detect);
        fsm.SetRelations((int)states.deactivate, (int)states.deactivate, (int)events.unresponsive);

    }

    protected override void Update()
    {
        switch (fsm.GetCurrentState())
        {
            case (int)states.patrol:
                {

                }break;
            case (int)states.deactivate:
                {

                }break;
            case (int)states.alarm:
                {

                }break;
            
        }
    }

}
