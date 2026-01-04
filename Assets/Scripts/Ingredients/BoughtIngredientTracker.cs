using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtIngredientTracker : MonoBehaviour
{
    public static BoughtIngredientTracker Instance;
    public List<string> boughtIngredients = new List<string>();

    // Que sea singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddIngredient(string ingredientName)
    {
        if (!boughtIngredients.Contains(ingredientName))
        {
            boughtIngredients.Add(ingredientName);
            Debug.Log("Ingredient added: " + ingredientName);
        }
    }

    public bool HasIngredient(string ingredientName)
    {
        return boughtIngredients.Contains(ingredientName);
    }
}
