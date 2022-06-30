using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Cards.Effect effect = new Cards.Effect();
public class Cards : DeckSystem
{
    public List<Cards> deck { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int handcount => throw new NotImplementedException();

    //   Effect myeffect = new Effect();
    public Cards(Effect.Effecttpye _param)
    {
        //myeffect.mytype = _param;
    }
    public event EventHandler cardeffect;
    public event EventHandler effecthandler;

    public void setabbility(Cards.Effect e)
    {
        //if(e.mytype == 0)
        //{
        //    e.effecthandler += tick;
        //}
        Debug.Log("COM");
    }

    static void tick(object sender, EventArgs e)
    {
        Debug.Log("tik tok");
    }

    public virtual void runfunction(object sender, EventArgs e)
    {
        EventHandler handler = cardeffect;
        handler?.Invoke(this, e);
        Debug.Log("Runf");
    }

    public void initialize()
    {
         deck = new List<Cards>();
     //   cards = new List<int>();

    }

    public void generateDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i] = new Cards(Cards.Effect.Effecttpye.deletecolumn);
        }
    }

    public class Effect
    {
       public enum Effecttpye
        {
            insertrow,
            deleterow,
            insertcolumn,
            deletecolumn,
            insertshape,
            deleteshape,
        }

        public event EventHandler effecthandler;
        protected virtual void effect(EventArgs e)
        {
            EventHandler handler = effecthandler;
            handler?.Invoke(this, e);
        }
    }
}
/// <summary>
/// insert row          .1
/// delete row          .2 
/// insert column       .3
/// delete column       .4
/// insert shape        .5
/// delete shape        .6
/// </summary>
/// 






