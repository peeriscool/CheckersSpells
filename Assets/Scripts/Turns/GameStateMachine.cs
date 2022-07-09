using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private IState currenstate;

    //since there is only one instance of this statemachine, it won't need an objectPool
    //Rather I will use a dictionary to save each state version. Since there will only ever be one version of each state
    private Dictionary<System.Type, IState> stateCollection;

    public GameStateMachine(System.Type _initialState)
    {
        stateCollection = new Dictionary<System.Type, IState>();
        currenstate = GetState(_initialState);
    }

    public void SwitchState(IState newState)
    {
        currenstate.OnSwitch();
        currenstate.Exit();
        currenstate = newState;
        currenstate.Enter();
    }

    public void StateUpdate()
    {
        currenstate.LogicUpdate();


        //Check each transition of a state, and if it wants to switch, switch.
        foreach (StateTransition transition in currenstate.Transitions)
        {
            if (transition.condition())
            {
                Debug.Log(transition.target);
                if (transition.target == typeof(PauseState))
                {
                    PauseState pause = GetState(transition.target) as PauseState;
                    pause.GetPreviousState(currenstate.GetType());
                }

                SwitchState(GetState(transition.target));
                break;
            }
        }
    }

    public IState GetState(System.Type t)
    {
        return GetOrCreateState(t);
    }

    private IState GetOrCreateState(System.Type t)
    {
        IState state;

        //if statecollection has that type, fetch it
        if (stateCollection.TryGetValue(t, out state))
        {
            return state;
        }

        //if it doesn't, make a new one.
        else
        { 
            object obj = System.Activator.CreateInstance(t);
            IState _instance = (IState)obj;

            stateCollection.Add(t, _instance);

            return _instance;
        }
    }
}
