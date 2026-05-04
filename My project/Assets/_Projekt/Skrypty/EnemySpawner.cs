using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ustawienia Spawnera")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] waypoints;

    [Header("System Fal (KAN-38 i KAN-41)")]
    public int maxWaves = 40;                // NOWOŒÆ: Cel gry!
    public int baseEnemies = 3;
    public int enemiesMultiplier = 2;
    public float timeBetweenEnemies = 1f;

    [Header("Timer Fal (KAN-39)")]
    public float timeBetweenWaves = 5f;
    public float countdown = 3f;

    public int waveIndex = 1;
    public bool isSpawning = false;

    [Header("Skalowanie Przeciwników (KAN-42, KAN-43)")]
    public float hpBonusPer5Waves = 0.25f;
    public float speedBonusPer10Waves = 0.15f;

    void Update()
    {
        if (isSpawning) return;

        // Jeœli osi¹gnêliœmy limit fal i licznik zszed³ do 0 - sprawdŸ wygran¹
        if (waveIndex > maxWaves)
        {
            CheckForWin();
            return; // Przerwij dalsze wypluwanie fal
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        Debug.Log("Rozpoczyna siê fala: " + waveIndex + " z " + maxWaves);

        int enemiesToSpawn = baseEnemies + ((waveIndex - 1) * enemiesMultiplier);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        waveIndex++;
        countdown = timeBetweenWaves;
        isSpawning = false;
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyMovement movementScript = newEnemy.GetComponent<EnemyMovement>();
        if (movementScript != null)
        {
            movementScript.waypoints = this.waypoints;
            float speedMultiplier = 1f + ((waveIndex / 10) * speedBonusPer10Waves);
            movementScript.speed *= speedMultiplier;
        }

        EnemyHealth healthScript = newEnemy.GetComponent<EnemyHealth>();
        if (healthScript != null)
        {
            float hpMultiplier = 1f + ((waveIndex / 5) * hpBonusPer5Waves);
            healthScript.maxHealth *= hpMultiplier;
        }
    }

    void CheckForWin()
    {
        // Sprawdza, czy na mapie nie ma ju¿ ¿adnych wrogów z tagiem "Enemy"
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (remainingEnemies.Length == 0)
        {
            GameManager.Instance.WinGame();
        }
    }
}