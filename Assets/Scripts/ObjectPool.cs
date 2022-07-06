﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//T means it can be anything, but we give it constraints so it at least has to be an implementation
//of the IPoolable interface
public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inActivePool = new List<T>();

    private T AddNewItemToPool()
    {
        //I think we use an activator because I can't create a new instance of T
        //by using the "new" keyword. 
        T instance = (T)Activator.CreateInstance(typeof(T));
        inActivePool.Add(instance);
        return instance;
    }

    public T RequestItem()
    {
        if(inActivePool.Count > 0)
        {
            //if there's items in the pool, fish the first on out
            return ActivateItem(inActivePool[0]);
        }

        //if there aren't items in the pool, create a new one.
        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T item)
    {
        item.OnEnableObject();
        item.Active = true;
        if (inActivePool.Contains(item))
        {
            inActivePool.Remove(item);
        }
        activePool.Add(item);
        return item;
    }

    public void ReturnObjectToPool(T item)
    {
        if (activePool.Contains(item))
        {
            activePool.Remove(item);
        }

        item.OnDisableObject();
        item.Active = false;
        inActivePool.Add(item);
    }


}
