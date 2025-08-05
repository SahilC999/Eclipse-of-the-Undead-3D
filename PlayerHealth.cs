using UnityEngine;

/// <summary>
/// Handles player health, damage, and healing.
/// Attach to Player.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource hurtSound;
    public AudioSource deathSound;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (hurtSound) hurtSound.Play();
        UIManager.instance.UpdateHealth(currentHealth, maxHealth);
        UIManager.instance.ShowDamageEffect();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UIManager.instance.UpdateHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player Died!");
        if (deathSound) deathSound.Play();

        GameManager.instance.GameOver();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
