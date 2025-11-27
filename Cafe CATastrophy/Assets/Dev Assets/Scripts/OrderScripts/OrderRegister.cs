using UnityEngine;

public class OrderRegister : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrderItem"))
        {
           OrderManager.Instance.RegisterItem(other.GetComponent<InventoryItems>());

            Destroy(other.gameObject);
        }

    }
}
