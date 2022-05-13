using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    Card_ScriptableObject[] collection;
    Card_ScriptableObject[] Deck;
    Vector3 handofset= new Vector3(2, 0, 0);
    int cardsinhand = 0;
    int maxcards = 8;
    public InventoryManager(Card_ScriptableObject[] playcards, int decksize)
    {
        collection = playcards;
        Deck = new Card_ScriptableObject[decksize];
        for (int i = 0; i < decksize; i++) //fill deck with random cards based on complete collection
        {
            Deck[i] = collection[Random.Range(0, collection.Length)];
        }
    }

    public void StartTurn()
    {
        DeckDraw();
    }
    private void DeckDraw()
    {
        cardsinhand++;
        if (cardsinhand <= maxcards)
        {
            Card_ScriptableObject spawn = Deck[Random.Range(0, Deck.Length)];
            GameObject Spawnable = GameObject.Instantiate(spawn.prefab);
            Spawnable.transform.position += handofset;
            Spawnable.AddComponent<BoxCollider2D>();
            handofset.x += 2;
            //GameObject.Instantiate(Spawnable);
            Debug.Log("DeckDraw");
        }
    }
}



