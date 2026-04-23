using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public float maxHealth = 100f;
    private float currentHealth;

    
    public int rewardGold = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        Debug.Log(gameObject.name + " dostal obrazenia! Zostalo HP: " + currentHealth);

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