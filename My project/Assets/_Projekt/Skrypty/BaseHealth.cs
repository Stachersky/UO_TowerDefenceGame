using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Baza zosta³a zaatakowana! Pozosta³e HP: " + health);

        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("PRZEGRANA! Baza zniszczona.");
        // Tu w przysz³oœci (KAN-51) dodamy ekran koñca gry
        Time.timeScale = 0f; // Zatrzymuje czas w grze
    }
}