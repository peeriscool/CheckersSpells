using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStateMachine
{
    private Turnstate currenstate;
    public void OnStart()
    {

    }

    public void SwitchState(Turnstate newState)
    {
        currenstate.Exit();
        currenstate = newState;
        currenstate.Enter();
    }
}
