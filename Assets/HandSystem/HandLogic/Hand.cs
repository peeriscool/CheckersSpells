using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
   // private GameObject hand;
    CursorHand Visual;
    /// <summary>
    /// Start using the hand of the player
    /// </summary>
    /// <param name="_hand">Animator Required</param>
    public Hand(GameObject _hand)
    {
        Visual = new CursorHand(_hand, _hand.GetComponent<Animator>());
    }
    public Vector3 Tick()
    {
        return UpdateVisual();
    }
    Vector3 UpdateVisual()
    {
        Visual.recieveInput(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.Mouse1));
        return Visual.UpdateHandVisual();
    }
}
