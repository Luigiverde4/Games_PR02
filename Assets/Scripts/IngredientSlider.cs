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

    public Ingredient[] ingredients;

    private int currentIndex = 0;

    void Start()
    {
        ShowIngredient();
    }

    public void NextIngredient()
    {
        currentIndex++;

        if (currentIndex >= ingredients.Length)
            currentIndex = 0;

        ShowIngredient();
    }

    public void PreviousIngredient()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = ingredients.Length - 1;

        ShowIngredient();
    }

    void ShowIngredient()
    {
        ingredientImage.sprite = ingredients[currentIndex].icon;
        priceText.text = ingredients[currentIndex].price + " $";
        nameText.text = ingredients[currentIndex].name;
    }
}
