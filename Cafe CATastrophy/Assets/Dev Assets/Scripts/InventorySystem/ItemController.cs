using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        interactScript = GameObject.Find("InteractionSystem").GetComponent<InteractAbility>();
    }

    public void OnCollisionEnter(Collision collision)
    {
            inventoryManager.AddItem(inventoryItem);
    }
}
