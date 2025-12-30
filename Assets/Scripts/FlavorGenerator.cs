using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlavorGenerator : MonoBehaviour
{
    [Header("UI Slots")]
    public List<TextMeshPro> orderSlots;

    [Header("Flavors")]
    public List<string> flavors = new List<string> { "Cheese", "Tomato" };
    public float flavorInterval = 10f;
    public int maxOrders = 5;

    float timer = 0f;
    int currentOrders = 0;

    void Start()
    {
        foreach (var slot in orderSlots)
        {
            slot.text = "---";
        }
    }

    TextMeshPro GetEmptySlot()
    {
        foreach (var slot in orderSlots)
        {
            if (slot.text == "---")
                return slot;
        }
        return null;
    }

    IEnumerator ClearOrderAfterDelay(TextMeshPro slot, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        slot.text = "---";
        currentOrders--;
    }

    string CreateOrder()
    {
        int nFlavors = Random.Range(1, flavors.Count + 1);
        List<string> selectedFlavors = new List<string>(flavors);
        List<string> resultFlavors = new List<string>();

        for (int i = 0; i < nFlavors; i++)
        {
            int index = Random.Range(0, selectedFlavors.Count);
            string ingredient = selectedFlavors[index];
            selectedFlavors.RemoveAt(index);
            int quantity = Random.Range(1, 6);
            resultFlavors.Add(quantity + " x" + ingredient);
        }

        string finalOrder = string.Join(", ", resultFlavors);
        Debug.Log(finalOrder);
        return finalOrder;
    }

    void TryCreateOrder()
    {
        if (currentOrders >= maxOrders)
            return;

        TextMeshPro slot = GetEmptySlot();
        if (slot == null)
            return;

        slot.text = CreateOrder();
        currentOrders++;
        StartCoroutine(ClearOrderAfterDelay(slot, 30f));
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flavorInterval)
        {
            TryCreateOrder();
            timer = 0f;
        }
    }
}