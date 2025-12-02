using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;
    public MachineTimer machineTimer;

    public InventoryItems item1;
    public InventoryItems item2;

    public InventoryItems finalProduct;

    private ScriptableObject[] allItems;

    public void Start()
    {
        machineTimer = GetComponent<MachineTimer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactScript = other.gameObject.GetComponent<InteractAbility>();
            inventoryManager = other.gameObject.GetComponent<InventoryManager>();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //placing items into combiner
        if (other.CompareTag("Player"))
        {
            if (interactScript.isInteracting && inventoryManager.Items.Count > 0)
            {
                //fills item1 and item2 
                if (item1 == null)
                {
                    item1 = inventoryManager.Items[0];
                    inventoryManager.RemoveItem(inventoryManager.Items[0]);
                    Debug.Log("First item placed");
                }
                else if (item2 == null)
                {
                    item2 = inventoryManager.Items[0];
                    inventoryManager.RemoveItem(inventoryManager.Items[0]);
                    Debug.Log("Second item placed");
                }
            }
        }

        if (item1 != null)
        {
            machineTimer.StartTimer();
            // make sure timer starts as soon as first item is placed
        }

        //combiner for all ingredients
        TryCreate("Glass", "Tea Leaf", "Tea");
        TryCreate("Glass", "Coffee Beans", "Coffee");
        TryCreate("Glass", "Matcha Powder", "Matcha");
        TryCreate("Plate", "Dough", "Croissant");
        TryCreate("Plate", "Cheese Dough", "Cheese Twist");
        TryCreate("Plate", "Sweet Dough", "Donut");
    }

    private void TryCreate(string item1name, string item2name, string result)
    {
        if (item1 == null && item2 == null) return;
        if (item1.itemName == item1name && item2.itemName == item2name && machineTimer.isFinished)
        {
            item1 = item2 = null;
            Instantiate(GameManager.Instance.GetItem(result).itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
