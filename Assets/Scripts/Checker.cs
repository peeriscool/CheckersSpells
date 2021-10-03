﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    //if we wanna keep the checkers on one color,  we just have to check the sum of the gridPos
    //One color is even, the other is odd. 1+1 = 2, 2+2 = 4, 5+7 = 12
    public GridPos myPos { get; private set; }
    private GameObject body;
    
    //true for black, false for white
    public bool BlackOrWhite { get; private set; }


    public Checker(GridPos initPos, bool _blackOrWhite, GameObject _body)
    {
        BlackOrWhite = _blackOrWhite;
        myPos = initPos;
        body = _body;
        GridSystem.AddChecker(this, initPos);
    }

    public void UpdatePos(GridPos _pos)
    {
        myPos = _pos;
    }

    public void UpdateChecker(Vector2 _offset)
    {
        body.transform.position = new Vector2(myPos.x + _offset.x, myPos.y + _offset.y);
    }

    public void Kill()
    {
        GameObject.Destroy(body);
    }

    //Pieces can move diagonally if that space is empty
    //This doesn't seem all that efficient. I am removing and adding a piece, while I should be moving a piece
    //public void MovePiece(GridPos newGridPos)
    //{
    //    if (GridSystem.checkGridPosition(newGridPos) == null
    //        && ((newGridPos.x == myPos.x - 1 || newGridPos.x == myPos.x + 1) && (newGridPos.y == myPos.y - 1 || newGridPos.y == myPos.y + 1)))
    //    {
    //        GridSystem.RemoveChecker(myPos);
    //        GridSystem.AddChecker(this,newGridPos);
    //        myPos = newGridPos;
    //    }
    //    else
    //    {
    //        Debug.Log("Can't move there");
    //    }
    //}
}
