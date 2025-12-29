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
        IniciarArrastre();
    }

    public void StartDragging()
    {
        IniciarArrastre();
    }

    void IniciarArrastre()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;

        offset = transform.position - mousePos;
        isDragging = true;
    }

    void pizzaPlacing()
    {
        isDragging = false;

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

        if (pizzaZone == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 centerPos = pizzaZone.bounds.center;
        centerPos.z = dragZ;
        transform.position = centerPos;

        PizzaManager pm = GetComponent<PizzaManager>();
        if (pm == null) return;

        if (pm.estado == "bola")
        {
            if (doughPlaced != null && doughPlaced != gameObject)
            {
                Destroy(gameObject);
                return;
            }

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = extendedDough;

            pm.setEstado("extendido");
            doughPlaced = gameObject;
        }
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        transform.position = mousePos + offset;

        if (Input.GetMouseButtonUp(0))
            pizzaPlacing();
    }
}