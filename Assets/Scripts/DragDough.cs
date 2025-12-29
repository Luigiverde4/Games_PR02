using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDough : MonoBehaviour
{
    public GameObject doughBall;
    public Sprite extendedSprite;
    private GameObject currentInstance;
    private bool isPlaced = false;
    public float spawnZ = -2f;

    void OnMouseDown()
    {
        if (isPlaced || doughBall == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = spawnZ;
        currentInstance = Instantiate(doughBall, mousePos, Quaternion.identity);

        DraggableDough dd = currentInstance.GetComponent<DraggableDough>();
        dd.startDragging();
        isPlaced = true;
    }

    void Update()
    {
        if (currentInstance == null) return;

        Collider2D hit = Physics2D.OverlapPoint(currentInstance.transform.position);
        if (hit != null && hit.CompareTag("PizzaZone"))
        {
            SpriteRenderer sr = currentInstance.GetComponent<SpriteRenderer>();
            sr.sprite = extendedSprite;

            Vector3 centerPos = hit.bounds.center;
            centerPos.z = spawnZ;
            currentInstance.transform.position = centerPos;

            currentInstance = null;
        }
    }
}
