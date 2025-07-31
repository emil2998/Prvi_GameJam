
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
     private Animator animator;

    public Transform playerTarget;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float resumeDelay = 1f;

    private Rigidbody enemyRigidbody;
    private float distanceToPlayer;
    private float resumeTimer = 0f;

    private enum State { Moving, Waiting, Idle }
    private State currentState = State.Moving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
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


    private void FixedUpdate()
    {
        if (currentState == State.Moving)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
            enemyRigidbody.MovePosition(newPosition);
        }  

        if(currentState == State.Idle)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
        }
        if(currentState == State.Waiting)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHP player))
        {
            player.Damage(damage);
        }
    }
}

