using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamestate : IState
{
    public bool Active { get; set; }
    public void OnEnableObject()
    {

    }
    public void OnDisableObject()
    {

    }
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
