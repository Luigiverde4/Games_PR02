using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSelector : MonoBehaviour
{
    private CheesePainter cheesePainter;
    private TomatoPainter tomatoPainter;

    void Start()
    {
        cheesePainter = FindObjectOfType<CheesePainter>();
        tomatoPainter = FindObjectOfType<TomatoPainter>();
    }

    void OnMouseDown()
    {
        if (cheesePainter != null)
        {
            tomatoPainter.DisableTomatoMode();
            cheesePainter.EnableCheeseMode();
            Debug.Log("Modalità formaggio attivata!");
        }
    }
}