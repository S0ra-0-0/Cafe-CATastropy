using UnityEngine;

public class OrderRegister : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrderItem"))
        {
            var itemInstance = other.GetComponent<ItemInstance>();
            if (itemInstance != null && itemInstance.itemData != null)
            {
                OrderManager.Instance.RegisterItem(itemInstance.itemData);
            }
            else
            {
                Debug.LogWarning("OrderRegister: collided object has no ItemInstance or itemData.", other.gameObject);
            }

            Destroy(other.gameObject);
        }

    }
}
