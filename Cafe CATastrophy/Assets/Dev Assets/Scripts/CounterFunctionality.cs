using NUnit.Framework;
using UnityEngine;

public class CounterFunctionality : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    [SerializeField] InventoryItems itemOnCounter;
    private GameObject counterItemInstance;
    public Transform[] counterPosition;

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
            for (int i = 0; i < counterPosition.Length; i++)
            {
                if (counterPosition[i].childCount == 0 && interactScript.isInteracting && inventoryManager.Items.Count > 0)
                    //check if player is interacting, has item, and counter position is empty
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
