﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new default obj", menuName = "Inventory system/Items/CardItem")]
public class CardItem : ItemObject , IPlaceable
{
    public GridPos myPos { get; private set; }
    public int blackOrWhite { get; private set; }

    //set to toy by default
    public void Awake()
    {
        type = ItemType.action;
    }

    public GameObject Get_Body(Checker _prefab)
    {
       return GameObject.CreatePrimitive(PrimitiveType.Plane);//getting a plane to put the sprite on 
    }

    public void Kill()
    {
        Debug.Log("cardItem has to be removed");
    }

    public void UpdatePos(GridPos _pos)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateVisual(GridPos _Offset)
    {
        throw new System.NotImplementedException();
    }
}