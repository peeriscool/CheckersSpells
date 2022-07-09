﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new default obj", menuName = "Inventory system/Items/CardItem")]
public class CardItem : ItemObject , IPoolable
{
    public GameObject instancedrefrence;
    public Vector2 mypos;
    int offsetX = 2;
    int offsetY = 2;
    public bool InRangeofCard(Vector2 _pos)
    {
        //if && ||
        //check if recieved position =~ to known postion of card
      //  Debug.Log("see if card is in range");
        if (_pos.x >= mypos.x - offsetX && _pos.x <= mypos.x + offsetX && _pos.y >= mypos.y - offsetY && _pos.y <= mypos.x + offsetY)
        {
            //in range
            Debug.Log("card is in range : True");
            return true;
        }
        else
        {
            Debug.Log("mypos,param" + mypos + "" + _pos);
        }
        return false;
    }
    public bool Active { get; set; }
    public void OnEnableObject()
    {

    }
    public void OnDisableObject()
    {

    }
    //set to toy by default
    public void Awake()
    {
        type = ItemType.action;
    }

}