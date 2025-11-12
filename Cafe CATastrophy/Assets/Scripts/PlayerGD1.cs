using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGD1 : MonoBehaviour
{
    private Vector3 m_Movement;

    public CinemachineBrain Brain;
    public CinemachineCamera Camera;
    public PlayerInput PlayerInputObj;

    public Material[] Colors;

    private void Start()
    {
        SetCameraSettings();
        GetComponent<MeshRenderer>().material = Colors[PlayerInputObj.playerIndex];
    }

    private void SetCameraSettings()
    {
        switch(PlayerInputObj.playerIndex)
        {
            case 0: // player 1
                Brain.ChannelMask = OutputChannels.Channel01;
                Camera.OutputChannel = OutputChannels.Channel01;
                break;
            case 1: // player 2
                Brain.ChannelMask = OutputChannels.Channel02;
                Camera.OutputChannel = OutputChannels.Channel02;
                break;
        }
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
