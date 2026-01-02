using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowClick : MonoBehaviour
{
    public IngredientSlider ingredientSlider;
    public bool nextArrow = true;

    void OnMouseDown()
    {
        if (nextArrow)
            ingredientSlider.NextIngredient();
        else
            ingredientSlider.PreviousIngredient();
    }
}
