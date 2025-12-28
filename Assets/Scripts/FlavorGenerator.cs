using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorGenerator : MonoBehaviour
{
    public List<string> flavors = new List<string> {"Cheese", "Tomato"};
    public float flavorInterval = 10f;
    float timer = 0f;

    void flavorSelector()
    {
        int nFlavors = Random.Range(1, Mathf.Min(4, flavors.Count +1));

        List<string> Flavors = new List<string>(flavors);
        List<string> selectedFlavors = new List<string>();
        List<int> Quantity = new List<int>();

        for (int i = 0; i < nFlavors; i++)
        {
            int index = Random.Range(0, Flavors.Count);
            string ingredient = Flavors[index];
            Flavors.RemoveAt(index);

            int q = Random.Range(1,6);

            selectedFlavors.Add(ingredient);
            Quantity.Add(q);
        }

        Debug.Log("Ingredientes para esta orden:");
        for (int i = 0; i < selectedFlavors.Count; i++)
        {
            Debug.Log(selectedFlavors[i] + " - Cantidad: " + Quantity[i]);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flavorInterval)
        {
            flavorSelector();
            timer = 0f;
        }
    }
}
