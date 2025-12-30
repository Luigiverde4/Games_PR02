using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDough : MonoBehaviour
{
    public Sprite extendedDough;
    public Sprite coockedPizza;
    public Sprite burntPizza;

    public float dragZ = -2f;
    private bool isDragging = false;
    private Vector3 offset;
    private static GameObject doughPlaced;

    public float cookingTime = 10f;
    public float burningTime = 3f;
    public int furnaceCapacity = 1;

    private static int furnaceCounter = 0;
    private Coroutine furnaceRoutine;
    private bool inFurnace = false;

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

    void enterFurnace()
    {
        if(inFurnace) return;

        inFurnace = true;
        furnaceCounter++;
        furnaceRoutine = StartCoroutine(furnaceMode());
    }

    void exitFurnace()
    {
        if (!inFurnace) return;

        inFurnace = false;
        furnaceCounter = Mathf.Max(0, furnaceCounter - 1);
        furnaceRoutine = null;
    }

    IEnumerator furnaceMode()
    {
        yield return new WaitForSeconds(cookingTime);

        PizzaManager pm = GetComponent<PizzaManager>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (!inFurnace || pm == null || sr == null) yield break;

        sr.sprite = coockedPizza;
        pm.setEstado("cocinado");

        yield return new WaitForSeconds(burningTime);
        if (!inFurnace) yield break;

        sr.sprite = burntPizza;
        pm.setEstado("quemado");
    }

    void pizzaPlacing()
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        Collider2D pizzaZone = null;
        Collider2D furnaceZone = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("PizzaZone"))
            {
                pizzaZone = hit;
            }
            else if (hit.CompareTag("Furnace"))
            {
                furnaceZone = hit;
            }
        }

        PizzaManager pm = GetComponent<PizzaManager>();
        if (pm == null) return;

        if (pizzaZone != null)
        {
            Vector3 centerPos = pizzaZone.bounds.center;
            centerPos.z = dragZ;
            transform.position = centerPos;

            if (pm.estado == "bola")
            {
                if (doughPlaced != null && doughPlaced != gameObject)
                {
                    Destroy(gameObject);
                    return;
                }

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = extendedDough;
                }
                pm.setEstado("extendido");
                doughPlaced = gameObject;
            }
            return;
        }

        if (furnaceZone != null)
        {
            if (inFurnace)
            {
                return;
            }
            
            if (pm.estado != "queso")
            {
                Destroy(gameObject);
                return;
            }

            if (!inFurnace && furnaceCounter >= furnaceCapacity) return;

            enterFurnace();
            return;
        }

        if (inFurnace)
        {
            exitFurnace();
        }
        
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (inFurnace)
        {
            exitFurnace();
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
            pizzaPlacing();
        }
    }
}