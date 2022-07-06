using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePool
{

    private static StatePool instance;
    private static StatePool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StatePool();
            }

            return instance;
        }
    }

    private Dictionary<System.Type, List<IState>> statePool = new Dictionary<System.Type, List<IState>>();

    public static IState GetState(System.Type t)
    {
        return Instance.GetOrCreateState(t);
    }

    private IState GetOrCreateState(System.Type t)
    {
        List<IState> list;
        if (statePool.TryGetValue(t, out list))
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (!list[i].Active)
                {
                    return list[i];
                }
            }

            IState _instance = (IState)System.Activator.CreateInstance(t);
            list.Add(_instance);

            //update the dictionary
            //DISCUSS: Figure out if I need to do this
            statePool[t] = list;

            return _instance;
        }
        else
        {
            //no list exists yet... create it
            list = new List<IState>();
            object obj = System.Activator.CreateInstance(t);
            IState _instance = (IState)obj;
            list.Add(_instance);

            statePool.Add(t, list);

            return _instance;
        }
    }
}
