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

    public static Checker[,] CheckerGrid { get; private set; }

    public static void SetGridSize(int _xSize, int _ySize)
    {
        CheckerGrid = new Checker[_xSize, _ySize];
        xSize = _xSize;
        ySize = _ySize;
    }
    public static void AddChecker(Checker checker, GridPos gridPos)
    {
        if (CheckerGrid[gridPos.x, gridPos.y] == null)
            CheckerGrid[gridPos.x, gridPos.y] = checker;
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
