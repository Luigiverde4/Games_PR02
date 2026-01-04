using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScale : MonoBehaviour
{
    Vector3 originalScale;
    public float hoverScale = 1.15f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        transform.localScale = originalScale * hoverScale;
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
    }
}
