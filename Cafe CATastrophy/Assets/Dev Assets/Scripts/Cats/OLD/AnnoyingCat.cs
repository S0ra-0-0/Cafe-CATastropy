using System.Collections.Generic;
using UnityEngine;

public class AnnoyingCat : CatBaseClass
{
    private float sitTimer;
    private bool isSitting;
    private bool playerHasBroom;
    public bool testFlee = false;

    protected override void Start()
    {
        base.Start();
        sitTimer = Random.Range(1f, 5f);
    }

    protected override void Update()
    {
        base.Update();
        if (currentState == CatState.Moving)
        { 
            //SitRandomly(); // Uncomment if you want the cat to sit randomly
        }

        if (testFlee)
        {
            currentState = CatState.Fleeing;
            testFlee = false;
        }
    }

    private void SitRandomly()
    {
        sitTimer -= Time.deltaTime;
        if (sitTimer <= 0f)
        {
            isSitting = !isSitting;
            if (isSitting)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                FindRandomLocation();
            }
            sitTimer = Random.Range(1f, 5f);
        }
    }

    public override void Action() { }

    protected override void Flee()
    {
        base.Flee();
    }
}
