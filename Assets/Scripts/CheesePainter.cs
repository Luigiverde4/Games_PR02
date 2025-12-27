using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheesePainter : MonoBehaviour
{
    public GameObject cheeseSplatPrefab;   
    public bool cheeseMode = false;       
    void Update()
    {
        if (!cheeseMode)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("PizzaTomate"))
            {
                GameObject splat = Instantiate(
                    cheeseSplatPrefab,
                    worldPos,
                    Quaternion.Euler(0, 0, Random.Range(0f, 360f)) 
                );

                splat.transform.SetParent(hit.collider.transform);

                Debug.Log("Macchia di formaggio aggiunta!");
            }
        }
    }

    // Chiamala quando l'utente clicca l'icona del formaggio
    public void EnableCheeseMode()
    {
        cheeseMode = true;
    }

    // (opzionale) disabilita il tool quando finito
    public void DisableCheeseMode()
    {
        cheeseMode = false;
    }
}