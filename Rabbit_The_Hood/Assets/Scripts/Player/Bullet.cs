using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    [SerializeField] private float bulletDamage = 50f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().isTrigger = true;
    }
    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, 5f);
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
