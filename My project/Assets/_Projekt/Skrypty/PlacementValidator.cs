using UnityEngine;

public class PlacementValidator : MonoBehaviour
{
    private int obstaclesCount = 0;

    
    public bool CanPlace()
    {
        return obstaclesCount == 0;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jeœli dotkniemy wroga, œcie¿ki, lub innej wie¿y, blokujemy budowê
        if (collision.CompareTag("Path") || collision.CompareTag("Tower") || collision.CompareTag("Enemy"))
        {
            obstaclesCount++;
        }
    }

    // Gdy wyjdziemy ze strefy kolizji
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Path") || collision.CompareTag("Tower") || collision.CompareTag("Enemy"))
        {
            obstaclesCount--;
            // Zabezpieczenie przed spadkiem poni¿ej 0
            if (obstaclesCount < 0) obstaclesCount = 0;
        }
    }
}