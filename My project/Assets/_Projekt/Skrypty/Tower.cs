using UnityEngine;
using System.Collections.Generic; // Potrzebne do obs³ugi Listy

public class Tower : MonoBehaviour
{
    [Header("Ustawienia wie¿y")]
    public float rotationSpeed = 5f;
    public float fireRate = 1f; // Strza³ co X sekund (przygotowanie pod KAN-28)

    [Header("Celowanie")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget;

    void Update()
    {
        UpdateTarget();
        RotateTowardsTarget();
    }

    // Funkcja wybieraj¹ca cel z listy wrogów w zasiêgu
    void UpdateTarget()
    {
        // Jeœli aktualny cel zgin¹³ lub uciek³, czyœcimy go
        if (currentTarget != null && !enemiesInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        // Jeœli nie mamy celu, a ktoœ jest w zasiêgu - bierzemy pierwszego z brzegu
        if (currentTarget == null && enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange[0];
        }
    }

    // Obracanie wie¿y (trójk¹ta) w stronê przeciwnika
    void RotateTowardsTarget()
    {
        if (currentTarget == null) return;

        // Obliczanie kierunku
        Vector3 direction = currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Obrót (odejmujemy 90 stopni, jeœli trójk¹t domyœlnie patrzy w górê)
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Wykrywanie wchodzenia w zasiêg (Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    // Wykrywanie wychodzenia z zasiêgu
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}