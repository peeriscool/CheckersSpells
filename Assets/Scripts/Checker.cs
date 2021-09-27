using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker
{
    //if we wanna keep the checkers on one color,  we just have to check the sum of the gridPos
    //One color is even, the other is uneven. 1+1 = 2, 2+2 = 4, 5+7 = 12
    private GridPos myPos;

    public Checker(GridPos initPos)
    {
        myPos = initPos;
        GridSystem.AddChecker(this, initPos);
    }
    public void Attack()
    {

    }

    public void Move()
    {
        GridSystem.AddChecker(this, new GridPos(1,1));
    }
}
