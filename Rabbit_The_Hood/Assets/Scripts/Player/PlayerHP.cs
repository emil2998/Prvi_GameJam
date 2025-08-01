
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    [SerializeField] private Image healthImage;

    [SerializeField] private UIManager uIManager;

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
            uIManager.GameLost();
            Time.timeScale = 0f;

        }
    }

    private void HealPlayer(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void UpdateHealthUI()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }
}


