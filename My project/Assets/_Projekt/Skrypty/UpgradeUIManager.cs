using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeUIManager : MonoBehaviour
{
    public static UpgradeUIManager Instance;

    [Header("Panel ulepszania")]
    public GameObject upgradePanel;
    public TextMeshProUGUI upgradeInfoText;
    public Button upgradeButton;

    private Tower selectedTower;

    private void Awake()
    {
        Instance = this;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    private void Start()
    {
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(UpgradeSelectedTower);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClickOutsideTower();
        }
    }

    private void CheckClickOutsideTower()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider == null)
        {
            HidePanel();
            return;
        }

        Tower tower = hit.collider.GetComponent<Tower>();

        if (tower == null)
        {
            HidePanel();
        }
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
        }

        RefreshPanel();
    }

    public void UpgradeSelectedTower()
    {
        if (selectedTower == null)
        {
            return;
        }

        selectedTower.UpgradeTower();

        RefreshPanel();
    }

    private void RefreshPanel()
    {
        if (selectedTower == null)
        {
            return;
        }

        if (upgradeInfoText != null)
        {
            if (selectedTower.level >= selectedTower.maxLevel)
            {
                upgradeInfoText.text =
                    "Wie¿a\n" +
                    "Poziom: MAX\n" +
                    "Obra¿enia: " + selectedTower.damage + "\n" +
                    "Szybkoœæ: " + selectedTower.fireRate;
            }
            else
            {
                upgradeInfoText.text =
                    "Wie¿a\n" +
                    "Poziom: " + selectedTower.level + " / " + selectedTower.maxLevel + "\n" +
                    "Koszt: " + selectedTower.upgradeCost + "\n" +
                    "Obra¿enia: " + selectedTower.damage + "\n" +
                    "Szybkoœæ: " + selectedTower.fireRate;
            }
        }

        if (upgradeButton != null)
        {
            upgradeButton.interactable = selectedTower.level < selectedTower.maxLevel;
        }
    }

    public void HidePanel()
    {
        selectedTower = null;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }
}