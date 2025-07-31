using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private Image healthImage;

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
            Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}
