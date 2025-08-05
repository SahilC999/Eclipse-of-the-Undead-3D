using UnityEngine;

/// <summary>
/// Handles zombie health, damage, and death.
/// Attach this to all zombie prefabs.
/// </summary>
public class ZombieHealth : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("References")]
    public Animator animator;
    public AudioSource hurtSound;
    public AudioSource deathSound;
    public ParticleSystem bloodEffect;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (hurtSound) hurtSound.Play();
        if (bloodEffect) bloodEffect.Play();

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            animator?.SetTrigger("Hurt");
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log(name + " was killed.");
        animator?.SetTrigger("Die");
        if (deathSound) deathSound.Play();

        // Add score
        GameManager.instance.AddScore(10);

        // Notify wave spawner
        WaveSpawner.ZombieKilled();

        // Destroy after animation
        Destroy(gameObject, 3f);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
