using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private ObjectPool<CardItem> pool;
    ItemDatabase database;
    public Deck()
    {
        pool = new ObjectPool<CardItem>();
        database = Resources.Load("Allcards_Database 1") as ItemDatabase;
    }

  public void Generatedeck()
    {
        Debug.Log("cards known " + database.Items.Length);
        for (int i = 0; i < database.Items.Length; i++)
        {
            CardItem carditem = new CardItem();
            carditem = database.Items[i] as CardItem;
            carditem.mypos = new Vector2(i, 0);
             pool.ReturnObjectToPool(carditem); //add card to pool
        }
     
    }
 
   public void CheckCardPosition(Vector2 _checkpos)
    {
        Debug.Log("Checking deck for correct card" + _checkpos);

        foreach (CardItem card in database.Items)
        {
            if (card.InRangeofCard(_checkpos))
            {
                for (int i = 0; i < DisplayInventory.Inventory.transform.childCount; i++) //for every child object visualised on screen
                {
                    card.instancedrefrence = DisplayInventory.Inventory.transform.GetChild(i).gameObject; //assign gameobject to refrence of scriptable obejct
                }
                Debug.Log("Selected ="+card.name + "loading effect");
                card.effect += Effect(card);
            }
        }
    }
   public CardItem Drawcard()
    {
        return pool.RequestItem();
    }

    public ItemObject.myeffect Effect(CardItem _item)
    {
        GameObject.Destroy( _item.instancedrefrence);
        Debug.Log("Item Destroyed");
        return null;
    }
    void BindEffect(CardItem _card)
    {
        _card = Drawcard();
        _card.effect += Effect(_card);
        Debug.Log(_card.description);
        Debug.Log(_card.UI.name);
    }
}
