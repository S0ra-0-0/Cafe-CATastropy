using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGD1 : MonoBehaviour
{
    private Vector3 m_Movement;

    public PlayerInput PlayerInputObj;

    public Material[] Colors;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = Colors[PlayerInputObj.playerIndex];
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_Movement.x = context.ReadValue<Vector2>().x;
        m_Movement.z = context.ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Movement * Time.deltaTime * 10);
    }
}
