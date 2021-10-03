using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GridSystem
{
    //public int[] Xrow;
    //public int[] Yrow;
    public static int xSize { get; private set; }
    public static int ySize { get; private set; }

    private static Checker[,] checkerGrid;

    public static void SetGridSize(int _xSize, int _ySize)
    {
        checkerGrid = new Checker[_xSize, _ySize];
        xSize = _xSize;
        ySize = _ySize;
    }

    public static void RemoveChecker(GridPos _gridPos)
    {
        if(checkerGrid[_gridPos.x, _gridPos.y] != null)
        {
            checkerGrid[_gridPos.x, _gridPos.y].Kill();
            checkerGrid[_gridPos.x, _gridPos.y] = null;
        }
    }
    public static void AddChecker(Checker _checker, GridPos _gridPos)
    {
        if (checkerGrid[_gridPos.x, _gridPos.y] == null)
            checkerGrid[_gridPos.x, _gridPos.y] = _checker;
    }

    public static void MoveChecker(GridPos _oldPos, GridPos _newPos)
    {
        if (checkerGrid[_oldPos.x, _oldPos.y] != null && checkerGrid[_newPos.x, _newPos.y] == null
            && (_newPos.x == _oldPos.x - 1 || _newPos.x == _oldPos.x + 1) && (_newPos.y == _oldPos.y - 1 || _newPos.y == _oldPos.y + 1))
        {
            checkerGrid[_newPos.x, _newPos.y] = checkerGrid[_oldPos.x, _oldPos.y];
            checkerGrid[_newPos.x, _newPos.y].UpdatePos(_newPos);
            checkerGrid[_oldPos.x, _oldPos.y] = null;
        }
        else
        {
            Debug.Log("Can't do that");
        }
    }

    public static void AttackChecker(GridPos _oldPos, GridPos _newPos)
    {
        if (checkerGrid[_oldPos.x, _oldPos.y] != null && checkerGrid[_newPos.x, _newPos.y] != null
            && (_newPos.x == _oldPos.x - 1 || _newPos.x == _oldPos.x + 1) && (_newPos.y == _oldPos.y - 1 || _newPos.y == _oldPos.y + 1)
            && checkerGrid[_oldPos.x, _oldPos.y].blackOrWhite != checkerGrid[_newPos.x, _newPos.y].blackOrWhite)
        {
            int xDirection = _newPos.x - _oldPos.x;
            int yDirection = _newPos.y - _oldPos.y;
            GridPos landPos = new GridPos(_newPos.x + xDirection, _newPos.y + yDirection);

            if (landPos.x >= 0 && landPos.x < xSize && landPos.y >= 0 && landPos.y < ySize && checkerGrid[landPos.x, landPos.y] == null)
            {
                RemoveChecker(_newPos);

                checkerGrid[landPos.x, landPos.y] = checkerGrid[_oldPos.x, _oldPos.y];
                checkerGrid[_oldPos.x, _oldPos.y] = null;
                //Debug.Log(checkerGrid[landPos.x, landPos.y]);
                checkerGrid[landPos.x, landPos.y].UpdatePos(landPos);
            }

            else
            {
                Debug.Log("Can't do that");
            }
        }
        else
        {
            Debug.Log("Can't do that");
        }
    }

    public static Checker checkGridPosition(GridPos _gridPos)
    {
        return checkerGrid[_gridPos .x, _gridPos.y];
    }

    //public Sprite[] GenerateVisual()
    // {
    //     Sprite[] data = new Sprite[Xrow.Length];
    //     for (int i = 0; i < Xrow.Length; i++)
    //     {

    //         // GameObject a = new GameObject(i.ToString(),typeof (cell));
    //         Sprite a = Sprite.Create(null,new Rect() ,Vector2.zero);          
    //         data[i] = a;
    //     }
    //     return data;
    // }
}

public struct GridPos
{
    public int x;
    public int y;

    public GridPos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}