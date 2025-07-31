using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private Image healthImage;

    private EnemyMovement enemyMovement;

    private void Awake()
    {
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

        }
    }

    private void UpdateHealthUI()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}
