using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSelector : MonoBehaviour
{
    private TomatoPainter tomatoPainter;
    private CheesePainter cheesePainter;

    void Start()
    {
        tomatoPainter = FindObjectOfType<TomatoPainter>();
        cheesePainter = FindObjectOfType<CheesePainter>();
    }

    void OnMouseDown()
    {
        if (tomatoPainter != null)
        {
            cheesePainter.DisableCheeseMode();
            tomatoPainter.EnableTomatoMode();
            Debug.Log("Modalità pomodoro attivata!");
        }
    }
}