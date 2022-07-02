using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Canvas canvas;
    GameStateMachine stateMachine;
    //this determines how many rows of checkers each player gets at the start
    public int rowsOfCheckers = 3;

    //the size of the board
    public int boardX, boardY;
    [SerializeField]
    private GameObject StartMenu;


    // public Card_ScriptableObject[] cards;
    public GameObject blackSquare, whiteSquare, blackPiece, whitePiece;
    public GameObject handvisual;
    Hand hand;
    private InputHandler inputHandler;

    // InventoryManager hand;
  //  Hand hand;
    private IPlaceable selectedPlaceable;
    private Vector2 gridStartPosition;
    private Vector3 scaleValue;
    private bool mouseSelect = true;
    private bool once = true;
    private GameObject currentCard;
    private GameObject selectedCard;

    [SerializeField]
    protected GameObject HandUI;

    [SerializeField]
    protected InventoryObject player1;
    // Start is called before the first frame update
    void Start()
    {
        hand = new Hand(handvisual);
        //since there is only one canvas, we can search for it
        canvas = FindObjectOfType<Canvas>();
        stateMachine = new GameStateMachine();

        inputHandler = new InputHandler();
        inputHandler.BindInput(KeyCode.Escape, new PauseCommand());

        //Instantiate(StartMenu, canvas.transform);

        // hand = new InventoryManager(cards, 10);

        stateMachine.Initialize();
        StartGame();
    }

    private void Update()
    {
       handvisual.transform.position = hand.Tick();
    //    Debug.Log(selectedPlaceable);


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
        if(Input.GetMouseButtonDown(1))
        {
            //check if we are over a card
            //move the ui component
            // avtivate effect
            Transform card = HandUI.GetComponentInChildren<Transform>();
            card.position = hand.Tick();
            //if (true)
            //{
            //    Debug.Log("Card check");
            //    RaycastHit hit;
            //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, 8)) //8 = cardlayer
            //    {
            //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            //        Debug.Log("Did Hit");
            //    }
            //}
        }
    }

    GridPos ClickOnTiles()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(worldPosition);

        return new GridPos((int)Mathf.Round(worldPosition.x - gridStartPosition.x), (int)Mathf.Round(worldPosition.y - gridStartPosition.y));
        //  handvisual.transform.position = hand.Tick();
        //    Debug.Log(selectedPlaceable);

        stateMachine.StateUpdate();
    }

    void StartGame()
    {
        HandUI.SetActive(true);
        DisplayInventory HandUIDisplay = new DisplayInventory(400, 400, 10, 10, 10, player1, HandUI);
        HandUIDisplay.CreateDisplay();

        //set the initial gridsize. Initial size will always be 8x8, because thats the size of a regular checkerboard
        GridSystem.SetGridSize(8, 8);

        //grid is at the center of the screen, so the start position will be taken from there
        gridStartPosition = new Vector2(Camera.main.transform.position.x - GridSystem.xSize / 2 + 0.5f, Camera.main.transform.position.y - GridSystem.ySize / 2 + 0.5f);

        GridSystem.InitializeGrid(gridStartPosition, blackSquare, whiteSquare);

        SpawnAllCheckers();
    }

    void SpawnAllCheckers()
    {

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

        GameObject visualRepresentation = Instantiate(temp, new Vector2(gridStartPosition.x + _initPos.x, gridStartPosition.y + _initPos.y), new Quaternion(0, 0, 0, 0));
        GridSystem.AddPlaceable(new Checker(_initPos, _color, visualRepresentation), _initPos);
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
