using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand
{
   // private GameObject hand;
    CursorHand Visual;
    GameObject hand;
    /// <summary>
    ///  using the hand of the player with a visual
    /// </summary>

    public Hand()
    {
    }
    public void Initialize()
    {
        hand = Resources.Load("Prefabs/hand") as GameObject;
        hand = Object.Instantiate(hand);
        Visual = new CursorHand(hand, hand.GetComponent<Animator>());
    }
    public Vector3 Tick()
    {
        return UpdateVisual();
    }
    public void Ticklocal()
    {
        hand.transform.position = UpdateVisual();
    }
    Vector3 UpdateVisual()
    {
        Visual.recieveInput(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.Mouse1));
        return Visual.UpdatehandVisual();
    }
}
