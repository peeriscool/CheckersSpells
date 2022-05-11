using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckMono : MonoBehaviour
{
    DeckSystem deck1 = new DeckSystem();
   
    // Start is called before the first frame update
    void Start()
    {
        deck1.initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
