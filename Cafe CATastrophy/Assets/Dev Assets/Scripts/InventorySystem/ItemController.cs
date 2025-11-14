using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        interactScript = GameObject.Find("InteractionSystem").GetComponent<InteractAbility>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger" + inRange);
        inRange = true;
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger" + inRange);
        inRange = false;
    }
}
