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

    private static Checker[,] CheckerGrid;

    public static void SetGridSize(int _xSize, int _ySize)
    {
        CheckerGrid = new Checker[_xSize, _ySize];
        xSize = _xSize;
        ySize = _ySize;
    }

    public static void RemoveChecker(GridPos _gridPos)
    {
        if(CheckerGrid[_gridPos.x, _gridPos.y] != null)
        {
            CheckerGrid[_gridPos.x, _gridPos.y].Kill();
            CheckerGrid[_gridPos.x, _gridPos.y] = null;
        }
    }
    public static void AddChecker(Checker _checker, GridPos _gridPos)
    {
        if (CheckerGrid[_gridPos.x, _gridPos.y] == null)
            CheckerGrid[_gridPos.x, _gridPos.y] = _checker;
    }

    public static void MoveChecker(GridPos _oldPos, GridPos _newPos)
    {
        if (CheckerGrid[_oldPos.x, _oldPos.y] != null && CheckerGrid[_newPos.x, _newPos.y] == null
            && (_newPos.x == _oldPos.x - 1 || _newPos.x == _oldPos.x + 1) && (_newPos.y == _oldPos.y - 1 || _newPos.y == _oldPos.y + 1))
        {
            CheckerGrid[_newPos.x, _newPos.y] = CheckerGrid[_oldPos.x, _oldPos.y];
            CheckerGrid[_newPos.x, _newPos.y].UpdatePos(_newPos);
            CheckerGrid[_oldPos.x, _oldPos.y] = null;
        }
        else
        {
            Debug.Log("Can't do that");
        }
    }

    public static void AttackChecker(GridPos _oldPos, GridPos _newPos)
    {
        if (CheckerGrid[_oldPos.x, _oldPos.y] != null && CheckerGrid[_newPos.x, _newPos.y] != null
            && (_newPos.x == _oldPos.x - 1 || _newPos.x == _oldPos.x + 1) && (_newPos.y == _oldPos.y - 1 || _newPos.y == _oldPos.y + 1)
            && CheckerGrid[_oldPos.x, _oldPos.y].BlackOrWhite != CheckerGrid[_newPos.x, _newPos.y].BlackOrWhite)
        {
            int xDirection = _newPos.x - _oldPos.x;
            int yDirection = _newPos.y - _oldPos.y;
            GridPos landPos = new GridPos(_newPos.x + xDirection, _newPos.y + yDirection);

            if (landPos.x >= 0 && landPos.x < xSize && landPos.y >= 0 && landPos.y < ySize && CheckerGrid[landPos.x, landPos.y] == null)
            {
                RemoveChecker(_newPos);

                CheckerGrid[landPos.x, landPos.y] = CheckerGrid[_oldPos.x, _oldPos.y];
                CheckerGrid[_oldPos.x, _oldPos.y] = null;
                //Debug.Log(CheckerGrid[landPos.x, landPos.y]);
                CheckerGrid[landPos.x, landPos.y].UpdatePos(landPos);
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
        return CheckerGrid[_gridPos .x, _gridPos.y];
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