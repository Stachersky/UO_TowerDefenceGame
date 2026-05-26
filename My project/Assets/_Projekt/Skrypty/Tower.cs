using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Tower : MonoBehaviour
{
    [Header("Statystyki wie¿y")]
    public float fireRate = 1f;
    public float damage = 25f;

    [Header("Ulepszenia")]
    public int level = 1;
    public int maxLevel = 3;
    public int upgradeCost = 50;

    [Header("Efekt zamra¿ania")]
    [Range(0f, 1f)]
    public float slowPercentage = 0f;
    public float slowDuration = 0f;

    [Header("Pocisk")]
    public GameObject projectilePrefab;

    [Header("DŸwiêk strza³u")]
    public AudioClip shootSound;

    [Header("Tekst poziomu nad wie¿¹")]
    public TextMeshPro levelText;

    private float fireCountdown = 0f;

    [Header("System celowania")]
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject currentTarget;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        originalScale = transform.localScale;

        UpdateLevelText();
    }

    private void Update()
    {
        UpdateTarget();

        if (currentTarget != null)
        {
            fireCountdown -= Time.deltaTime;

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = bulletGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Seek(currentTarget.transform, damage, slowPercentage, slowDuration);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySfx(shootSound);
        }
    }

    private void UpdateTarget()
    {
        enemiesInRange.RemoveAll(item => item == null);

        if (currentTarget != null && !enemiesInRange.Contains(currentTarget))
        {
            currentTarget = null;
        }

        if (currentTarget == null && enemiesInRange.Count > 0)
        {
            currentTarget = enemiesInRange[0];
        }
    }

    private void OnMouseDown()
    {
        if (UpgradeUIManager.Instance != null)
        {
            UpgradeUIManager.Instance.SelectTower(this);
        }
    }

    public void UpgradeTower()
    {
        if (level >= maxLevel)
        {
            Debug.Log("Wie¿a ma maksymalny poziom.");

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayNotEnoughGold();
            }

            return;
        }

        if (PlayerCurrency.Instance == null)
        {
            Debug.Log("Brak PlayerCurrency.");
            return;
        }

        if (PlayerCurrency.Instance.SpendGold(upgradeCost))
        {
            level++;

            damage += 10f;
            fireRate += 0.5f;

            upgradeCost += 50;

            UpdateLevelText();

            StartCoroutine(UpgradeEffect());

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayUpgradeSuccess();
            }

            Debug.Log("Ulepszono wie¿ê do poziomu: " + level);
        }
        else
        {
            Debug.Log("Za ma³o z³ota na ulepszenie.");

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayNotEnoughGold();
            }
        }
    }

    public void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = level.ToString();
        }
    }

    private IEnumerator UpgradeEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }

        transform.localScale = originalScale * 1.2f;

        yield return new WaitForSeconds(0.2f);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        transform.localScale = originalScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}