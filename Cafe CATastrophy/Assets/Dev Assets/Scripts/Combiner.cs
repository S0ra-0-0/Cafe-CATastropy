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

    private ScriptableObject[] allItems;

    public void Start()
    {
        allItems = new ScriptableObject[15];
        //is het wel nodig om alle items te laden hier?

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


        //combiner for all ingredients
        if (item1.itemName == "Glass" && item2.itemName == "Tea Leaf")
        {
            if (machineTimer.isFinished)
            {
                item1 = item2 = null;
                //add item as pickup
                //

            }
        }
        if (item1.itemName == "Glass" && item2.itemName == "Coffee Beans")
        {
            
        }
        if (item1.itemName == "Glass" && item2.itemName == "Matcha Powder")
        {
            
        }
        if (item1.itemName == "Dough" && item2.itemName == "Plate")
        {
            
        }
        if (item1.itemName == "Sweet Dough" && item2.itemName == "Plate")
        {
            
        }
        if (item1.itemName == "Cheese Dough" && item2.itemName == "Plate")
        {
            
        }

        if (item1 != null)
        {
            machineTimer.StartTimer();
            // make sure timer starts as soon as first item is placed
        }
    }
}
