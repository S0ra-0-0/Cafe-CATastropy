using UnityEngine;

public abstract class CatTesting : MonoBehaviour
{

    [Header("Movement Settings")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 4f;
    public float acceleration = 6f;
    public float turnSpeed = 5f;

    [Header("Wander Settings")]
    public float wanderRadius = 5f;
    public float wanderPauseTime = 2f;

    [Header("Jump Settings")]
    public float jumpHeight = 1f;
    public float jumpDuration = 0.5f;

    public int droppedByThisPlayer;

    protected UnityEngine.AI.NavMeshAgent agent;

    protected enum CatState
    {
        OnConveyor,
        JumpOff,
        Wander,
        Flee,
        Action,
        JumpOn,
        Despawn
    }

    protected CatState state = CatState.OnConveyor;

    protected Rigidbody rb;
    protected Vector3 velocity;

    protected float conveyorTimer = 0f;

    protected Vector3 wanderTarget;
    protected float wanderPauseTimer = 0f;

    protected Vector3 jumpStart;
    protected Vector3 jumpEnd;
    protected float jumpTimer = 0f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.speed = walkSpeed;
    }


    protected virtual void Update()
    {
        StateMachineTick();
    }

    protected virtual void FixedUpdate()
    {
        MovementTick();
    }


    protected virtual void StateMachineTick()
    {
        switch (state)
        {
            case CatState.OnConveyor:
                OnConveyorUpdate();
                break;

            case CatState.JumpOff:
                JumpOffUpdate();
                break;

            case CatState.Wander:
                WanderUpdate();
                break;

            case CatState.Flee:
                FleeUpdate();
                break;

                case CatState.Action:
             ActionUpdate();
                break;

            case CatState.JumpOn:
                JumpOnUpdate();
                break;

            case CatState.Despawn:
                DespawnUpdate();
                break;
        }
    }

    protected void EnterState(CatState newState)
    {
        state = newState;

        switch (newState)
        {
            case CatState.OnConveyor: OnEnterConveyor(); break;
            case CatState.JumpOff: OnEnterJumpOff(); break;
            case CatState.Wander: OnEnterWander(); break;
            case CatState.Flee: OnEnterFlee(); break;
            case CatState.Action: OnEnterAction(); break;
            case CatState.JumpOn: OnEnterJumpOn(); break;
            case CatState.Despawn: OnEnterDespawn(); break;
        }
    }


    protected virtual void OnEnterConveyor()
    {
        EnableRBMovement();

        conveyorTimer = 0f;
        velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPoint") && state == CatState.OnConveyor)
        {
            if (other.name == "JumpPointPlayer1" && Random.Range(0, 2) == 0)
            {
                Debug.Log("Jumped at player 1");
                droppedByThisPlayer = 1;
                EnterState(CatState.JumpOff);

            }
            else if (other.name == "JumpPointPlayer2" && state == CatState.OnConveyor)
            {
                Debug.Log("Jumped at player 2");
                droppedByThisPlayer = 2;
                EnterState(CatState.JumpOff);

            }
        }
    }
    protected virtual void OnConveyorUpdate()
    {
       
    
    }

    protected virtual void OnEnterJumpOff()
    {
        EnableRBMovement();

        jumpTimer = 0f;
        jumpStart = transform.position;
        jumpEnd = transform.position + transform.right * -5f;
    }


    protected virtual void JumpOffUpdate()
    {
        jumpTimer += Time.deltaTime;

        float t = jumpTimer / jumpDuration;

        if (t >= 1f)
        {
            EnterState(CatState.Wander);
            return;
        }

        Vector3 pos = Vector3.Lerp(jumpStart, jumpEnd, t);
        pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

        rb.MovePosition(pos);
    }

    protected virtual void OnEnterWander()
    {
        EnableAgentMovement();

        PickNewWanderTarget();
        agent.speed = walkSpeed;
        agent.SetDestination(wanderTarget);

        wanderPauseTimer = wanderPauseTime;
    }



    protected virtual void WanderUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            wanderPauseTimer -= Time.deltaTime;

            if (wanderPauseTimer <= 0f)
            {
                wanderPauseTimer = wanderPauseTime;
                PickNewWanderTarget();
                agent.SetDestination(wanderTarget);
            }
        }
        else
        {
            Vector3 next = agent.nextPosition;
            MoveTowards(next, walkSpeed);
        }

        if (ShouldFlee())
        {
            EnterState(CatState.Flee);
        }

        if (PerformAction())
        {
            EnterState(CatState.Action);
        }
    }


    protected virtual void OnEnterFlee()
    {
        EnableRBMovement();
    }

    protected virtual void FleeUpdate()
    {
        Vector3 target = GetFleeTarget();

        MoveTowards(target, runSpeed);

        if (Vector3.Distance(transform.position, target) < 0.4f)
        {
            EnterState(CatState.JumpOn);
        }
    }

    protected virtual void OnEnterAction()
    {
        EnableRBMovement();
    }
    protected virtual void ActionUpdate()
    {
        // Placeholder for action logic to be ovverridden by child classes to hit food or player etc.
        EnterState(CatState.Wander);
    }
    protected virtual void OnEnterJumpOn()
    {
        EnableRBMovement();

        jumpTimer = 0f;
        jumpStart = transform.position;
        jumpEnd = GetConveyorPoint();
    }


    protected virtual void JumpOnUpdate()
    {
        jumpTimer += Time.deltaTime;

        float t = jumpTimer / jumpDuration;

        if (t >= 1f)
        {
            velocity = Vector3.zero;

            return;
        }

        Vector3 pos = Vector3.Lerp(jumpStart, jumpEnd, t);
        pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

        rb.MovePosition(pos);
    }

    protected virtual void OnEnterDespawn()
    {
        gameObject.SetActive(false);
    }

    protected virtual void DespawnUpdate() { }

    protected void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        Vector3 desiredVel = dir * speed;

        velocity = Vector3.MoveTowards(velocity, desiredVel, acceleration * Time.deltaTime);

        // smooth rotation
        if (velocity.sqrMagnitude > 0.01f)
        {
            Vector3 flatVel = new Vector3(velocity.x, 0f, velocity.z);

            if (flatVel.sqrMagnitude > 0.01f)
            {
                Quaternion look = Quaternion.LookRotation(flatVel);
                transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);
            }

        }
    }

    protected virtual void MovementTick()
    {
        if (state == CatState.JumpOff || state == CatState.JumpOn)
            return;

        if (agent.enabled)
        {

            transform.position = agent.nextPosition;
            return;
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    protected void PickNewWanderTarget()
    {
        Vector3 randomPoint = transform.position + new Vector3(
            Random.Range(-wanderRadius, wanderRadius),
            0,
            Random.Range(-wanderRadius, wanderRadius)
        );

        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            wanderTarget = hit.position;
        }
        else
        {
            wanderTarget = transform.position;
        }
    }

    protected void EnableAgentMovement()
    {
        agent.enabled = true;
        rb.isKinematic = true;
    }

    protected void EnableRBMovement()
    {
        agent.enabled = false;
        rb.isKinematic = false;
    }


    // Child must tell where the conveyor is
    protected abstract Vector3 GetConveyorPoint();

    // Child must give flee target
    protected abstract Vector3 GetFleeTarget();

    // Child can decide when to flee
    protected abstract bool ShouldFlee();

    protected abstract bool PerformAction();
}
