using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this determines how many rows of checkers each player gets at the start
    public int rowsOfCheckers = 3;

    //the size of the board
    public int boardX, boardY;

    public Card_ScriptableObject[] cards;
    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;

    private GameMenu gameMenu;
    public Button enterButton;

    InventoryManager hand;
    private Checker selectedChecker;
    private Vector2 gridStartPosition;
    private Vector3 scaleValue;
    private bool mouseSelect = true;
    private bool once = true;
    private GameObject currentCard;
    private GameObject[,] tiles;
    private GameObject selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        //working on this, but commenting it so there won't be errors
        //enterButton = fetchComponent("");
        //gameMenu = new GameMenu();
        
        
        //add a listener to the onclick event from the button to start the game
        enterButton.onClick.AddListener(StartGame);
        
        hand = new InventoryManager(cards, 10);

        //if the number of rows is bigger than half of the board, the players can't both get that amount of rows, so an error is given
        if (rowsOfCheckers > GridSystem.ySize / 2)
        {
            Debug.LogError("Rows of checkers exceed the useable capacity. Make the rows smallers, or the grid larger");
        }


        //I spawn checkers based on the gridSize
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
                    SpawnChecker(new GridPos(j, i), pieceColor);
            }
        }
    }

    private void Update()
    {
        Debug.Log(selectedChecker);


        //replace this with input from the inputHandler
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

        if (Input.GetMouseButton(0)) //picking up Cards ToDo: place on grid tile to activate effect.
        {
            //Debug.Log("Pressed left click, casting ray.");
            CastRay(mouseSelect);
            if (selectedCard != null)
            {
                Vector3 Mouseinput = Input.mousePosition;

                //hover
                if (selectedCard.transform.localScale.x <= scaleValue.x * 2)
                    selectedCard.transform.localScale = selectedCard.transform.localScale * 2;
            }
        }

        if (Input.GetMouseButtonUp(0) && currentCard != null)
        {
            currentCard.transform.localScale = scaleValue;
            currentCard = null;
        }

    }
    void CastRay(bool _mouseselect)
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

                if (once && selectedCard != currentCard) { scaleValue = selectedCard.transform.localScale; once = false; }

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

    void StartGame()
    {
        //set the initial gridsize. Initial size will always be 8x8, because thats the size of a regular checkerboard


        CreateNewBoard(8, 8);
        SpawnGrid();
    }

    void CreateNewBoard(int xSize, int ySize)
    {
        GridSystem.SetGridSize(xSize, ySize);
    }

    void SpawnGrid()
    {
        tiles = new GameObject[GridSystem.xSize,GridSystem.ySize];

        //Tells me how many tiles there are in total
        Debug.Log(GridSystem.xSize * GridSystem.ySize + " is the amount of tiles availible");
        for (int i = 0; i < GridSystem.xSize; i++)
        {
            for (int j = 0; j < GridSystem.ySize; j++)
            {
                GameObject squareColor;
                //check if it's even, all even tiles are black, all odd tiles are white. Quick math
                if ((i + j) % 2 == 0)
                {
                    squareColor = blackSquare;
                }
                else
                {
                    squareColor = whiteSquare;
                }
                tiles[i, j] = squareColor;

                //the board will always spawn in the center of the screen. Each tile will spawn individually
                Instantiate(tiles[i, j], new Vector3((Camera.main.transform.position.x - GridSystem.xSize / 2 + 0.5f) + i, (Camera.main.transform.position.y - GridSystem.ySize / 2 + 0.5f) + j, 0.1f), new Quaternion(0, 0, 0, 0));
                
            }
        }
        
    }

    void SpawnAllCheckers()
    {

    }


    void SpawnChecker(GridPos _initPos, bool _color)
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

    Component fetchComponent(string ObjectName, System.Type componentType)
    {
        GameObject foundGameobject = GameObject.Find(ObjectName);

        if(foundGameobject = null)
        {
            Debug.Log("Couldn't find GameObject with the name: " + ObjectName);
            return null;
        }

        Component componentTemp = foundGameobject.GetComponent(componentType);
        if(componentTemp = null)
        {
            Debug.Log("Couldn't find component in GameObject. Searching for component in Children");
        }
        return componentTemp;
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
