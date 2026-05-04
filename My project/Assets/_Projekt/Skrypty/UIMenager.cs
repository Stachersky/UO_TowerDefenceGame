using UnityEngine;
using TMPro; // Ta linijka jest BARDZO WA¯NA - pozwala u¿ywaæ TextMeshPro!

public class UIManager : MonoBehaviour
{
    [Header("Elementy UI na ekranie")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timeText;

    [Header("Skrypty z logik¹ gry")]
    public EnemySpawner spawner;

    void Update()
    {
        UpdateGoldUI();
        UpdateWaveUI();
    }

    void UpdateGoldUI()
    {
        // Pobieramy z³oto z Singletona (skrypt kolegi)
        if (PlayerCurrency.Instance != null)
        {
            goldText.text = "Z³oto: " + PlayerCurrency.Instance.GetCurrentGold();
        }
    }

    void UpdateWaveUI()
    {
        if (spawner != null)
        {
            // Aktualizacja numeru fali
            waveText.text = "Fala: " + spawner.waveIndex;

            // Logika timera
            if (spawner.isSpawning)
            {
                timeText.text = "Czas: Atak!";
            }
            else
            {
                // Mathf.Ceil zaokr¹gla u³amki w górê (np. 2.3 sekundy wyœwietli jako 3)
                timeText.text = "Czas: " + Mathf.Ceil(spawner.countdown).ToString();
            }
        }
    }
}