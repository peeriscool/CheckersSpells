using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//https://docs.unity3d.com/2019.1/Documentation/ScriptReference/EventSystems.UIBehaviour.html
public class DisplayInventory //hand visualizer
{
    public static GameObject Inventory;
    public InventoryObject inventory; //displayed inventory
    public GameObject inventoryprefab; //item that gets card sprites assigned
    public int X_start; //horizontal offset
    public int Y_start; //vertical offset
    public int X_Spacer;
    public int Column;
    public int Y_Spacer;
    /// <summary>
    /// inventory sytem for cards
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xpace"></param>
    /// <param name="column"></param>
    /// <param name="yspace"></param>
    /// <param name="_inventory"></param>
    /// <param name="_inventoryprefab"></param>
    public DisplayInventory(int x,int y, int xpace, int column, int yspace, InventoryObject _inventory)
    {
        X_start = x;
        Y_start = y;
        X_Spacer = xpace;
        Column = column;
        Y_Spacer = yspace;
        inventory = _inventory;
        inventoryprefab = GenerateInventory(inventory);
    }
    public static void AddCardToInventory()
    {
        var obj = Object.Instantiate(Resources.Load("Prefabs/Card") as GameObject);
        obj.name = "card";
        obj.transform.SetParent(Inventory.transform);
        //Todo we need to assign the carditem
    }
    private GameObject GenerateInventory(InventoryObject _inventory)
    {
        GameObject Inventoryholder = Object.Instantiate(new GameObject());
        Inventoryholder.name = "Inventory";
        Debug.Log(_inventory.Container.items);
        foreach (InventorySlot slot in _inventory.Container.items)
        {
            for (int i = 0; i < slot.amount; i++)
            {
                var obj = Object.Instantiate(Resources.Load("Prefabs/Card") as GameObject);
                obj.name = "card";
                obj.transform.SetParent(Inventoryholder.transform);
            }
        }
        Inventory = Inventoryholder;
       return Inventory; //_inventoryprefab;
    }

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    public Vector3 GetPosition(int index)//assign inventory location
    {
        return new Vector3(X_start + (X_Spacer * (index % Column)), Y_start + (-Y_Spacer * (index / Column)), 0f); //use start locations with offset --0--0--0--;
    }
    public void CreateDisplay()
    {
        GameObject obj = inventoryprefab;
        for (int i = 0; i < inventory.Container.items.Count; i++)
        {
            InventorySlot slot = inventory.Container.items[i];
            Debug.Log("getting items: " + slot.item.id); //+ "sprite: " +obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite);
            obj.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().sprite = inventory.database.Itemdict[i].UI;
            obj.transform.GetChild(i).GetComponent<Transform>().localPosition = GetPosition(i);
            obj.transform.GetChild(i).GetComponent<Transform>().localPosition = GetPosition(i);
            itemsDisplayed.Add(slot, obj);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.items.Count; i++) //item already exist update count
        {
            InventorySlot slot = inventory.Container.items[i];
            if (itemsDisplayed.ContainsKey(slot))
            {
                //          itemsDisplayed[slot].GetComponentInChildren<TextMeshPro>().text = slot.amount.ToString("n0");
                return;
            }
            else //new item 
            {
                    var obj = GameObject.Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity); //mising transform
                    obj.transform.GetChild(i).GetComponentInChildren<SpriteRenderer>().sprite = inventory.database.Itemdict[slot.item.id].UI;
                    obj.GetComponent<Transform>().localPosition = GetPosition(i);
                    itemsDisplayed.Add(inventory.Container.items[i], obj);
            }
        }
    }
}
