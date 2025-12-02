using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGD1 : MonoBehaviour
{
    private Vector3 m_Movement;
    public PlayerInput PlayerInputObj;
    public Material[] Colors;
    private Rigidbody rb;
    private bool isStunned = false;
    [SerializeField] private Animator animator;
    [SerializeField] private float walkThreshold = 0.1f; // Minimum input magnitude to trigger walking
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Transform itemHoldPosistion;
    private GameObject heldItem;

    public Image invImage;
    public InventoryManager inventoryManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventoryManager = GetComponent<InventoryManager>();

        if (PlayerInputObj.playerIndex < Colors.Length)
        {
            GetComponentInChildren<MeshRenderer>().material = Colors[PlayerInputObj.playerIndex];
        }
        else
        {
            Debug.LogError("Player index out of range for Colors array!");
        }
    }


    void Update()
    {
        //code for player inventory in UI
        


        if (!isStunned)
        {
            Vector2 input = PlayerInputObj.actions["Move"].ReadValue<Vector2>();
            m_Movement.z = input.x * -1;
            m_Movement.x = input.y;

            rb.linearVelocity = new Vector3(m_Movement.x * 10, rb.linearVelocity.y, m_Movement.z * 10);

            bool isWalking = input.magnitude > walkThreshold;
            animator.SetBool("IsWalking", isWalking);
            animator.SetBool("IsStunned", false);

            if (isWalking)
            {
                float angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, angle - 90, 0);
            }


        }
        else
        {
            animator.SetBool("IsStunned", true);
            animator.SetBool("IsWalking", false);
        }

        if (inventory.Items != null && inventory.Items.Count > 0)
        {
            if (inventory.Items[0].itemPrefab != null && heldItem == null)
            {
                animator.SetBool("IsHoldingItem", true);
                Debug.Log("Spawning held item");
                heldItem = Instantiate(
                 inventory.Items[0].itemPrefab,
                 itemHoldPosistion.position, 
                 inventory.Items[0].itemPrefab.transform.rotation,  
                 itemHoldPosistion          
                  );
            }
            else if (inventory.Items[0].itemPrefab == null && heldItem != null)
            {
                Destroy(heldItem);
                heldItem = null;
                animator.SetBool("IsHoldingItem", false);
            }
        }
        else
        {
            if (heldItem != null)
            {
                Destroy(heldItem);
                heldItem = null;
            }
            animator.SetBool("IsHoldingItem", false);
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
