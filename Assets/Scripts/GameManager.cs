using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // Checker checkerTest;
    public Card_ScriptableObject[] Cards;
    InventoryManager Hand;
    private GameObject selctedcard;
    private Vector3 Scalevalue;
    private bool Mouseselect = true;
    private bool once = true;
    private GameObject CurrentCard;
    // Start is called before the first frame update
    void Start()
    {
        GridSystem.SetGridSize(8, 8);
       // checkerTest = new Checker(new GridPos(5, 7));
        Debug.Log(GridSystem.checkGridPosition(new GridPos(5, 7)));
        Hand = new InventoryManager(Cards, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Hand.StartTurn();
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed left click, casting ray.");            
            CastRay(Mouseselect);
            if (selctedcard != null)
            {
                Vector3 Mouseinput = Input.mousePosition;
               
                    //hover
                    if(selctedcard.transform.localScale.x <= Scalevalue.x*2)
                    selctedcard.transform.localScale = selctedcard.transform.localScale * 2;
                

            }
        }
        if(Input.GetMouseButtonUp(0))
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
            Debug.Log(hit.collider.gameObject.name);
            if (selctedcard!=null && selctedcard != CurrentCard) { CurrentCard = selctedcard; }//card switch
                selctedcard = hit.collider.gameObject;

            if(once && selctedcard != CurrentCard) { Scalevalue = selctedcard.transform.localScale; once = false; }
            
            hit.collider.gameObject.transform.position = ray.GetPoint(0f);

            //if (Mouseselect){ selctedcard.transform.localScale = selctedcard.transform.localScale * 3; }
            //else { selctedcard.transform.localScale = selctedcard.transform.localScale / 3; }

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
