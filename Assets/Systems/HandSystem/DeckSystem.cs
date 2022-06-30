using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface DeckSystem
    {
   // Hand p1 = new Hand();
   // Hand p2 = new Hand();
    List<Cards> deck { get; set; }
    int handcount { get; }
//    Cards card = new Cards(Cards.Effect.Effecttpye.deletecolumn); //make a card with an effect

   
     event EventHandler effecthandler;
    void initialize();
    //{
    //    //deck = new List<Cards>();
    //    //cards = new List<int>();
    //   // card.setabbility(card.effect);
    //}
    void generateDeck();
}