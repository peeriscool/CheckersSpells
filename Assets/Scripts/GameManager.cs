using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int rowsOfCheckers = 3;
    private Vector2 gridStartPosition;
    // Checker checkerTest;
    public Card_ScriptableObject[] Cards;
    InventoryManager Hand;
    private GameObject selctedcard;
    private Vector3 Scalevalue;
    private bool Mouseselect = true;
    private bool once = true;
    private GameObject CurrentCard;

    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        gridStartPosition = new Vector2(-3.5f, -3.5f);
        SpawnGrid(gridStartPosition);
        SpawnPieces(gridStartPosition, blackPiece, whitePiece);
        //checkerTest = new Checker(new GridPos(5, 7), true);
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
                if ((i + j) % 2 == 0)
                    spawnChecker(new GridPos(j, i), pieceColor);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) //gives the player a card
        {
            Hand.StartTurn();
        }

        if (Input.GetMouseButton(0)) //picking up cards ToDo: place on grid tile to activate effect.
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay(Mouseselect);
            if (selctedcard != null)
            {
                Vector3 Mouseinput = Input.mousePosition;

                //hover
                if (selctedcard.transform.localScale.x <= Scalevalue.x * 2)
                    selctedcard.transform.localScale = selctedcard.transform.localScale * 2;
            }
        }
        if (Input.GetMouseButtonUp(0) && CurrentCard != null)
        {
            CurrentCard.transform.localScale = Scalevalue;
            CurrentCard = null;
        }

    }
    void CastRay(bool Mouseselect)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit)
        {
            if (hit.collider.gameObject.layer == 8) //card handler "CardLayer"
            {
                Debug.Log(hit.collider.gameObject.name);
                if (selctedcard != null && selctedcard != CurrentCard) { CurrentCard = selctedcard; }//card switch
                selctedcard = hit.collider.gameObject;

                if (once && selctedcard != CurrentCard) { Scalevalue = selctedcard.transform.localScale; once = false; }

                hit.collider.gameObject.transform.position = ray.GetPoint(0f);
            }
            if (hit.collider.gameObject.layer == 9) //pieces handler "PiecesLayer"
            {
                hit.collider.gameObject.transform.position = ray.GetPoint(0f);
            }
            else //must be a click tile action
            {
                ClickOnTiles();
            }
            //if (Mouseselect){ selctedcard.transform.localScale = selctedcard.transform.localScale * 3; }
            //else { selctedcard.transform.localScale = selctedcard.transform.localScale / 3; }
        }
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
        for (int i = 0; i < GridSystem.xSize; i++)
        {
            for (int j = 0; j < GridSystem.ySize; j++)
            {
                GameObject squareColor;
                //check if it's even
                if ((i + j) % 2 == 0)
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


    void SpawnPieces(Vector2 startlocation, GameObject Black, GameObject White)
    {
        for (int i = 0; i < GridSystem.xSize; i++)
        {
            for (int j = 0; j < GridSystem.ySize; j++)
            {

                if (j == GridSystem.ySize - GridSystem.ySize / 2 || j == GridSystem.ySize - GridSystem.ySize / 2 - 1)
                {
                    continue;
                }
                GameObject squareColor;
                //check if it's even
                if ((i + j) % 2 == 0)
                {
                    squareColor = White;
                }
                else
                {
                    // continue; if we only want white pieces
                    squareColor = Black;
                }
                if (squareColor != null)
                {
                    Instantiate(squareColor, new Vector3(startlocation.x + i, startlocation.y + j, -1), new Quaternion(0, 0, 0, 0));
                    //  squareColor.AddComponent<BoxCollider2D>();
                }
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
