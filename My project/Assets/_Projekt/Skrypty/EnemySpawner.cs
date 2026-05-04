using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ustawienia Spawnera")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] waypoints;

    [Header("System Fal")]
    public int baseEnemies = 3;              // Ile wrogów w 1. fali
    public int enemiesMultiplier = 2;        // O ile wrogów powiêksza siê ka¿da kolejna fala (Skalowanie)
    public float timeBetweenEnemies = 1f;    // Czas miêdzy wychodzeniem wrogów w trakcie jednej fali

    [Header("Timer Fal")]
    public float timeBetweenWaves = 5f;      // Czas na przygotowanie siê przed kolejn¹ fal¹
    public float countdown = 3f;            // Odliczanie do pierwszej fali (np. 3 sekundy na start)

    public int waveIndex = 1;               // Aktualny numer fali
    public bool isSpawning = false;         // Zabezpieczenie, ¿eby nie odpaliæ dwóch fal naraz

    void Update()
    {
        // Jeœli aktualnie trwa wypluwanie przeciwników, zatrzymaj timer
        if (isSpawning) return;

        // Jeœli odliczanie dobieg³o koñca, startujemy falê
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
        }
        else
        {
            // Odejmujemy czas (Time.deltaTime to u³amek sekundy od ostatniej klatki)
            countdown -= Time.deltaTime;
        }
    }

    // To jest Korutyna - potrafi "czekaæ"
    IEnumerator SpawnWave()
    {
        isSpawning = true;
        Debug.Log("Rozpoczyna siê fala: " + waveIndex);

        // KAN-41: Skalowanie liczby przeciwników
        // Wzór: bazowa iloœæ + (numer fali - 1) * mno¿nik
        // Np. Fala 1 = 3 wrogów, Fala 2 = 5 wrogów, Fala 3 = 7 wrogów
        int enemiesToSpawn = baseEnemies + ((waveIndex - 1) * enemiesMultiplier);

        // KAN-38: System fal (pêtla tworz¹ca odpowiedni¹ liczbê wrogów)
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();

            // Magia Korutyny: czekamy np. 1 sekundê przed kolejnym obrotem pêtli
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        // Gdy fala siê skoñczy:
        waveIndex++;                     // Zwiêkszamy numer fali na nastêpny raz
        countdown = timeBetweenWaves;    // Resetujemy timer do nastêpnej fali
        isSpawning = false;              // Pozwalamy timerowi znowu tykaæ w funkcji Update
    }

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyMovement movementScript = newEnemy.GetComponent<EnemyMovement>();

        if (movementScript != null)
        {
            movementScript.waypoints = this.waypoints;
        }
    }
}