using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularTimer : MonoBehaviour
{
    public SpriteMask mask;
    public SpriteRenderer fill;
    public float totalTime = 10f;

    float currentTime;

    void Start()
    {
        currentTime = totalTime;
        UpdateVisuals();
    }

    void Update()
    {
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            fill.color = Color.black;
            UpdateMask(0f);
            return;
        }

        currentTime -= Time.deltaTime;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        float normalized = Mathf.Clamp01(currentTime / totalTime);
        float elapsed = 1f - normalized;

        UpdateMask(normalized);
        UpdateColor(elapsed);
    }

    void UpdateMask(float normalized)
    {
        // El sector se va cerrando en sentido antihorario
        mask.transform.localScale = new Vector3(normalized, 1f, 1f);
    }

    void UpdateColor(float elapsed)
    {
        if (elapsed < 0.25f)
            fill.color = Color.blue;
        else if (elapsed < 0.50f)
            fill.color = Color.green;
        else if (elapsed < 0.75f)
            fill.color = Color.yellow;
        else if (elapsed < 1.0f)
            fill.color = Color.red;
        else
            fill.color = Color.black;
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
    }
}
