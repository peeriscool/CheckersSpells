using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int rowsOfCheckers = 3;
    public Card_ScriptableObject[] cards;
    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;

    InventoryManager hand;
    private Checker selectedChecker;
    private Vector2 gridStartPosition;
    private Vector3 scalevalue;
    private bool mouseSelect = true;
    private bool once = true;
    private GameObject CurrentCard;
    private GameObject selctedcard;
    private GameObject[,] Tiles;
    private GameObject selectedCard;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        gridStartPosition = new Vector2(-3.5f, -3.5f);
        SpawnGrid(gridStartPosition);
        //SpawnPieces(gridStartPosition, blackPiece, whitePiece);
        //checkerTest = new Checker(new GridPos(5, 7), true);
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)));
        Hand = new InventoryManager(Cards, 10);

        if (rowsOfCheckers > GridSystem.ySize / 2)
        {
            Debug.LogError("Rows of checkers exceed the useable capacity. Make the rows smallers, or the grid larger");
        }

        bool pieceColor = false;
        for (int i = 0; i < GridSystem.ySize; i++)
        {
            if (i < rowsOfCheckers)
                pieceColor = false;

            if (i >= GridSystem.ySize - rowsOfCheckers)
                pieceColor = true;

            if (i >= rowsOfCheckers && i < GridSystem.ySize - rowsOfCheckers)
                continue;

            for (int j = 0; j < GridSystem.xSize; j++)
            {
                if ((i + j) % 2 == 0)
                    spawnChecker(new GridPos(j, i), pieceColor);
            }
        }
    }

    private void Update()
    {
        Debug.Log(selectedChecker);
        //if (selectedChecker != null)
        //    Debug.Log(selectedChecker.myPos.x + ", " + selectedChecker.myPos.y);

        if (Input.GetMouseButtonDown(0))
        {
            Checker clickedTile = GridSystem.checkGridPosition(ClickOnTiles());
            GridPos clickedPos = ClickOnTiles();

            if (selectedChecker != null)
            {
                if (clickedTile != null)
                    GridSystem.AttackChecker(selectedChecker.myPos, clickedPos);

                else if (clickedTile == null)
                    GridSystem.MoveChecker(selectedChecker.myPos, clickedPos);

                selectedChecker.UpdateChecker(gridStartPosition);
                selectedChecker = null;
            }

            else if (selectedChecker == null)
            {
                if (clickedTile != null)
                    selectedChecker = clickedTile;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A)) //gives the player a card
        {
            hand.StartTurn();
        }

        if (Input.GetMouseButton(0)) //picking up cards ToDo: place on grid tile to activate effect.
        {
            //Debug.Log("Pressed left click, casting ray.");
            CastRay(mouseSelect);
            if (selectedCard != null)
            {
                Vector3 Mouseinput = Input.mousePosition;

                //hover
                if (selectedCard.transform.localScale.x <= scalevalue.x * 2)
                    selectedCard.transform.localScale = selectedCard.transform.localScale * 2;
            }
        }

        if (Input.GetMouseButtonUp(0) && currentCard != null)
        {
            currentCard.transform.localScale = scalevalue;
            currentCard = null;
        }

    }
    void CastRay(bool _Mouseselect)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit)
        {
            if (hit.collider.gameObject.layer == 8) //card handler "CardLayer"
            {
                Debug.Log(hit.collider.gameObject.name);
                if (selectedCard != null && selectedCard != currentCard) { currentCard = selectedCard; }//card switch
                selectedCard = hit.collider.gameObject;

                if (once && selectedCard != currentCard) { scalevalue = selectedCard.transform.localScale; once = false; }

                hit.collider.gameObject.transform.position = ray.GetPoint(0f);

            }
            //if (hit.collider.gameObject.layer == 9) //pieces handler "PiecesLayer"
            //{
            //    hit.collider.gameObject.transform.position = ray.GetPoint(0f);
            //}

            //if (Mouseselect){ selctedcard.transform.localScale = selctedcard.transform.localScale * 3; }
            //else { selctedcard.transform.localScale = selctedcard.transform.localScale / 3; }
        }
    }
    GridPos ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x), (int)Mathf.Round(worldPosition.y - gridStartPosition.y));
    }

    void SpawnGrid(Vector2 _startlocation)
    {
        Tiles = new GameObject[GridSystem.xSize,GridSystem.ySize];
        Debug.Log(GridSystem.xSize * GridSystem.ySize + " is the amount of tiles availible");
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
                Tiles[i, j] = squareColor;
                Instantiate(Tiles[i, j], new Vector3(_startlocation.x + i, _startlocation.y + j, 0.1f), new Quaternion(0, 0, 0, 0));
                
            }
        }
        
    }


    void SpawnPieces(Vector2 _startlocation, GameObject _Black, GameObject _White)
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
                    squareColor = _White;
                }
                else
                {
                    // continue; if we only want white pieces
                    squareColor = _Black;
                }
                if (squareColor != null)
                {
                    Instantiate(squareColor, new Vector3(_startlocation.x + i, _startlocation.y + j, -1), new Quaternion(0, 0, 0, 0));
                    //  squareColor.AddComponent<BoxCollider2D>();
                }
            }
        }
    }

    void spawnChecker(GridPos _initPos, bool _color)
    {
        Debug.Log("Spawning Checker");
        GameObject temp;
        if (_color)
        {
           temp = Instantiate(blackPiece, new Vector2(gridStartPosition.x + _initPos.x, gridStartPosition.y + _initPos.y), new Quaternion(0, 0, 0, 0));
        }
        else
        {
           temp = Instantiate(whitePiece, new Vector2(gridStartPosition.x + _initPos.x, gridStartPosition.y + _initPos.y), new Quaternion(0, 0, 0, 0));
        }

        new Checker(_initPos, _color, temp);
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
