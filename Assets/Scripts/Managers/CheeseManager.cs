using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseManager : MonoBehaviour
{
    public Sprite cheeseSprite;
    public static bool cheeseMode = false;
    public float targetWorldSize = 5f;

    void OnMouseDown()
    {
        cheeseMode = true;
    }

    void Update()
    {
        if (!cheeseMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            Collider2D hit = Physics2D.OverlapPoint(worldPos);
            if (hit == null) return;

            PizzaManager pm = hit.GetComponent<PizzaManager>();
            if (pm == null) return;

            if (pm.estado == "tomate")
            {
                SpriteRenderer sr = hit.GetComponent<SpriteRenderer>();
                if (sr == null) return;

                sr.sprite = cheeseSprite;
                AjustarEscala(sr);
                pm.setEstado("queso");
            }

            cheeseMode = false;
        }
    }

    void AjustarEscala(SpriteRenderer sr)
    {
        float size = sr.sprite.bounds.size.x;
        if (size <= 0f) return;

        float factor = targetWorldSize / size;
        sr.transform.localScale = new Vector3(factor, factor, 1f);
    }
}
