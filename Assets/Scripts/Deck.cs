using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    Deck deck = new Deck();
    int decksize = 10;
    private ObjectPool<CardItem> pool;
    Deck()
    {
        pool = new ObjectPool<CardItem>();
        Generatedeck();
    }

    void Generatedeck()
    {
        for (int i = 0; i < decksize; i++)
        {
            //string s = i.ToString();
            CardItem s = new CardItem();
            pool.ReturnObjectToPool(s); //add card to pool
        }
//            deck1.RequestItem();

        //  deck1.ReturnObjectToPool() //add to obj pool
    }

    void Shuffledeck()
    {

    }
    void Drawcard()
    {

    }
}
