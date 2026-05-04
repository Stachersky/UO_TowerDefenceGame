using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Co kupujemy?")]
    public GameObject towerPrefab;
    public int towerCost = 50;

    private GameObject dragPreview;
    private SpriteRenderer previewRenderer;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlayerCurrency.Instance.GetCurrentGold() < towerCost)
        {
            Debug.Log("Za ma³o z³ota!");
            return;
        }

        dragPreview = new GameObject("TowerPreview");
        previewRenderer = dragPreview.AddComponent<SpriteRenderer>();

        SpriteRenderer prefabRenderer = towerPrefab.GetComponent<SpriteRenderer>();
        if (prefabRenderer != null)
        {
            previewRenderer.sprite = prefabRenderer.sprite;
            dragPreview.transform.localScale = towerPrefab.transform.localScale;
        }

        // --- KLUCZOWA POPRAWKA ---
        // Zamiast gigantycznego kó³ka zasiêgu, dodajemy ma³y kwadrat (BoxCollider).
        // Unity automatycznie dopasuje jego wielkoœæ do rozmiaru obrazka wie¿y!
        BoxCollider2D col = dragPreview.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        // Zmniejszamy hitboxa o 20%, ¿eby mo¿na by³o stawiaæ wie¿e bardzo blisko œcie¿ki
        col.size = col.size * 0.8f;
        // -------------------------

        Rigidbody2D rb = dragPreview.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;

        dragPreview.AddComponent<PlacementValidator>();

        previewRenderer.sortingOrder = 50;
        previewRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragPreview != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            dragPreview.transform.position = mousePos;

            PlacementValidator validator = dragPreview.GetComponent<PlacementValidator>();

            if (validator.CanPlace())
            {
                previewRenderer.color = new Color(0f, 1f, 0f, 0.6f);
            }
            else
            {
                previewRenderer.color = new Color(1f, 0f, 0f, 0.6f);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragPreview != null)
        {
            Vector3 finalPosition = dragPreview.transform.position;
            PlacementValidator validator = dragPreview.GetComponent<PlacementValidator>();
            bool isPlacementLegal = validator.CanPlace();

            Destroy(dragPreview);

            if (isPlacementLegal && PlayerCurrency.Instance.SpendGold(towerCost))
            {
                GameObject newTower = Instantiate(towerPrefab, finalPosition, Quaternion.identity);
                newTower.transform.position = new Vector3(finalPosition.x, finalPosition.y, 0f);
            }
            else
            {
                Debug.Log("Nie mo¿na tu zbudowaæ wie¿y!");
            }
        }
    }
}