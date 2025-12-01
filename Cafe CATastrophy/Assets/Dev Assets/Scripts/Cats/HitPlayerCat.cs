using System.Collections;
using UnityEngine;

public class HitPlayerCat : CatBase
{
    [SerializeField] private float hitTimer = 0f;
    [SerializeField] private float actionRadius = 3f; // Distance to trigger hit
    [SerializeField] private float moveSpeed = 5f; // Speed when moving toward player
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float failSafeTimer = 10f;
    private Coroutine actionCoroutine;

    protected override bool ShouldFlee()
    {
        return false;
    }

    protected override bool PerformAction()
    {
        return false;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        hitTimer = Random.Range(10f, 20f);
        actionCoroutine = StartCoroutine(TriggerAction());
    }

    private IEnumerator TriggerAction()
    {
        yield return new WaitForSeconds(hitTimer);
        EnterState(CatState.Action);
    }

    protected override void ActionUpdate()
    {
        if (state == CatState.Action && playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);
            transform.rotation = Quaternion.Slerp(
              transform.rotation,
              Quaternion.LookRotation(playerTransform.position - transform.position),
              Time.deltaTime * 5f
          );

            float distance = Vector3.Distance(transform.position, playerTransform.position);
            StartCoroutine(failTimer());
            if (distance < actionRadius && failSafeTimer > 0)
            {
                if (actionCoroutine != null)
                    StopCoroutine(actionCoroutine);
                StartCoroutine(HitPlayer());
            }
        }
    }

    private IEnumerator failTimer()
    {
        yield return new WaitForSeconds(failSafeTimer);
        EnterState(CatState.Flee);
        failSafeTimer = 10f;
    }

    private IEnumerator HitPlayer()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(.3f);
        playerTransform.GetComponent<PlayerGD1>().StunPlayer(2.3f);
        yield return new WaitForSeconds(0.8f);

        EnterState(CatState.Flee);
    }

}