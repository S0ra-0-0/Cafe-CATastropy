using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    [SerializeField] GameObject itemOnCounter;

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
        if (interactScript.isInteracting && inventoryItem)
        {
            inventoryManager.AddItem(inventoryItem);
        }
        if (interactScript.isInteracting && !inventoryItem)
        {
            Instantiate(inventoryManager.Items[0], itemOnCounter.transform);
            inventoryManager.Items.RemoveAt(0);
            //item neerzetten op counter
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
