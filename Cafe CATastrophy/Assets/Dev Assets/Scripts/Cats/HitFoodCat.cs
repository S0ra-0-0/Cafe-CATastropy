using UnityEngine;
using System.Collections;

public class HitFoodCat : CatBase
{
    private Transform itemTarget;
    private GameObject targetObject;

    protected override bool ShouldFlee()
    {
        return false;
    }

    protected override bool PerformAction()
    {
        return false;
    }

    public void SetTarget(Transform targetTransform, GameObject targetItem)
    {
        itemTarget = targetTransform;
        targetObject = targetItem;
        state = CatState.Action;

    }

    protected override void ActionUpdate()
    {
        if (state == CatState.Action)
        {
            agent.SetDestination(itemTarget.position);
            float distance = Vector3.Distance(transform.position, itemTarget.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(itemTarget.position - transform.position),
                Time.deltaTime * 5f
            );
            if (distance < 3f)
            {
                Debug.LogWarning("Destroying all child objects!");

                foreach (Transform child in targetObject.transform)
                {
                    Destroy(child.gameObject);
                }

                CounterFunctionality counter = targetObject.GetComponent<CounterFunctionality>();
                if (counter != null)
                {
                    counter.itemOnCounter = null;
                }

                targetObject = null;
                StartCoroutine(StealAnimation());
            }
        }
    }


    private IEnumerator StealAnimation()
    {

        agent.enabled = false;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;

        animator.SetTrigger("Steal");

        yield return new WaitForSeconds(.8f);
        EnterState(CatState.Flee);

    }



}
