using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGD1 : MonoBehaviour
{
    private Vector3 m_Movement;

    public PlayerInput PlayerInputObj;

    public Material[] Colors;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = Colors[PlayerInputObj.playerIndex];
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_Movement.z = context.ReadValue<Vector2>().x * -1;
        m_Movement.x = context.ReadValue<Vector2>().y;
    }

    void Update()
    {
        rb.linearVelocity = new Vector3(m_Movement.x * 10, rb.linearVelocity.y, m_Movement.z * 10);
    }
}
