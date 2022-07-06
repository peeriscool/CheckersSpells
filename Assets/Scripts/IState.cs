﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState : IPoolable
{
    List<StateTransition> Transitions { get; }
    void Enter();
    void Exit();
    void LogicUpdate();
    void OnSwitch();
}
