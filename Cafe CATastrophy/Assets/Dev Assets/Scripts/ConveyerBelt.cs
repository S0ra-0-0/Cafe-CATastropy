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
    private float cooldownTimer = 0;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= 1f)
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

    public void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
