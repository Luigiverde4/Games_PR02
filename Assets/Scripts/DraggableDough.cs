using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDough : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public float dragZ = -2f;

    public void startDragging()
    {
        
        Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPos.z = dragZ;
        offset = transform.position - clickPos;
        isDragging = true;
    }

    void checkPlace()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);

        if (hit != null && !hit.CompareTag("PizzaZone"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        transform.position = mousePos + offset;

        if (Input.GetMouseButtonUp(0))
        {
            checkPlace();
            isDragging = false;
        }
    }
}
