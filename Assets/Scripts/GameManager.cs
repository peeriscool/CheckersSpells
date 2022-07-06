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
    public GameObject blackSquare, whiteSquare;
    public GameObject handvisual;
    Hand hand;
    private InputHandler inputHandler;

    // InventoryManager hand;
  //  Hand hand;
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
    DisplayInventory HandUIDisplay;
    // Start is called before the first frame update
    void Start()
    {
        //hand = new Hand(handvisual);
        //since there is only one canvas, we can search for it
        canvas = FindObjectOfType<Canvas>();
        stateMachine = new GameStateMachine(typeof(WhiteTurn));

        inputHandler = new InputHandler();
        inputHandler.BindInput(KeyCode.Escape, new PauseCommand());

        //Instantiate(StartMenu, canvas.transform);

        // hand = new InventoryManager(cards, 10);
        StartGame();
    }

    private void Update()
    {
       //handvisual.transform.position = hand.Tick();
    //    Debug.Log(selectedPlaceable);

        if(Input.GetMouseButtonDown(1))
        {
            //check if we are over a card
            //move the ui component
            // avtivate effect
            //Transform card = HandUI.GetComponentInChildren<Transform>();
            //card.position = hand.Tick();
            
            
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

        stateMachine.StateUpdate();
    }

    void StartGame()
    {
        //HandUI.SetActive(true);
        //HandUIDisplay = new DisplayInventory(400, 400, 10, 10, 10, player1, HandUI);
        //HandUIDisplay.CreateDisplay();

        //set the initial gridsize. Initial size will always be 8x8, because thats the size of a regular checkerboard
        //If grid is bigger than camera view, move camera
        GridSystem.SetGridSize(8,8);

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


    void SpawnChecker(GridPos _initPos, int _placeableType)
    {
        Debug.Log("Spawning Checker");
        GridSystem.AddPlaceable(_placeableType, _initPos);
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
