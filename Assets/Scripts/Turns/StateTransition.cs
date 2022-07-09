using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool Condition();

public class StateTransition
{
    public System.Type target;
    public Condition condition;

    /// <summary>
    /// </summary>
    /// <param name="_target">Gamestate</param>
    /// <param name="_condition">bool</param>
    public StateTransition(System.Type _target, Condition _condition)
    {
        target = _target;
        condition = _condition;
    }
}
