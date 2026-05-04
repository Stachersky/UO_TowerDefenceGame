using UnityEngine;
using UnityEngine.UI; // Potrzebne do obs³ugi paska (Slider)
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Elementy UI na ekranie")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timeText;

    [Header("Pasek Zdrowia Bazy")]
    public Slider baseHealthSlider;
    public BaseHealth baseHealthScript; // Podpinamy bazê, ¿eby znaæ jej HP

    [Header("Skrypty z logik¹ gry")]
    public EnemySpawner spawner;

    void Start()
    {
        // Na start ustawiamy maksymalne zdrowie paska (¿eby wiedzia³, co to znaczy 100%)
        if (baseHealthScript != null)
        {
            baseHealthSlider.maxValue = baseHealthScript.health;
            baseHealthSlider.value = baseHealthScript.health;
        }
    }

    void Update()
    {
        UpdateGoldUI();
        UpdateWaveUI();
        UpdateTimerUI();
        UpdateHealthUI();
    }

    void UpdateGoldUI()
    {
        if (PlayerCurrency.Instance != null)
            goldText.text = "Z³oto: " + PlayerCurrency.Instance.GetCurrentGold();
    }

    void UpdateWaveUI()
    {
        if (spawner != null)
            waveText.text = "Fala: " + spawner.waveIndex;
    }

    void UpdateTimerUI()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver == false)
        {
            float time = GameManager.Instance.timeElapsed; // ZMIANA: czytamy czas w górê

            // Formatowanie sekund na minuty i sekundy
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            timeText.text = string.Format("Czas: {0:00}:{1:00}", minutes, seconds);
        }
    }

    void UpdateHealthUI()
    {
        if (baseHealthScript != null)
        {
            // Aktualizacja zielonego paska
            baseHealthSlider.value = baseHealthScript.health;
        }
    }
}