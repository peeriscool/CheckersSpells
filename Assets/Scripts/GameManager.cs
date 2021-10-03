using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int rowsOfCheckers = 3;
    private Vector2 gridStartPosition;
    Checker checkerTest;
    public Card_ScriptableObject[] Cards;
    InventoryManager Hand;

    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        gridStartPosition = new Vector2(-3.5f, -3.5f);
        SpawnGrid(gridStartPosition);
        checkerTest = new Checker(new GridPos(5, 7), true);
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)));
        //Hand = new InventoryManager(Cards, 10);

        if (rowsOfCheckers > GridSystem.ySize / 2)
        {
            Debug.LogError("Rows of checkers exceed the useable capacity. Make the rows smallers, or the grid larger");
        }

        bool pieceColor = false;
        for (int i = 0; i < GridSystem.ySize; i++)
        {
            if (i < rowsOfCheckers)
                pieceColor = false;

            if (i > GridSystem.ySize - rowsOfCheckers)
                pieceColor = true;

            if (i >= rowsOfCheckers && i <= GridSystem.ySize - rowsOfCheckers)
                continue;

            for (int j = 0; j < GridSystem.xSize; j++)
            {
                if((i+j) % 2 == 0)
                spawnChecker(new GridPos(j, i), pieceColor);            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Hand.StartTurn();
        }

        ClickOnTiles();
    }

    Checker ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            return GridSystem.checkGridPosition(new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x), (int)Mathf.Round(worldPosition.y - gridStartPosition.y)));
        }
        return null;
    }

    void SpawnGrid(Vector2 startlocation)
    {
        for(int i = 0; i < GridSystem.xSize; i++)
        {
            for(int j = 0; j < GridSystem.ySize; j++)
            {
                GameObject squareColor;
                //check if it's even
                if((i + j) % 2 == 0)
                {
                    squareColor = blackSquare;
                }
                else
                {
                    squareColor = whiteSquare;
                }

                Instantiate(squareColor, new Vector3(startlocation.x + i, startlocation.y + j, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }

    void spawnChecker(GridPos initPos, bool color)
    {
        Debug.Log("Spawning Checker");
        new Checker(initPos, color);
        if (color)
        {
            Instantiate(blackPiece, new Vector2(gridStartPosition.x + initPos.x, gridStartPosition.y + initPos.y), new Quaternion(0, 0, 0, 0));
        }
        else
        {
            Instantiate(whitePiece, new Vector2(gridStartPosition.x + initPos.x, gridStartPosition.y + initPos.y), new Quaternion(0, 0, 0, 0));
        }
    }
}

/*
 *using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    GridSystem grid;
    InventoryManager Hand;
    public Card_ScriptableObject[] Cards;

    public delegate Card_ScriptableObject DrawACard();
    void Start()
    {
        Hand = new InventoryManager(Cards, 10);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Hand.StartTurn();            
        }
    }
}
 
 */
