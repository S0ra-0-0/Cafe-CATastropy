using NUnit.Framework;
using UnityEngine;

public class CounterFunctionality : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    [SerializeField] InventoryItems itemOnCounter;
    public Transform[] counterPosition;

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
        Debug.Log("player in range of counter");
        for (int i = 0; i < counterPosition.Length; i++)
            {
            //check if counter pos is empty, player is interacting and player has item to put down
                if (counterPosition[i].childCount <= 1 && interactScript.isInteracting && inventoryManager.Items.Count > 0)
                {
                Debug.Log("put down item");
                    itemOnCounter = inventoryManager.Items[0];
                    Instantiate(itemOnCounter.itemPrefab, counterPosition[i]);
                    inventoryManager.Items.RemoveAt(0);
                    break; //exit loop after placing item
                }
            }
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
