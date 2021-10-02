using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2 gridStartPosition;
    Checker checkerTest;
    public Card_ScriptableObject[] Cards;
    InventoryManager Hand;

    public GameObject blackSquare, whiteSquare;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        gridStartPosition = new Vector2(-3.5f, -3.5f);
        SpawnGrid(gridStartPosition);
        checkerTest = new Checker(new GridPos(5, 7), true);
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)));
        Hand = new InventoryManager(Cards, 10);
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

    void ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log(GridSystem.checkGridPosition(new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x) + 1, (int)Mathf.Round(worldPosition.y - gridStartPosition.y) + 1)));
        }
    }

    void SpawnGrid(Vector2 startlocation)
    {
        for(int i = 0; i < GridSystem.xSize; i++)
        {
            for(int j = 0; j < GridSystem.ySize; j++)
            {
                GameObject squareColor = null;
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
