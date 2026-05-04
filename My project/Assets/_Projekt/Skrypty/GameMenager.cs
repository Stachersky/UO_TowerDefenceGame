using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float timeElapsed = 0f;
    public bool isGameOver = false;

    public GameObject endGamePanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI finalTimeText;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        timeElapsed += Time.deltaTime;
    }

    public void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        ShowEnd("WYGRANA");
    }

    public void LoseGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        ShowEnd("PRZEGRANA");
    }

    void ShowEnd(string text)
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        if (resultText != null)
            resultText.text = text;

        if (finalTimeText != null)
        {
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);

            finalTimeText.text = "Czas: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}