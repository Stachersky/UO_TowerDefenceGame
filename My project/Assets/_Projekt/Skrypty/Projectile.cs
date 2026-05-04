using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    private float damage;

    // Zmienne do spowolnienia
    private float slowPct;
    private float slowDur;

    // ZMIANA: Funkcja Seek przyjmuje teraz 4 wartoœci
    public void Seek(Transform _target, float _damage, float _slowPct, float _slowDur)
    {
        target = _target;
        damage = _damage;
        slowPct = _slowPct;
        slowDur = _slowDur;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // 1. Zadawanie obra¿eñ
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        // 2. Nak³adanie spowolnienia (jeœli pocisk je posiada)
        if (slowPct > 0)
        {
            EnemyMovement enemyMovement = target.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.ApplySlow(slowPct, slowDur);
            }
        }

        Destroy(gameObject);
    }
}