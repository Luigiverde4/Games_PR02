using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPopup : MonoBehaviour
{
    public GameObject panel;

    public void OpenPopup()
    {
        panel.SetActive(true);
        Debug.Log("Popup aperto.");

    }

    public void ClosePopup()
    {
        panel.SetActive(false);
        
    }

    public void ConfirmBuy()
    {
        Debug.Log("Hai comprato!");
        ClosePopup();
    }
}
