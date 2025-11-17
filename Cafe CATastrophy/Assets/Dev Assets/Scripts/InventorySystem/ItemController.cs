using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactScript = other.gameObject.GetComponent<InteractAbility>();
            inventoryManager = other.gameObject.GetComponent<InventoryManager>();
            inRange = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (interactScript.isInteracting)
        {
            inventoryManager.AddItem(inventoryItem);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
