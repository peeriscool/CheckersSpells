using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//https://docs.unity3d.com/2019.1/Documentation/ScriptReference/EventSystems.UIBehaviour.html
public class DisplayInventory
{
    public InventoryObject inventory; //displayed inventory
                                      //public ItemInteraction activeinventory;
    public GameObject inventoryprefab; //item that holds all sprites


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
    public DisplayInventory(int x,int y, int xpace, int column, int yspace, InventoryObject _inventory, GameObject _inventoryprefab)
    {
        X_start = x;
        Y_start = y;
        X_Spacer = xpace;
        Column = column;
        Y_Spacer = yspace;
        inventory = _inventory;   
        inventoryprefab = _inventoryprefab;
    }
  

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    //private void Init()
    //{
    //    //if(activeinventory != null & inventory == null) //use item interaction inventory when we have no inventory
    //    //{
    //    //    inventory = activeinventory.inventory;
    //    //}
    //    CreateDisplay();
    //}
    private void Update()
    {
        UpdateDisplay();
    }

    public Vector3 GetPosition(int index)//assign inventory location
    {
        return new Vector3(X_start + (X_Spacer * (index % Column)), Y_start + (-Y_Spacer * (index / Column)), 0f); //use start locations
    }
    public void CreateDisplay()
    {
        GameObject obj = inventoryprefab;
        for (int i = 0; i < inventory.Container.items.Count; i++)
        {
            InventorySlot slot = inventory.Container.items[i];
            //var obj = GameObject.Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity,transform);
            //var obj = Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity,  transform);
            Debug.Log("getting items: " + slot.item.id); //+ "sprite: " +obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite);
            for (int j = 0; j < inventory.database.Itemdict.Count; j++)
            {
                obj.transform.GetChild(j).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[j].UI;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
         //   obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[slot.item.id].UI;  //inventory.Container[i].item.UI
          //  obj.transform.GetChild(1).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[slot.item.id].UI;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
          //  obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0"); //n0 = format with commas           
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
                itemsDisplayed[slot].GetComponentInChildren<TextMeshPro>().text = slot.amount.ToString("n0");
            }
            else //new item 
            {
                //to do fix GetItem[slot.item.id].UI.name  make sure there is a name
                var obj = GameObject.Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity); //mising transform
                for (int j = 0; j < inventory.database.Itemdict.Count; j++)
                {
                    obj.transform.GetChild(j).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[slot.item.id].UI;
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                } 
                obj.GetComponentInChildren<TextMeshPro>().text = slot.amount.ToString("n0"); //n0 format with commas
                itemsDisplayed.Add(inventory.Container.items[i], obj);
            }
        }
    }
}
