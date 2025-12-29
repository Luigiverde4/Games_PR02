using UnityEngine;

public class DraggableDough : MonoBehaviour
{
    public Sprite extendedDough;
    public float dragZ = -2f;
    private bool isDragging = false;
    private Vector3 offset;
    private static GameObject doughPlaced;

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;

        offset = transform.position - mousePos;
        isDragging = true;
    }

    public void StartDragging()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;

        offset = transform.position - mousePos;
        isDragging = true;
    }

    void pizzaPlacing()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        Collider2D pizzaZone = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("PizzaZone"))
            {
                pizzaZone = hit;
                break;
            }
        }

        if (pizzaZone != null)
        {
            if (doughPlaced != null && doughPlaced != gameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector3 centerPos = pizzaZone.bounds.center;
                centerPos.z = dragZ;
                transform.position = centerPos;

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr) sr.sprite = extendedDough;

                doughPlaced = gameObject;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        isDragging = false;
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        transform.position = mousePos + offset;

        if (Input.GetMouseButtonUp(0))
        {
            pizzaPlacing();
        }
    }
}