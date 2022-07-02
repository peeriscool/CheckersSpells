using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GridSystem
{
    static Vector2 gridStartPosition;
    //the size of the board, horizontally and vertically
    public static int xSize { get; private set; }
    public static int ySize { get; private set; }

    //an array that tracks where the checkers are
    private static IPlaceable[,] checkerGrid;
    private static GameObject[,] tiles;

    //Initializes the grid, by input size
    public static void SetGridSize(int _xSize, int _ySize)
    {
        checkerGrid = new Checker[_xSize, _ySize];
        xSize = _xSize;
        ySize = _ySize;
        
    }

    public static void InitializeGrid(Vector2 _gridStartPosition, GameObject _blackSquare, GameObject _whiteSquare)
    {
        gridStartPosition = _gridStartPosition;
        tiles = new GameObject[xSize, ySize];

        //Tells me how many tiles there are in total
        Debug.Log(xSize * ySize + " is the amount of tiles availible");

        //let's spawn the tiles
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                GameObject squareColor;
                //check if it's even, all even tiles are black, all odd tiles are white. Quick math
                if ((i + j) % 2 == 0)
                {
                    squareColor = _blackSquare;
                }
                else
                {
                    squareColor = _whiteSquare;
                }
                tiles[i, j] = squareColor;

                //the board will always spawn in the center of the screen. Each tile will spawn individually
                UnityEngine.Object.Instantiate(tiles[i, j], new Vector3(gridStartPosition.x + i, gridStartPosition.y + j, 0.1f), new Quaternion(0, 0, 0, 0));
                //instantiate(gridsysyem())
            }
        }

        //if the number of rows is bigger than half of the board, the players can't both get that amount of rows, so an error is given
        //if (rowsOfCheckers > GridSystem.ySize / 2)
        //{
        //    Debug.LogError("Rows of checkers exceed the useable capacity. Make the rows smallers, or the grid larger");
        //}

    }

    public static GridPos ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(worldPosition);

        return new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x), (int)Mathf.Round(worldPosition.y - gridStartPosition.y));
    }

    //Remove a checker on a specific coordinate in case they get hit, or we need to remove them for any other reason
    public static void RemoveChecker(GridPos _gridPos)
    {
        if (checkerGrid[_gridPos.x, _gridPos.y] == null)
        {
            return;
        }
        checkerGrid[_gridPos.x, _gridPos.y].Kill();
        checkerGrid[_gridPos.x, _gridPos.y] = null;
    }

    //function that can add a checker to a specific position on the board
    public static void AddPlaceable(IPlaceable _placeable, GridPos _gridPos)
    {
        if (checkerGrid[_gridPos.x, _gridPos.y] != null)
            return;

        checkerGrid[_gridPos.x, _gridPos.y] = _placeable;
    }

    //move a checker, from a chosen position to a new position. Will only work if it's a legal move
    public static bool MoveChecker(GridPos _oldPos, GridPos _newPos)
    {
        //for now it only works when they move any diagonal direction, should be changed to move only forward, and only backwards when you strike a piece
        if (checkerGrid[_oldPos.x, _oldPos.y] != null && checkerGrid[_newPos.x, _newPos.y] == null
            && (_newPos.x == _oldPos.x - 1 || _newPos.x == _oldPos.x + 1) && (_newPos.y == _oldPos.y - 1 || _newPos.y == _oldPos.y + 1))
        {
            GridPos direction = _newPos - _oldPos;
            if((direction.y < 0 && checkerGrid[_oldPos.x, _oldPos.y].blackOrWhite == 1) || (direction.y > 0 && checkerGrid[_oldPos.x, _oldPos.y].blackOrWhite == 0))
            {
                Debug.Log("Can't go that direction");
                return false;
            }

            checkerGrid[_newPos.x, _newPos.y] = checkerGrid[_oldPos.x, _oldPos.y];
            checkerGrid[_newPos.x, _newPos.y].UpdatePos(_newPos);
            checkerGrid[_newPos.x, _newPos.y].UpdateVisual(_oldPos - _newPos);
            checkerGrid[_oldPos.x, _oldPos.y] = null;
            return true;
        }

        else
        {
            Debug.Log("Can't do that");
            return false;
        }
    }

    //Attack another piece, and remove it if it's a legal move
    public static bool AttackChecker(GridPos _oldPos, GridPos _newPos)
    {
        Debug.Log("ATTACK");
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
                checkerGrid[landPos.x, landPos.y].UpdateVisual(_oldPos - landPos);
                return true;
            }

            else
            {
                Debug.Log("Can't do that");
                return false;
            }
        }
        else
        {
            Debug.Log("Can't do that");
            return false;
        }
    }

    //fetch what the checker is on a given location
    public static IPlaceable checkGridPosition(GridPos _gridPos)
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

//a struct for positions on the grid
public struct GridPos
{
    public int x;
    public int y;

    public GridPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static GridPos operator +(GridPos lhs, GridPos rhs)
    {
        return new GridPos(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static GridPos operator -(GridPos lhs, GridPos rhs)
    {
        return new GridPos(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static GridPos operator -(GridPos v)
    {
        return new GridPos(-v.x, -v.y);
    }
}