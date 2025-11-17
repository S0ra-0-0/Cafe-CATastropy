using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CatState { Idle, Moving, Detecting, Attacking, Eating, Fleeing }

public abstract class CatBaseClass : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Animator animator;
    protected CatState currentState;
    protected Vector3 targetLocation;
    protected float moveRadius = 5f;
    protected float detectRadius = 3f;
    protected Transform player;
    protected Transform targetJumpPoint;


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentState = CatState.Idle;
        FindRandomLocation();
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case CatState.Moving:
                MoveToTarget();
                break;
            case CatState.Detecting:
                Detect();
                break;
            case CatState.Fleeing:
                Flee();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPoint"))
        {
            if (other.name == "JumpPointPlayer1" && Random.Range(0, 2) == 0)
            {
                Debug.Log("Jumped at player 1");
                StartCoroutine(JumpOffConveyorBelt());
            }
            else if (other.name == "JumpPointPlayer2")
            {
                Debug.Log("Jumped at player 2");
                StartCoroutine(JumpOffConveyorBelt());
            }
        }
    }

    private IEnumerator JumpOffConveyorBelt()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z - 2f);
        rb.isKinematic = true;
        agent.enabled = true;
        currentState = CatState.Moving;
        FindRandomLocation();
    }

    protected void FindRandomLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1) && currentState == CatState.Moving)
        {
            targetLocation = hit.position;
            agent.SetDestination(targetLocation);
            currentState = CatState.Moving;
        }
    }

    protected virtual void MoveToTarget()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            FindRandomLocation();
        }
    }

    private void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                InventoryManager inventoryManager = player.GetComponent<InventoryManager>();
                if (inventoryManager != null)
                {
                    inventoryManager.Items = inventoryManager.Items ?? new List<InventoryItems>();

                    bool hasSpecificItem = inventoryManager.Items.Exists(item => item.itemName == "YourItemNameHere");

                    if (hasSpecificItem)
                    {
                        currentState = CatState.Fleeing;
                        FindClosestJumpPoint();

                    }
                }
            }
        }
    }

    public void FindClosestJumpPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        float closestDistance = Mathf.Infinity;
        Transform closestJumpPoint = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("JumpPoint"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestJumpPoint = hitCollider.transform;
                }
            }
        }

        if (closestJumpPoint != null)
        {
            targetJumpPoint = closestJumpPoint;
            agent.enabled = false;
            rb.isKinematic= false;

            transform.position = targetJumpPoint.position;
        }
    }


    protected virtual void Detect() { CheckForPlayer(); }

    protected virtual void Flee()
    {
      FindClosestJumpPoint();
    }
    public abstract void Action();
}
