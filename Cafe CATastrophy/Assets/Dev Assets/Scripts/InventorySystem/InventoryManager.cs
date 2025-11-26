using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventoryItems> Items = new List<InventoryItems>();
    public PlayerManagerGD1 playerManager;

    public GameObject P1Inv;
    public GameObject P2Inv;

    public void Awake()
    {
        //clear inventory when game starts
        Items.Clear();
    }

    public void Update()
    {
        /*
        if (Items.Count > 1)
        {
            //limit inventory size to 1
            Items.RemoveAt(1);
        }

        //change based on which player is active ???
        if (Items[0] != null)
        {
            P1Inv.GetComponent<SpriteRenderer>().sprite = Items[0].icon;
            P2Inv.GetComponent<SpriteRenderer>().sprite = Items[0].icon;
        }
        */
        //ff uitgezet want was errors aan het spammen in console
    }

    public void ClearInventory()
    {
        Items.Clear();
    }

    public void AddItem(InventoryItems item)
    {
        Items.Add(item);
    }

    public void RemoveItem(InventoryItems item) {
        Items.Remove(item);
    }
}
