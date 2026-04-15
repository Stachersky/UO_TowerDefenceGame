using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ustawienia Spawnera (KAN-60)")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;

    [Header("Œcie¿ka dla przeciwników")]
    public Transform[] waypoints;     // <--- DODANE: Spawner trzyma listê punktów

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // 1. Tworzymy przeciwnika i zapisujemy go jako now¹ "zmienn¹"
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // 2. Pobieramy jego skrypt chodzenia
        EnemyMovement movementScript = newEnemy.GetComponent<EnemyMovement>();

        // 3. Przekazujemy mu nasze punkty œcie¿ki!
        if (movementScript != null)
        {
            movementScript.waypoints = this.waypoints;
        }
    }
}