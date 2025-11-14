using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractAbility : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    public PlayerInput playerInputObj;
    private Transform playerTransform;

    private void Awake()
    {
        playerInputObj = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        playerInputObj.actions["Interact"].performed += DoInteract;
    }

    private void OnDisable()
    {
        playerInputObj.actions["Interact"].performed -= DoInteract;
    }
    private void DoInteract(InputAction.CallbackContext context)
    {
        Debug.Log("interacted");
        if (!Physics.Raycast(playerTransform.position + (Vector3.up * 0.3f) + (playerTransform.forward * 0.2f), playerTransform.forward, out RaycastHit hitInfo, 2f, interactableLayer))
        {
            return;
            //if nothing is hit, don't use the raycast
        }
    }
}
