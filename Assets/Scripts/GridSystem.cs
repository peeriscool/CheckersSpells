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
    public static void AddChecker(Checker checker, GridPos gridPos)
    {
        if (CheckerGrid[gridPos.x, gridPos.y] == null)
            CheckerGrid[gridPos.x, gridPos.y] = checker;
    }

    public static void MoveChecker(GridPos oldPos, GridPos newPos)
    {
        if (CheckerGrid[oldPos.x, oldPos.y] != null && CheckerGrid[newPos.x, newPos.y] == null
            && (newPos.x == oldPos.x - 1 || newPos.x == oldPos.x + 1) && (newPos.y == oldPos.y - 1 || newPos.y == oldPos.y + 1))
        {
            CheckerGrid[newPos.x, newPos.y] = CheckerGrid[oldPos.x, oldPos.y];
            CheckerGrid[newPos.x, newPos.y].UpdatePos(newPos);
            CheckerGrid[oldPos.x, oldPos.y] = null;
        }
        else
        {
            Debug.Log("Can't do that");
        }
    }

    public static void AttackChecker(GridPos oldPos, GridPos newPos)
    {
        if (CheckerGrid[oldPos.x, oldPos.y] != null && CheckerGrid[newPos.x, newPos.y] != null
            && (newPos.x == oldPos.x - 1 || newPos.x == oldPos.x + 1) && (newPos.y == oldPos.y - 1 || newPos.y == oldPos.y + 1)
            && CheckerGrid[oldPos.x, oldPos.y].BlackOrWhite != CheckerGrid[newPos.x, newPos.y].BlackOrWhite)
        {
            int xDirection = newPos.x - oldPos.x;
            int yDirection = newPos.y - oldPos.y;
            GridPos landPos = new GridPos(newPos.x + xDirection, newPos.y + yDirection);

            if (landPos.x >= 0 && landPos.x < xSize && landPos.y >= 0 && landPos.y < ySize && CheckerGrid[landPos.x, landPos.y] == null)
            {
                RemoveChecker(newPos);

                CheckerGrid[landPos.y, landPos.y] = CheckerGrid[oldPos.x, oldPos.y];
                CheckerGrid[oldPos.x, oldPos.y] = null;
                Debug.Log(CheckerGrid[landPos.x, landPos.y]);
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

    public static Checker checkGridPosition(GridPos gridPos)
    {
        return CheckerGrid[gridPos.x, gridPos.y];
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
public class cell
{
    cell() { }
}
