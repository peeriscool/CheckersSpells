using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : IPlaceable
{
    //if we wanna keep the checkers on one color,  we just have to check the sum of the gridPos (X + Y position)
    //One color is even, the other is odd. 1+1 = 2, 2+2 = 4, 5+7 = 12
    public GridPos myPos { get; private set; }

    //true for black, false for white
    public int blackOrWhite { get; private set; }    
    
    //in game respresentation of the checker
    protected GameObject body;
    
    //initialize the checker
    public Checker(GridPos _initPos, int _blackOrWhite, GameObject _body)
    {
        blackOrWhite = _blackOrWhite;
        myPos = _initPos;
        body = _body;
    }

    //Function to move the checker
    public void UpdatePos(GridPos _pos)
    {
        myPos = _pos;
    }

    //Update the visual representation of the checker
    public void UpdateVisual(GridPos _Offset)
    {
        Debug.Log("moving " + body.name + " to " + new Vector2(body.transform.position.x + _Offset.x, body.transform.position.y + _Offset.y));
        body.transform.position = new Vector2(body.transform.position.x - _Offset.x, body.transform.position.y - _Offset.y);
    }

    //Function to fetch the body of the checker
    public GameObject Get_Body(Checker _prefab)
    {
        return _prefab.body;
    }

    //kill the checker
    public void Kill()
    {
        GameObject.Destroy(body);
    }
}
