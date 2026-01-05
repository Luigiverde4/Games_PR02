using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public List<TextMeshPro> orderSlots;

    void SyncOrders()
    {
        // Reset all slots first to allow duplicates to occupy separate slots
        foreach (var slot in orderSlots)
        {
            slot.text = "---";
        }

        // Fill slots in order; duplicates will consume multiple slots
        int slotIndex = 0;
        foreach (string order in FlavorGenerator.ActiveOrders)
        {
            if (slotIndex >= orderSlots.Count) break;
            orderSlots[slotIndex].text = order;
            slotIndex++;
        }
    }

    void Update()
    {
        SyncOrders();
    }
}
