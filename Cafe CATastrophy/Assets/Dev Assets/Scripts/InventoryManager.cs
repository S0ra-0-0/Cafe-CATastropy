using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventoryItems> Items = new List<InventoryItems>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(InventoryItems item)
    {
        Items.Add(item);
    }

    public void RemoveItem(InventoryItems item) {
        Items.Remove(item);
    }
}
