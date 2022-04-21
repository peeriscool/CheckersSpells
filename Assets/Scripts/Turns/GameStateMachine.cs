using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private Gamestate currenstate;
    public void OnStart()
    {

    }

    public void SwitchState(Gamestate newState)
    {
        currenstate.Exit();
        currenstate = newState;
        currenstate.Enter();
    }
}
