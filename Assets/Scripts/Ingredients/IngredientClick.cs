using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientClick : MonoBehaviour
{
    public BuyPopup popup;

    void OnMouseDown()
    {
        popup.OpenPopup();
    }
}
