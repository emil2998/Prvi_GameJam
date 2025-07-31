using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody rb;
    [SerializeField] private float bulletDamage = 50f;

    private Vector3 gravityDirection = Vector3.down;
    private float gravityStrength = 9.81f;
    private bool fired = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().isTrigger = true;
        rb.useGravity = false;
    }
    private void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        fired = true;
        Destroy(gameObject, 5f);
    }
    
    void FixedUpdate()
    {

        if (fired)
        {
            Vector3 gravity = gravityDirection.normalized * gravityStrength;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {/*
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(bulletDamage);
            Destroy(gameObject);
        }
        */
        Destroy(gameObject);
    }
}
