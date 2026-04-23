using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [Header("Ustawienia budowania")]
    public GameObject towerPrefab; 
    public int towerCost = 50;     

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        
        
        if (PlayerCurrency.Instance.SpendGold(towerCost))
        {
            PlaceTowerAtMousePosition();
        }
        else
        {
            Debug.Log("Nie staæ Ciê! Potrzebujesz: " + towerCost);
            // Tu w przysz³oœci (KAN-30) dodasz informacjê na ekranie (UI)
        }
    }

    void PlaceTowerAtMousePosition()
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0; 

        
        Instantiate(towerPrefab, worldPosition, Quaternion.identity);
        Debug.Log("Postawiono wie¿ê!");
    }
}