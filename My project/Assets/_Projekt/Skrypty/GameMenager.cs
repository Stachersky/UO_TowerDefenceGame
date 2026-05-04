using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Czas Gry")]
    public float timeElapsed = 0f; // Czas liczony w górê
    public bool isGameOver = false;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver) return;

        // Dodajemy czas
        timeElapsed += Time.deltaTime;
    }

    public void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("ZWYCIÊSTWO! Ukoñczy³eœ grê w czasie: " + timeElapsed + " sekund!");
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("PRZEGRANA! Baza zosta³a zniszczona.");
        Time.timeScale = 0f;
    }
}