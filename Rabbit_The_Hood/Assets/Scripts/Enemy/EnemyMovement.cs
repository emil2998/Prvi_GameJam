
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    private Animator animator;

    public Transform playerTarget;
    [SerializeField] private float movementSpeed = 1f;

    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float resumeDelay = 1f;

    private Rigidbody enemyRigidbody;
    private float distanceToPlayer;
    private float resumeTimer = 0f;

    private enum State { Moving, Waiting, Idle }
    private State currentState = State.Moving;

    [SerializeField] private PlayerHP playerHP;

    public bool isDead = false;

   

    //patrol
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    public bool playerSpotted = false;
    private Transform currentPatrolTarget;
    private float patrolReachThreshold = 0.2f;

    private void Start()
    {
        playerSpotted = false;
        currentPatrolTarget = pointA;

    }
    private void Awake()
    {
  
        playerHP = FindAnyObjectByType<PlayerHP>();
        animator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }
    private void Patrol()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentPatrolTarget.position);

        // Face patrol point
        transform.LookAt(currentPatrolTarget.position);

        // Move toward patrol point
        Vector3 direction = (currentPatrolTarget.position - transform.position).normalized;
        Vector3 move = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
        enemyRigidbody.MovePosition(move);

        // Play walking animation
        animator.SetBool("Walking", true);
        animator.SetBool("Attacking", false);

        // Switch target if close enough
        if (distanceToTarget < patrolReachThreshold)
        {
            currentPatrolTarget = currentPatrolTarget == pointA ? pointB : pointA;
        }
    }

    private void Update()
    {


        if (isDead) { return; }
       
        if (playerSpotted)
        {

            distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

            switch (currentState)
            {
                case State.Moving:
                    transform.LookAt(playerTarget.position);

                    if (distanceToPlayer <= stopDistance)
                    {
                        currentState = State.Idle;
                    }
                    break;

                case State.Idle:
                    if (distanceToPlayer > stopDistance)
                    {
                        currentState = State.Waiting;
                        resumeTimer = resumeDelay;
                    }
                    break;

                case State.Waiting:
                    resumeTimer -= Time.deltaTime;
                    if (resumeTimer <= 0f)
                    {
                        currentState = State.Moving;
                    }
                    break;
            }
        }                 
    }


    private void FixedUpdate()
    {

        if (isDead) { return; }
        if (!playerSpotted)
        {
            Patrol();
            return;
        }
        if (currentState == State.Moving)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
            enemyRigidbody.MovePosition(newPosition);
        }

        if (currentState == State.Idle)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);


        }
        if (currentState == State.Waiting)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);
        }
    }
}

