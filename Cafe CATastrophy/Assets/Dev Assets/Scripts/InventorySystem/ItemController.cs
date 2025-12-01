using UnityEngine;

public class ItemController : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;

    private float cooldownTimer = 0;

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            //Debug.Log(cooldownTimer);
        }
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
        if (cooldownTimer <= 0 && interactScript.isInteracting && inventoryItem) //object can give an item (machine) so pick it up
        {
            inventoryManager.AddItem(inventoryItem);
            cooldownTimer = 2;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
