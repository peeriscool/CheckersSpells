using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private ObjectPool<CardItem> pool;
    DisplayInventory inventorycards;
    public Deck()
    {
        pool = new ObjectPool<CardItem>();
    }

  public void Generatedeck(DisplayInventory _inventorycards)
    {
        for (int i = 0; i < _inventorycards.inventory.Container.items.Count; i++)
        {
            CardItem carditem = new CardItem();
            carditem = _inventorycards.inventory.database.Items[i] as CardItem;
            pool.ReturnObjectToPool(carditem); //add card to pool
        }
        inventorycards = _inventorycards;
    }
    //void Shuffledeck()
    //{

    //}
   public void CheckCardPosition(Vector2 _checkpos)
    {
        CardItem test = inventorycards.inventory.database.Items[0] as CardItem;
        if(test.InRangeofCard(_checkpos))
        {

        }
    }
   public CardItem Drawcard()
    {
        return pool.RequestItem();
    }

    public ItemObject.myeffect Effect()
    {
        Debug.Log("Example spawn obstacle");
       // GameObject.CreatePrimitive(PrimitiveType.Cube);
        return null;
    }
    void BindEffect(CardItem _card)
    {
        _card = Drawcard();
        _card.effect += Effect();
        Debug.Log(_card.description);
        Debug.Log(_card.UI.name);
    }
}
