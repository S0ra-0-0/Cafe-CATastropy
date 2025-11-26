using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerGD1 : MonoBehaviour
{
    private Vector3 m_Movement;
    public PlayerInput PlayerInputObj;
    public Material[] Colors;
    private Rigidbody rb;
    private bool isStunned = false;

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
        if (!isStunned)
        {
            rb.linearVelocity = new Vector3(m_Movement.x * 10, rb.linearVelocity.y, m_Movement.z * 10);
        }
    }

    public void StunPlayer(float stunDuration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(stunDuration));
        }
    }   

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}
