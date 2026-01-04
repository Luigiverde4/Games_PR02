using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpriteButton : MonoBehaviour
{
    public BuyPopup popup;
    public bool isYes = false;

    void OnMouseDown()
    {
        if (isYes)
            popup.ConfirmBuy();
        else
            popup.ClosePopup();
    }
}