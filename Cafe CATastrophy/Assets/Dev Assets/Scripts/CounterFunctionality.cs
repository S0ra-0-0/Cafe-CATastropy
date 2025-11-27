using System.Collections;
using UnityEngine;

public class CounterFunctionality : MonoBehaviour
{
    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;
    public float decayTimer = 5f;
    public bool inRange;
    public InventoryItems itemOnCounter;
    public Transform[] counterPosition;
    private float cooldownTimer = 0;

    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
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
            if (itemOnCounter == null) // Only do this if counter is empty
            {
                for (int i = 0; i < counterPosition.Length; i++)
                {
                    if (cooldownTimer <= 0 && interactScript.isInteracting && inventoryManager.Items.Count > 0)
                    {
                        itemOnCounter = inventoryManager.Items[0];
                        Instantiate(
                         itemOnCounter.itemPrefab,
                         counterPosition[i].position + new Vector3(0, 0.58f, 0), 
                         itemOnCounter.itemPrefab.transform.rotation,
                        this.transform
                         );

                        inventoryManager.ClearInventory();
                        StartCoroutine(StartTimer());
                        cooldownTimer = 1.5f;
                        break;
                    }
                }
            }
            if (cooldownTimer <= 0 && itemOnCounter != null && interactScript.isInteracting && inventoryManager.Items.Count == 0)
            {
                inventoryManager.AddItem(itemOnCounter);
                Destroy(itemOnCounter.itemPrefab.gameObject);
                itemOnCounter = null;
                cooldownTimer = 1.5f;
            }
        }
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(decayTimer);
        Debug.LogWarning("Timer over");
        HitFoodCat[] allCats = FindObjectsByType<HitFoodCat>(FindObjectsSortMode.None);
        foreach (HitFoodCat cat in allCats)
        {
            if (cat.state == CatBase.CatState.Wander)
            {
                cat.SetTarget(counterPosition[0], gameObject);
                yield break;
            }
        }
        Debug.LogWarning("No wandering cat found!");
        decayTimer = 5f;
    }

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
