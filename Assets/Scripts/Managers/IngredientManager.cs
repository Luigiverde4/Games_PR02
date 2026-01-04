using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
   public GameObject ingredientPrefab;
   public Sprite ingredientSprite;

   public float dragZ = -3f;
   public Vector2 ingredientScale = new Vector2(1f,1f);
   private GameObject currentIngredient;
   private bool isDragging = false;

   void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        currentIngredient = Instantiate(ingredientPrefab, mousePos, Quaternion.identity);

        SpriteRenderer sr = currentIngredient.GetComponent<SpriteRenderer>();
        sr.sprite = ingredientSprite;
        currentIngredient.transform.localScale = new Vector3(ingredientScale.x, ingredientScale.y, 1f);
        isDragging = true;
    }

    void PlaceIngredient()
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(currentIngredient.transform.position);
        Collider2D pizzaCollider = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Pizza"))
            {
                pizzaCollider = hit;
                break;
            }
        }

        if (pizzaCollider == null)
        {
            Destroy(currentIngredient);
            return;
        }

        PizzaManager pm = pizzaCollider.GetComponentInParent<PizzaManager>();
        if (pm == null || pm.estado != "queso")
        {
            Destroy(currentIngredient);
            return;
        }

        currentIngredient.transform.SetParent(pizzaCollider.transform);
        Collider2D col = currentIngredient.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        SpriteRenderer pizzaSR = pizzaCollider.GetComponent<SpriteRenderer>();
        SpriteRenderer ingredientSR = currentIngredient.GetComponent<SpriteRenderer>();

        if (pizzaSR != null && ingredientSR != null)
        {
            ingredientSR.sortingLayerID = pizzaSR.sortingLayerID;
            ingredientSR.sortingOrder = pizzaSR.sortingOrder +1;
        }

        switch (gameObject.tag)
        {
            case "Pepperoni":
                pm.pepperoni += 1;
                break;
            case "Mushroom":
                pm.mushroom += 1;
                break;
            case "Bacon":
                pm.bacon += 1;
                break;
            case "Egg":
                pm.egg += 1;
                break;
            case "Olive":
                pm.olive += 1;
                break;
            case "Onion":
                pm.onion += 1;
                break;
            case "Pineapple":
                pm.pineapple += 1;
                break;
            case "Pepper":
                pm.pepper += 1;
                break;
            case "Shrimp":
                pm.shrimp += 1;
                break;
            case "Cheese":
                pm.cheese += 1;
                break;
            case "Anchovies":
                pm.anchovies += 1;
                break;
            case "Caper":
                pm.caper += 1;
                break;
        }

        currentIngredient = null;
    }

    void Update()
    {
        if (!isDragging || currentIngredient == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        currentIngredient.transform.position = mousePos;

        if (Input.GetMouseButtonUp(0))
        {
            PlaceIngredient();
        }
    }
}
