using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDough : MonoBehaviour
{
    public Sprite stretchedPizzaSprite;

    private bool isDragging = false;
    private Vector3 offset;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        offset = transform.position - mousePos;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PizzaZone"))
        {
            sr.sprite = stretchedPizzaSprite;
            Debug.Log("Pizza stesa!");
        }
    }
}
