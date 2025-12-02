using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 direction = Vector3.forward;

    public InventoryItems inventoryItem;
    public InventoryManager inventoryManager;
    public InteractAbility interactScript;

    public bool inRange;
    public InventoryItems itemOnCounter;
    public Transform[] counterPosition;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= .9f)
        {
            Rigidbody rb = collision.collider.attachedRigidbody;
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
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
            for (int i = 0; i < counterPosition.Length; i++)
            {
                if (interactScript.isInteracting && inventoryManager.Items.Count > 0)
                {
                    itemOnCounter = inventoryManager.Items[0];
                    GameObject order = Instantiate(
                     itemOnCounter.itemPrefab,
                     counterPosition[i].position + new Vector3(0, 0.58f, 0),
                     itemOnCounter.itemPrefab.transform.rotation
                     );
                    order.gameObject.tag = "OrderItem";
                    order.layer = LayerMask.NameToLayer("OrderItem");
                    order.AddComponent<Rigidbody>();
                    order.AddComponent<BoxCollider>();
                    var instance = order.AddComponent<ItemInstance>();
                    instance.Initialize(itemOnCounter);


                    inventoryManager.ClearInventory();
                    itemOnCounter = null;
                    break;
                }
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
