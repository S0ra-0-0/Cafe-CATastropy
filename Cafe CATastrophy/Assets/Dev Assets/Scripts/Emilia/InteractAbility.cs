using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public interface IInteractable
{
    void Interact(InteractAbility interactor);
}

public class InteractAbility : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactRadius = 1.5f;
    [SerializeField] private Transform interactOrigin;   // optional; defaults to this transform
    [SerializeField] private float interactCooldown = 0.3f;

    [Header("References")]
    public PlayerInput playerInputObj;
    public InventoryManager inventoryManager;

    public bool isInteracting { get; private set; }

    private readonly Collider[] hitBuffer = new Collider[8];

    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        playerInputObj = GetComponent<PlayerInput>();

        if (interactOrigin == null)
            interactOrigin = transform;
    }

    // Hook this up to the Interact action (once!) in PlayerInput
    public void DoInteract(InputAction.CallbackContext context)
    {
        // Only act on the performed phase, and ignore presses during cooldown
        if (!context.performed || isInteracting) return;

        StartCoroutine(InteractRoutine());
    }

    private IEnumerator InteractRoutine()
    {
        isInteracting = true;

        TryInteract();

        yield return new WaitForSeconds(interactCooldown);
        isInteracting = false;
    }

    private void TryInteract()
    {
        int count = Physics.OverlapSphereNonAlloc(
            interactOrigin.position,
            interactRadius,
            hitBuffer,
            interactableLayer);

        IInteractable closest = null;
        float closestDistSqr = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (!hitBuffer[i].TryGetComponent(out IInteractable interactable))
                continue;

            float distSqr = (hitBuffer[i].transform.position - interactOrigin.position).sqrMagnitude;
            if (distSqr < closestDistSqr)
            {
                closestDistSqr = distSqr;
                closest = interactable;
            }
        }

        if (closest != null)
        {
            closest.Interact(this);
        }
    }

    private void OnDisable()
    {
        // Don't get stuck in cooldown if the object is disabled mid-coroutine
        isInteracting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Transform origin = interactOrigin != null ? interactOrigin : transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin.position, interactRadius);
    }
}