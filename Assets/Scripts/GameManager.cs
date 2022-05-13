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

   // public Card_ScriptableObject[] cards;
    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;
    public GameObject handvisual;
    private InputHandler inputHandler;

    // InventoryManager hand;
    Hand hand;
    private IPlaceable selectedPlaceable;
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
        hand = new Hand(handvisual);
        inputHandler = new InputHandler();
        inputHandler.BindInput(KeyCode.Escape, new PauseCommand());

       // hand = new InventoryManager(cards, 10);

        StartGame();
    }

    private void Update()
    {
     //  handvisual.transform.position = hand.Tick();
        Debug.Log(selectedPlaceable);


        //replace this with input from the inputHandler
        if (Input.GetMouseButtonDown(0))
        {
            IPlaceable clickedTile = GridSystem.checkGridPosition(ClickOnTiles());
            GridPos clickedPos = ClickOnTiles();

            if (selectedPlaceable != null)
            {
                if (clickedTile != null)
                    GridSystem.AttackChecker(selectedPlaceable.myPos, clickedPos);

                else if (clickedTile == null)
                    GridSystem.MoveChecker(selectedPlaceable.myPos, clickedPos);

                selectedPlaceable = null;
            }

            else if (selectedPlaceable == null)
            {
                if (clickedTile != null)
                    selectedPlaceable = clickedTile;
            }
        }
    }

    GridPos ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x), (int)Mathf.Round(worldPosition.y - gridStartPosition.y));
    }

    void StartGame()
    {
        GameObject.Find("UI").SetActive(false);
   
        //set the initial gridsize. Initial size will always be 8x8, because thats the size of a regular checkerboard
        GridSystem.SetGridSize(8, 8);
        SpawnGrid();

        //if the number of rows is bigger than half of the board, the players can't both get that amount of rows, so an error is given
        if (rowsOfCheckers > GridSystem.ySize / 2)
        {
            Debug.LogError("Rows of checkers exceed the useable capacity. Make the rows smallers, or the grid larger");
        }


        //I spawn checkers based on the gridSize
        int pieceColor = 2;
        for (int i = 0; i < GridSystem.ySize; i++)
        {
            if (i < rowsOfCheckers)
                pieceColor = 1;

            if (i >= GridSystem.ySize - rowsOfCheckers)
                pieceColor = 0;

            if (i >= rowsOfCheckers && i < GridSystem.ySize - rowsOfCheckers)
                continue;

            for (int j = 0; j < GridSystem.xSize; j++)
            {
                if ((i + j) % 2 == 0)
                    SpawnChecker(new GridPos(j, i), pieceColor);
            }
        }
    }

    void SpawnGrid()
    {
        tiles = new GameObject[GridSystem.xSize,GridSystem.ySize];

        //Tells me how many tiles there are in total
        Debug.Log(GridSystem.xSize * GridSystem.ySize + " is the amount of tiles availible");

        //grid is at the center of the screen, so the start position will be taken from there
        gridStartPosition = new Vector2(Camera.main.transform.position.x - GridSystem.xSize / 2 + 0.5f, Camera.main.transform.position.y - GridSystem.ySize / 2 + 0.5f);
        
        //let's spawn the tiles
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
                //instantiate(gridsysyem())
            }
        }
        
    }

    void SpawnAllCheckers()
    {

    }


    void SpawnChecker(GridPos _initPos, int _color)
    {
        Debug.Log("Spawning Checker");
        GameObject temp = null;

        //black piece
        if (_color == 0)
        {
            temp = blackPiece;
        }

        //white piece
        if(_color == 1)
        {
            temp = whitePiece;
        }

        if(temp == null)
        {
            return;
        }

        Instantiate(temp, new Vector2(gridStartPosition.x + _initPos.x, gridStartPosition.y + _initPos.y), new Quaternion(0, 0, 0, 0));
        GridSystem.AddPlaceable(new Checker(_initPos, _color, temp), _initPos);
    }

}

//failed attempt at making fetching components more efficient
/*

//maybe give this its own file

//The componentFetcher is static so that it can be referenced by any class wherever.
static class ComponentFetcher
{
    //when referenced, we can search for a specific component at the specific name of a GameObject
    //must enter the componentType as: typeof(nameofcomponentyouwant)
    public static Component fetchComponent(string ObjectName, System.Type componentType)
    {
        GameObject foundGameobject = GameObject.Find(ObjectName);

        //If the name can't be found, we return null
        if (foundGameobject = null)
        {
            Debug.Log("Couldn't find GameObject with the name: " + ObjectName);
            return null;
        }

        Component componentTemp = foundGameobject.GetComponent(componentType);

        //if the name does exist, and it has the component, we return that component
        if (componentTemp != null)
        {
            Debug.Log("Component of correct type found");
            return componentTemp;
        }

        //if the object doesn't have the component, we will search in its children
        Debug.Log("Couldn't find component in GameObject. Searching for component in Children");

        Component[] childrencomponents = foundGameobject.GetComponentsInChildren(componentType);
        if (childrencomponents.Length > 1)
        {
            Debug.Log("There's more than one component in its children. returning null");
            return null;
        }

        //only when in its children there is one component, we will return it, otherwise we won't have enough information to specify which component we need
        if (childrencomponents.Length == 1)
        {
            Debug.Log("Found one component in GameObject's children, returning component");
            return childrencomponents[0];
        }

        else
        {
            Debug.Log("No components found");
            return null;
        }
    }
}

*/
