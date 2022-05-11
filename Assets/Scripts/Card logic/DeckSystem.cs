using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeckSystem
    {
   // Hand p1 = new Hand();
   // Hand p2 = new Hand();
    List<Cards> deck;
    List<int> cards;
    int Deckcount = 52;
    Cards card = new Cards(Cards.Effect.Effecttpye.deletecolumn); //make a card with an effect

   
    public event EventHandler effecthandler;
    public void initialize()
    {
        deck = new List<Cards>();
        cards = new List<int>();
       // card.setabbility(card.effect);
    }
    void generateDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i] = new Cards(Cards.Effect.Effecttpye.deletecolumn);
        }
    }
}