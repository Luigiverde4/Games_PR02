using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPainter : MonoBehaviour
{
    public GameObject tomatoSplatPrefab;   // prefab della macchia
    public bool tomatoMode = false;        // true quando l'utente ha selezionato il pomodoro

    void Update()
    {
        if (!tomatoMode)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("PizzaTomate"))
            {
                // Istanzia la macchia sulla pizza
                GameObject splat = Instantiate(
                    tomatoSplatPrefab,
                    worldPos,
                    Quaternion.Euler(0, 0, Random.Range(0f, 360f)) // rotazione random carina
                );

                // la macchia diventa figlia della pizza
                splat.transform.SetParent(hit.collider.transform);

                Debug.Log("Macchia di pomodoro aggiunta!");
            }
        }
    }

    // Chiamala quando l'utente clicca l'icona del pomodoro
    public void EnableTomatoMode()
    {
        tomatoMode = true;
    }

    // (opzionale) disabilita il tool quando finito
    public void DisableTomatoMode()
    {
        tomatoMode = false;
    }
}