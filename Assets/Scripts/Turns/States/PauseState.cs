using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Gamestate
{
    System.Type previousState;
    public Pause()
    {
        Transitions = new List<StateTransition>();
        Transitions.Add(new StateTransition(previousState, () => Input.GetKeyDown(KeyCode.Escape)));
    }
    public override void Enter()
    {
        base.Enter();
        //I also want to get the previous gamestate, so we can revert back to that if we want to unpause

        //get the pause menu in screen + make sure player can't make a move
        //also stop time for any vfx things happening

        Time.timeScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
    }
}
