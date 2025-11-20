using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventoryItems> Items = new List<InventoryItems>();

    public void Awake()
    {
        //clear inventory when game starts
        Items.Clear();
    }

    public void Update()
    {
        if (Items.Count > 1)
        {
            //limit inventory size to 1
            Items.RemoveAt(1);
        }
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
