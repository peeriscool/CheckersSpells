using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    //if we wanna keep the checkers on one color,  we just have to check the sum of the gridPos
    //One color is even, the other is odd. 1+1 = 2, 2+2 = 4, 5+7 = 12
    private GridPos myPos;
    
    //true for black, false for white
    public bool BlackOrWhite { get; private set; }


    public Checker(GridPos initPos, bool _blackOrWhite)
    {
        BlackOrWhite = _blackOrWhite;
        myPos = initPos;
        GridSystem.AddChecker(this, initPos);
    }

    //Checker pieces can attack diagonally when there's an enemy piece there, and the next diagonal space in that direction is empty
    //Ideally you can string attacks together
    public void Attack(GridPos attackDirection)
    {
        if (GridSystem.checkGridPosition(attackDirection).BlackOrWhite != BlackOrWhite
            && ((attackDirection.x == myPos.x - 1 || attackDirection.x == myPos.x + 1) && (attackDirection.y == myPos.y - 1 || attackDirection.y == myPos.y + 1)))
        {
            GridPos landPos = new GridPos(myPos.x - (myPos.x - attackDirection.x), myPos.y - (myPos.y - attackDirection.y));
            GridSystem.RemoveChecker(attackDirection);
            GridSystem.RemoveChecker(myPos);
            GridSystem.AddChecker(this, landPos);
        }
        else
        {
            Debug.Log("Can't attack in that direction");
        }
    }

    //Pieces can move diagonally if that space is empty
    public void MovePiece(GridPos newGridPos)
    {
        if (GridSystem.checkGridPosition(newGridPos) == null
            && ((newGridPos.x == myPos.x - 1 || newGridPos.x == myPos.x + 1) && (newGridPos.y == myPos.y - 1 || newGridPos.y == myPos.y + 1)))
        {
            GridSystem.RemoveChecker(myPos);
            GridSystem.AddChecker(this,newGridPos);
            myPos = newGridPos;
        }
        else
        {
            Debug.Log("Can't move there");
        }
    }
}
