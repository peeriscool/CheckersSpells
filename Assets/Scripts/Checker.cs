using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    //if we wanna keep the checkers on one color,  we just have to check the sum of the gridPos (X + Y position)
    //One color is even, the other is odd. 1+1 = 2, 2+2 = 4, 5+7 = 12
    public GridPos myPos { get; private set; }

    //true for black, false for white
    public bool blackOrWhite { get; private set; }    
    
    //in game respresentation of the checker
    protected GameObject body;
    
    //initialize the checker
    public Checker(GridPos _initPos, bool _blackOrWhite, GameObject _body)
    {
        blackOrWhite = _blackOrWhite;
        myPos = _initPos;
        body = _body;
        GridSystem.AddChecker(this, _initPos);
    }

    //Function to move the checker
    public void UpdatePos(GridPos _pos)
    {
        myPos = _pos;
    }

    //Function to fetch the body of the checker
    public GameObject Get_Body(Checker _prefab)
    {
        return _prefab.body;
    }


    //Move the checker in a given direction
    public void UpdateChecker(Vector2 _offset)
    {
        body.transform.position = new Vector2(myPos.x + _offset.x, myPos.y + _offset.y);
    }

    //kill the checker
    public void Kill()
    {
        GameObject.Destroy(body);
    }
}
