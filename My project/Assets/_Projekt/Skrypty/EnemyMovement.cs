using UnityEngine;
using System.Collections; // BARDZO WA¯NE: Dodano, aby móc u¿ywaæ IEnumerator (czasomierzy)

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private float originalSpeed; // Zapamiêtuje normaln¹ prêdkoœæ wroga

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private Coroutine slowCoroutine; // Przechowuje nasz aktywny stoper spowolnienia

    void Start()
    {
        // Zapisujemy prêdkoœæ bazow¹ na samym pocz¹tku
        originalSpeed = speed;
    }

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            GameObject gameBase = GameObject.FindWithTag("Base");

            if (gameBase != null)
            {
                BaseHealth baseHealth = gameBase.GetComponent<BaseHealth>();
                if (baseHealth != null)
                {
                    baseHealth.TakeDamage(10f);
                }
            }

            Destroy(gameObject);
        }
    }

    // --- NOWA FUNKCJA: Spowalnianie ---
    public void ApplySlow(float pct, float duration)
    {
        if (pct <= 0) return; // Jeœli wie¿a nie spowalnia, zignoruj

        // Obliczamy now¹ prêdkoœæ (np. 0.5 to obciêcie o 50%)
        speed = originalSpeed * (1f - pct);

        // Jeœli wróg by³ ju¿ spowolniony, resetujemy stoper
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(RemoveSlow(duration));
    }

    // Stoper, który czeka i przywraca prêdkoœæ
    private IEnumerator RemoveSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }
}