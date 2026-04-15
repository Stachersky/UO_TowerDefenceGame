using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Ustawienia wie¿y")]
    public float rotationSpeed = 5f;
    public float fireRate = 1f; // Strza³ co 1 sekundê
    public float damage = 25f;  // Ile HP zabiera jeden strza³

    private float fireCountdown = 0f; // Wewnêtrzny stoper wie¿y

    [Header("Celowanie")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget;

    void Update()
    {
        UpdateTarget();
        RotateTowardsTarget();

        // Jeœli mamy cel, zaczynamy strzelaæ
        if (currentTarget != null)
        {
            // Odliczanie do nastêpnego strza³u
            fireCountdown -= Time.deltaTime;

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate; // Reset stopera (np. 1/1 = 1 sekunda)
            }
        }
    }

    void Shoot()
    {
        // Sprawdzamy, czy cel na pewno ma nasz skrypt zdrowia
        EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    // --- Poni¿ej zostaje to samo, co mieliœmy wczeœniej ---

    void UpdateTarget()
    {
        if (currentTarget != null && !enemiesInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        if (currentTarget == null && enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange[0];
        }
    }

    void RotateTowardsTarget()
    {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}