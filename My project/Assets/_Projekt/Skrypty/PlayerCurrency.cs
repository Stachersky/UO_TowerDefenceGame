using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    
    public int startingGold = 100;
    private int currentGold;

    public static PlayerCurrency Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentGold = startingGold;
        Debug.Log("Startowy gold gracza: " + currentGold);
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log("Dodano " + amount + " golda. Aktualny gold: " + currentGold);
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log("Wydano " + amount + " golda. Zostalo: " + currentGold);
            return true;
        }

        Debug.Log("Za malo golda. Aktualny gold: " + currentGold);
        return false;
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }
}