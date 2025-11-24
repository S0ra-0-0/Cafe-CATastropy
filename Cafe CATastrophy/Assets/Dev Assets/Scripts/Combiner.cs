using UnityEngine;

public class Combiner : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;
    public MachineTimer machineTimer;

    public InventoryItems item1;
    public InventoryItems item2;

    public void Start()
    {
        //make a list or array with all scriptable objects that can be combined & final item

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
                    // vfx that first item has been placed?
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
        }

        //combiner for all ingredients
        if (item1.itemName == "Glass" && item2.itemName == "Tea Leaf")
        {
            if (machineTimer.isFinished)
            {
                //make tea
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
    }
}
