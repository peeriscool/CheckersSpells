using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : Gamestate
{
    System.Type previousState;

    public PauseState()
    {
        Transitions = new List<StateTransition>();
    }

    public void GetPreviousState(System.Type _previousState)
    {
        Transitions = new List<StateTransition>();
        previousState = _previousState;
        Debug.Log(previousState);
        Transitions.Add(new StateTransition(previousState, () => Input.GetKeyDown(KeyCode.Escape)));
    }

    public override void Enter()
    {
        base.Enter();
        //I also want to get the previous gamestate, so we can revert back to that if we want to unpause

        //get the pause menu in screen + make sure player can't make a move
        //also stop time for any vfx things happening

        Debug.Log("pausing game");
        Time.timeScale = 0;
    }

    public override void Exit()
    {
        Debug.Log("unpausing game");
        base.Exit();
        Time.timeScale = 1;
    }
}
