using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Statystyki wie¿y")]
    public float fireRate = 1f;
    public float damage = 25f;

    [Header("Efekt zamra¿ania (Tylko wie¿a Medium)")]
    [Range(0f, 1f)]
    public float slowPercentage = 0f; // 0 = brak, 0.5 = spowolnienie o 50%
    public float slowDuration = 0f;   // Jak d³ugo trwa efekt w sekundach

    [Header("Pocisk")]
    public GameObject projectilePrefab;

    private float fireCountdown = 0f;

    [Header("System celowania")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget;

    void Update()
    {
        UpdateTarget();

        if (currentTarget != null)
        {
            fireCountdown -= Time.deltaTime;

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = bulletGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            // ZMIANA: Przekazujemy pociskowi równie¿ dane o spowolnieniu
            projectile.Seek(currentTarget.transform, damage, slowPercentage, slowDuration);
        }
    }

    void UpdateTarget()
    {
        enemiesInRange.RemoveAll(item => item == null);

        if (currentTarget != null && !enemiesInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        if (currentTarget == null && enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange[0];
        }
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