using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoManager : MonoBehaviour
{
    public Sprite tomatoSprite;
    public static bool tomatoMode = false;
    public float targetWorldSize = 4.5f;

    void OnMouseDown()
    {
        tomatoMode = true;
    }

    void Update()
    {
        if (!tomatoMode) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            Collider2D hit = Physics2D.OverlapPoint(worldPos);
            if (hit == null) return;

            PizzaManager pm = hit.GetComponent<PizzaManager>();
            if (pm == null) return;

            if (pm.estado == "extendido")
            {
                SpriteRenderer sr = hit.GetComponent<SpriteRenderer>();
                if (sr == null) return;

                sr.sprite = tomatoSprite;
                AjustarEscala(sr);
                pm.setEstado("tomate");
            }

            tomatoMode = false;
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
