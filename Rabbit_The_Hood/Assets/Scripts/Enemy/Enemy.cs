using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private Image healthImage;

    private EnemyMovement enemyMovement;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    [SerializeField] private SphereCollider sphereCollider;

    [SerializeField] private GameObject canvas;

    [SerializeField] private ParticleSystem particleSleep;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            animator.SetBool("Dead", true);
            enemyMovement.isDead = true;

            rb.isKinematic = true;
            capsuleCollider.enabled = false;
            sphereCollider.enabled = false;
            canvas.SetActive(false);
            particleSleep.Play();
        }
    }

    private void UpdateHealthUI()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}
