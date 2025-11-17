using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    [SerializeField] InventoryItems itemOnCounter;
    private GameObject counterItemInstance;
    public Transform counterPosition;

    private void Start()
    {
        counterItemInstance = itemOnCounter.itemPrefab;
    }
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
            itemOnCounter = inventoryManager.Items[0];
            Instantiate(itemOnCounter.itemPrefab, counterPosition.position, counterPosition.rotation);
            inventoryManager.Items.RemoveAt(0);
            //item neerzetten op counter
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
