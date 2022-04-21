using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Turnstate
{
    public override void Enter()
    {
        base.Enter();
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
