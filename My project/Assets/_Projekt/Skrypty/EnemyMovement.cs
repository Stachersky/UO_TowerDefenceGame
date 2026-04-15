using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Statystyki (KAN-19)")]
    public float speed = 2f;

    [Header("Œcie¿ka (KAN-58)")]
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Update()
    {
        // Sprawdzamy, czy mamy jeszcze jakieœ punkty do przejœcia
        if (currentWaypointIndex < waypoints.Length)
        {
            // Poruszanie siê w stronê aktualnego punktu (magia matematyki 2D)
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // Sprawdzenie, czy jesteœmy bardzo blisko punktu (dotarliœmy)
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++; // IdŸ do nastêpnego punktu
            }
        }
        else
        {
            // Szukamy obiektu z tagiem Base
            GameObject gameBase = GameObject.FindWithTag("Base");

            if (gameBase != null)
            {
                BaseHealth baseHealth = gameBase.GetComponent<BaseHealth>();
                if (baseHealth != null)
                {
                    baseHealth.TakeDamage(10f); // Zadaje 10 obra¿eñ (zadanie KAN-21)
                }
            }

            Destroy(gameObject); // Przeciwnik znika po ataku
        }
    }

}
