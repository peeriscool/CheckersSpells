using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Checker checkerTest;
    public Card_ScriptableObject[] Cards;
    InventoryManager Hand;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
        checkerTest = new Checker(new GridPos(5, 7), true);
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)).BlackOrWhite);
        Hand = new InventoryManager(Cards, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Hand.StartTurn();
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
