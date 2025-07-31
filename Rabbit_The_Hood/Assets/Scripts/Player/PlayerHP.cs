
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        // gameManager.UpdateHealthUI(currentHealth, maxHealth);
    }
    public void Damage(float damage)
    {
        currentHealth -= damage;
        // gameManager.UpdateHealthUI(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
        }
    }

    private void HealPlayerOnce(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        //gameManager.UpdateHealthUI(currentHealth, maxHealth);
    }
}

   
