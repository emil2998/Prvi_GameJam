using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform patrolPointA;
    [SerializeField] private Transform patrolPointB;
    private Rigidbody rb;
    [SerializeField] private float movementSpeed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Patrol();
    }
    private void Patrol()
    {/*
        animator.SetBool("Walking", true);
        animator.SetBool("Attacking", false);
        Vector3 direction = (patrolPointA.position - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);*/
    }

}
