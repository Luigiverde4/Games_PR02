using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSlider : MonoBehaviour
{
    [System.Serializable]
    public class Ingredient
    {
        public Sprite icon;
        public int price;
        public string name;
    }

    public Image ingredientImage;
    public TextMeshPro priceText;
    public TextMeshPro nameText;

    public List<Ingredient> ingredients = new List<Ingredient>();

    private int currentIndex = 0;

    void Start()
    {
        // Filtrar ingredientes ya comprados
        if (BoughtIngredientTracker.Instance != null)
        {
            Debug.Log("Ingredientes comprados: " + string.Join(", ", BoughtIngredientTracker.Instance.boughtIngredients));
            
            // Crear una nueva lista sin los ingredientes comprados
            List<Ingredient> filteredIngredients = new List<Ingredient>();
            foreach (Ingredient ing in ingredients)
            {
                if (!BoughtIngredientTracker.Instance.HasIngredient(ing.name))
                {
                    filteredIngredients.Add(ing);
                    Debug.Log("Ingrediente disponible: " + ing.name);
                }
                else
                {
                    Debug.Log("Ingrediente ya comprado (filtrado): " + ing.name);
                }
            }
            ingredients = filteredIngredients;
        }
        
        ShowIngredient();
    }

    public void NextIngredient()
    {
        if (ingredients.Count == 0) return;

        currentIndex++;

        if (currentIndex >= ingredients.Count)
            currentIndex = 0;

        ShowIngredient();
    }

    public void PreviousIngredient()
    {
        if (ingredients.Count == 0) return;

        currentIndex--;

        if (currentIndex < 0)
            currentIndex = ingredients.Count - 1;

        ShowIngredient();
    }

    void ShowIngredient()
    {
        if (ingredients.Count == 0)
        {
            ingredientImage.sprite = null;
            priceText.text = "";
            nameText.text = "";
            return;
        }

        ingredientImage.sprite = ingredients[currentIndex].icon;
        priceText.text = ingredients[currentIndex].price + " $";
        nameText.text = ingredients[currentIndex].name;
    }

    public void RemoveIngredientByName(string ingredientName)
    {
        int idx = ingredients.FindIndex(i => i.name == ingredientName);
        if (idx < 0) return;

        ingredients.RemoveAt(idx);
        if (ingredients.Count == 0)
        {
            currentIndex = 0;
            ShowIngredient();
            return;
        }

        if (currentIndex >= ingredients.Count)
            currentIndex = Mathf.Max(0, ingredients.Count - 1);

        ShowIngredient();
    }
}
