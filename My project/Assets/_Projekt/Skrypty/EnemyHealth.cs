using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public int rewardGold = 10;

    // Dodajemy tylko zmienn¹ do grafiki
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;

        // Na starcie pobieramy grafikê wroga, ¿eby móc j¹ kolorowaæ
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log(gameObject.name + " dostal obrazenia! Zostalo HP: " + currentHealth);

        // --- NASZ DODATEK: Zmiana koloru przy uderzeniu ---
        if (spriteRenderer != null)
        {
            // Liczymy procent zdrowia (np. 50 / 100 = 0.5)
            float healthPercentage = currentHealth / maxHealth;
            // Lerp miesza kolor czerwony (0% HP) z bia³ym (100% HP)
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, healthPercentage);
        }
        // --------------------------------------------------

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Przeciwnik pokonany!");

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