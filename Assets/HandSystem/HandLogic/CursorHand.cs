using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CursorHand
{
    //works without monobehavior
    //get animation controller for hand
    //bind hand to mouse position.
    //play animation when clicked

    // Start is called before the first frame update
    public Animator HandController;
    public GameObject Hand;
    Vector3 TrackedPos;
    float time;

    public CursorHand(GameObject _hand,Animator _Handcontroller )
    {
        HandController = _Handcontroller;
        Hand = _hand;
        InitializeVisual();
    }
    void InitializeVisual()
    {
        HandController.enabled = true;
        TrackedPos = Vector3.zero;
        Debug.Log(HandController.layerCount + "Hand Positions Availible");
    }
   public void recieveInput(Vector3 Pos,bool MouseLeft, bool MouseRight ) //gets the mouse position and click functions
    {
         TrackedPos = new Vector3(Pos.x,Pos.y,0f);

        if (MouseLeft)
        {
            Debug.Log("mouse left" + Hand.transform.position);
            HandController.Play("Armature|Hand grab");
          //  CastRay(true);
            //card selected
            //checker selected
            return;
        }
        if (MouseRight)
        {
            Debug.Log("mouse right" + TrackedPos);
            playanimation("Armature|Hand cards");
            HandController.ResetTrigger("cursormode");
            //card removed
            //checker removed
            return;
        } 
   }

    //void CastRay(bool _mouseselect) //see if we select a card
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
    //    if (hit)
    //    {
    //        if (hit.collider.gameObject.layer == 8) //card handler "CardLayer"
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            hit.collider.gameObject.transform.position = ray.GetPoint(0f);
    //        }
    //    }
    //}
    public Vector3 UpdateHandVisual()
    {
    return TrackedPos;
    }

    public void playanimation(string name)
    {
        HandController.Play(name);
    }
}

//public class Whitehand //returns true if white is at play
//{
//    Gamestate myTurn;
//    public bool MyTurn(Gamestate _current, WhiteTurn _myTurn)
//    {
//        myTurn = _myTurn;
//        return (_current == _myTurn);   
//    }
//}
//public class Blackhand //returns true if black is at play
//{
//    Gamestate myTurn;
//    public bool MyTurn(Gamestate _current, WhiteTurn _myTurn)
//    {
//        myTurn = _myTurn;
//        return (_current == _myTurn);
//    }
//}
