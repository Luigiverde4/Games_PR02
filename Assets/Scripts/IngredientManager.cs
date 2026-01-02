using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public gameObject ingredientPrefab;

    public Sprite pepperoni;
    public Sprite mushroom;
    public Sprite bacon;
    public Sprite egg;
    public Sprite onion;
    public Sprite pepper;
    public Sprite olive;
    public Sprite pineapple;
    public Sprite shrimp;
    public Sprite cheese;
    public Sprite anchovies;
    public Sprite caper;

    public float dragZ = -2f;

    private gameObject currentIngredient;
    private bool isDragging = false;
    private string ingredientType;

    void GetSprite(string type)
    {
        switch (type)
        {
            case "Pepperoni": return pepperoni;
            case "Mushroom": return mushroom;
            case "Bacon": return bacon;
            case "Egg": return egg;
            case "Onion":return onion;
            case "Pepper": return pepper;
            case "Olive": return olive;
            case "Pineapple": return pineapple;
            case "Shrimp": return shrimp;
            case "Cheese": return  cheese;
        }
    }

    void SpawnIngredient()
    {
        ingredientType = gameObject.Tag;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        currentIngredient = Instantiate(ingredientPrefab, mousePos, Quaternion.identity);

        SpriteRenderer sr = currentIngredient.GetComponent<SpriteRenderer>();
        sr.sprite = GetSprite(ingredientType);
        isDragging = true;
    } 
}
