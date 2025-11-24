using UnityEngine;

public class Combiner : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public GameObject[] ingredients;

    public InventoryItems item1;
    public InventoryItems item2;

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
        if (interactScript.isInteracting && inventoryManager.Items.Count > 0)
        {
            //fills item1 and item2 
            if (item1 == null)
            {
                item1 = inventoryManager.Items[0];
                inventoryManager.RemoveItem(item1);
            }
            else if (item2 == null)
            {
                item2 = inventoryManager.Items[0];
                inventoryManager.RemoveItem(item2);
            }
        }


        //combiner for all ingredients

    }

    //2 ingredients tegelijk, if [0] + [1] == recipe then combine to new object
}
