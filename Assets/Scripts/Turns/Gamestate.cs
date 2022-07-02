using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamestate
{
    public List<StateTransition> Transitions { get; protected set; }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void OnSwitch()
    {

    }
}
