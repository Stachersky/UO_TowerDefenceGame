using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [Header("Ustawienia wie¿y")]
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public float damage = 25f; 

    private float fireCountdown = 0f;

    [Header("Celowanie")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget;

    void Update()
    {
        UpdateTarget();
        RotateTowardsTarget();

        
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
        
        EnemyHealth enemyHealth = currentTarget.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    

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