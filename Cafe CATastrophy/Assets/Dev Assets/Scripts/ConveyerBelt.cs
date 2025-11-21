using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector3 direction = Vector3.forward;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= 1f)
        {
            Rigidbody rb = collision.collider.attachedRigidbody;
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
        }
    }
}
