using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public int rewardGold = 10;

    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damageAmount;

        Debug.Log(gameObject.name + " dosta³ obra¿enia! Zosta³o HP: " + currentHealth);

        if (spriteRenderer != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercentage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        Debug.Log("Przeciwnik pokonany!");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEnemyDeath();
        }

        if (PlayerCurrency.Instance != null)
        {
            PlayerCurrency.Instance.AddGold(rewardGold);
        }
        else
        {
            Debug.LogWarning("Brak obiektu PlayerCurrency na scenie!");
        }

        Destroy(gameObject);
    }
}