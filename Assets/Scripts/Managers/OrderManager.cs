using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public List<TextMeshPro> orderSlots;

    void SyncOrders()
    {
        foreach (var slot in orderSlots)
        {
            if (!string.IsNullOrEmpty(slot.text) && slot.text != "---")
            {
                if (!FlavorGenerator.ActiveOrders.Contains(slot.text))
                {
                    slot.text = "---";
                }
            }
        }

        foreach (string order in FlavorGenerator.ActiveOrders)
        {
            if (!IsOrderDisplayed(order))
            {
                TextMeshPro slot = GetEmptySlot();
                if (slot != null)
                {
                    slot.text = order;
                }
            }
        }
    }

    bool IsOrderDisplayed(string order)
    {
        foreach (var slot in orderSlots)
        {
            if (slot.text == order) return true;
        }
        return false;
    }

    TextMeshPro GetEmptySlot()
    {
        foreach (var slot in orderSlots)
        {
            if (string.IsNullOrEmpty(slot.text) || slot.text == "---") return slot;
        }
        return null;
    }

    void Update()
    {
        SyncOrders();
    }
}
