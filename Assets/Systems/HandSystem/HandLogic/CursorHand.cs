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
         TrackedPos = new Vector3(Pos.x,Pos.y,-4f);

        if (MouseLeft)
        {
            HandController.Play("Armature|Hand grab");
            //card selected
            //checker selected
            return;
        }
        if (MouseRight)
        {
            playanimation("Armature|Hand cards");
            HandController.ResetTrigger("cursormode");
            return;
        } 
   }

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
