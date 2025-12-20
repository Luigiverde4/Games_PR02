using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextTextController : MonoBehaviour
{
    [Header("Riferimenti")]
    public TextMeshPro textMeshPro;

    [Header("Testi")]
    [TextArea(2, 5)]
    public string[] texts;

    private int currentIndex = 0;

    void Start()
    {
        if (texts.Length > 0)
        {
            textMeshPro.text = texts[currentIndex];
        }
    }

    public void NextText()
    {
        currentIndex++;

        if (currentIndex >= texts.Length)
        {
            currentIndex = texts.Length - 1; // oppure 0 se vuoi loop
        }

        textMeshPro.text = texts[currentIndex];
    }
}