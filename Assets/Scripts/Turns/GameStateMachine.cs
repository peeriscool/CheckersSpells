using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private Gamestate currenstate;

    WhiteTurn whiteTurn;
    BlackTurn blackTurn;
    public void Initialize()
    {
        whiteTurn = new WhiteTurn();
        blackTurn = new BlackTurn();

        currenstate = whiteTurn;
    }

    public void SwitchState(Gamestate newState)
    {
        currenstate.OnSwitch();
        currenstate.Exit();
        currenstate = newState;
        currenstate.Enter();
    }

    public void StateUpdate()
    {
        currenstate.LogicUpdate();

        foreach(StateTransition transition in currenstate.Transitions)
        {
            if (transition.condition())
            {
            //    SwitchState(transition.target);
            }
        }
    }
}
