using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peer_Mono_TryOut : MonoBehaviour
{
    public GameObject hand;
    CursorHand Hand;
    // Start is called before the first frame update
    void Start()
    {
        Hand = new CursorHand(hand,hand.GetComponent<Animator>());
    }

    // Update is called once per frame
    void Update()
    {
        Hand.recieveInput(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.GetKeyDown(KeyCode.Mouse0), Input.GetKeyDown(KeyCode.Mouse1));
        hand.transform.position = Hand.UpdateHandVisual();
    }
}
