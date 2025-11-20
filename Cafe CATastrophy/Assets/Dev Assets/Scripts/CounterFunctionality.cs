using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class CounterFunctionality : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    [SerializeField] InventoryItems itemOnCounter;
    public Transform[] counterPosition;

    private float cooldownTimer = 0;

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            Debug.Log(cooldownTimer);
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
        if (other.CompareTag("Player"))
        {
            if (itemOnCounter == null) //only do this is counter is empty
            {
                for (int i = 0; i < counterPosition.Length; i++)
                {
                    //check if player is interacting and player has item to put down
                    if (cooldownTimer <= 0 && interactScript.isInteracting && inventoryManager.Items.Count > 0)
                    {
                        itemOnCounter = inventoryManager.Items[0];
                        Instantiate(itemOnCounter.itemPrefab, counterPosition[i]);
                        inventoryManager.Items.RemoveAt(0);
                        cooldownTimer = 1;
                        break; //exit loop after placing item
                    }
                }
            }

            if (cooldownTimer <= 0 && itemOnCounter != null && interactScript.isInteracting && inventoryManager.Items.Count == 0)
            {
                inventoryManager.AddItem(itemOnCounter);
                itemOnCounter = null;
                cooldownTimer = 1;
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
