using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whitecardstate : Gamestate
{
    GameObject handvisual;
    bool turnFinished = false;
    public Whitecardstate()
    {
        Transitions = new List<StateTransition>();
        Transitions.Add(new StateTransition(typeof(WhiteTurn), () => turnFinished == true));
        Transitions.Add(new StateTransition(typeof(BlackTurn), () => turnFinished == true));
        handvisual = Resources.Load("Prefabs/hand") as GameObject;
        handvisual = Object.Instantiate(handvisual);
    }

    public override void Enter()
    {
        base.Enter();

    }
    public override void LogicUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //check if we are over a card DONE
            //move the ui component Kinda DONE
            // avtivate effect NOT YET
            Debug.Log("Check");
            RaycastHit2D hit = Physics2D.Raycast(handvisual.transform.position,Camera.main.transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(handvisual.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Color.white, 1f, true);
            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                //foreach (Transform child in HandUI.transform)
                //{
                //    if (hit.transform.transform == child)
                //    {
                //        child.gameObject.transform.position = handvisual.transform.position;
                //    }
                //}

            }
        }

    }
    public override void Exit()
    {
        base.Exit();
    }

}
public class Blackcardstate : Gamestate
{
    bool turnFinished = false;
    public Blackcardstate()
    {
        Transitions = new List<StateTransition>();
        Transitions.Add(new StateTransition(typeof(WhiteTurn), () => turnFinished == true));
        Transitions.Add(new StateTransition(typeof(BlackTurn), () => turnFinished == true));
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    //check if we are over a card DONE
        //    //move the ui component Kinda DONE
        //    // avtivate effect NOT YET
        //    Debug.Log("Check");
        //    RaycastHit2D hit = Physics2D.Raycast(handvisual.transform.position, transform.TransformDirection(Vector3.forward));
        //    Debug.DrawRay(handvisual.transform.position, transform.TransformDirection(Vector3.forward), Color.white, 1f, true);
        //    if (hit.collider != null)
        //    {
        //        Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        //        foreach (Transform child in HandUI.transform)
        //        {
        //            if (hit.transform.transform == child)
        //            {
        //                child.gameObject.transform.position = handvisual.transform.position;
        //            }
        //        }

        //    }
        //}

    }
    public override void Exit()
    {
        base.Exit();
    }

}
