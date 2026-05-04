using UnityEngine;
using UnityEngine.EventSystems; // Niezbêdne do obs³ugi przeci¹gania UI!

public class DragDropTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Ustawienia Wie¿y")]
    public GameObject towerPrefab;
    public int towerCost = 50;

    private GameObject dragPreview;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Sprawdzamy czy staæ nas na wie¿ê, zanim zaczniemy ci¹gn¹æ
        if (PlayerCurrency.Instance.GetCurrentGold() < towerCost)
        {
            Debug.Log("Za ma³o z³ota, aby zacz¹æ budowê!");
            return; // Przerywamy - gracz jest za biedny!
        }

        // Tworzymy "ducha" wie¿y do podgl¹du (zamiast prawdziwej wie¿y, ¿eby od razu nie strzela³a)
        dragPreview = new GameObject("TowerPreview");
        SpriteRenderer sr = dragPreview.AddComponent<SpriteRenderer>();

        // Kopiujemy grafikê i rozmiar z oryginalnego prefaba
        sr.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;
        dragPreview.transform.localScale = towerPrefab.transform.localScale;

        // Ustawiamy kolor na lekko przezroczysty i warstwê na sam wierzch mapy
        sr.color = new Color(1f, 1f, 1f, 0.5f);
        sr.sortingOrder = 10;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Jeœli mamy ducha (czyli staæ nas by³o na start), przesuwamy go za kursorem
        if (dragPreview != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Upewniamy siê, ¿e duch jest p³asko na mapie (Z = 0)
            dragPreview.transform.position = mousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Gdy puœcimy lewy przycisk myszy
        if (dragPreview != null)
        {
            Vector3 dropPosition = dragPreview.transform.position;
            Destroy(dragPreview); // Usuwamy podgl¹d

            // Sprawdzamy portfel i jeœli siê zgadza, stawiamy prawdziw¹ wie¿ê
            if (PlayerCurrency.Instance.SpendGold(towerCost))
            {
                Instantiate(towerPrefab, dropPosition, Quaternion.identity);
                Debug.Log("Postawiono wie¿ê na koordynatach: " + dropPosition);
            }
        }
    }
}