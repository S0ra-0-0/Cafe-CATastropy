using UnityEngine;

public class AnnoyingCat : CatBaseClass
{
    private float sitTimer;
    private bool isSitting;
    private Transform player;
    private bool playerHasBroom;

    protected override void Start()
    {
        base.Start();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        sitTimer = Random.Range(1f, 5f);
    }

    protected override void Update()
    {
        base.Update();
        if (currentState == CatState.Moving)
        {
            //CheckForBroom();
            SitRandomly();
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
        FindRandomLocation();
        currentState = CatState.Moving;
    }
}
