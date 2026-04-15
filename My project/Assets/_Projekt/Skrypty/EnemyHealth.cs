using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Statystyki (KAN-19)")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        // Na start przeciwnik ma pe³ne zdrowie
        currentHealth = maxHealth;
    }

    // Tê funkcjê bêdzie wywo³ywaæ wie¿yczka, gdy strzeli w przeciwnika
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Wyœwietlamy w konsoli, ¿eby widzieæ, ¿e dzia³a (do testów)
        Debug.Log(gameObject.name + " dosta³ obra¿enia! Zosta³o HP: " + currentHealth);

        // Sprawdzamy, czy HP spad³o do zera
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Zadanie KAN-22: Usuwanie przeciwnika po œmierci
        Debug.Log("Przeciwnik pokonany!");

        // TODO dla zadania KAN-36: Tutaj w przysz³oœci dodamy dodawanie z³ota graczowi

        Destroy(gameObject); // Usuwa ca³kowicie obiekt przeciwnika ze sceny
    }
}