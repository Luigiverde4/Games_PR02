using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : MonoBehaviour
{
    // Sprites para cada modo
    public Sprite spriteInfinito;
    public Sprite spriteCronometro;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Actualizar el sprite al cargar la escena según el modo actual
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        if (spriteRenderer == null || GameModeManager.Instance == null)
            return;

        if (GameModeManager.Instance.currentMode == GameModeManager.GameMode.Infinito)
        {
            spriteRenderer.sprite = spriteInfinito;
            // Aplicar escala específica para el modo Infinito
            transform.localScale = new Vector3(23.4038315f, 24.6626892f, 14.8248911f);
        }
        else if (GameModeManager.Instance.currentMode == GameModeManager.GameMode.Cronometro)
        {
            spriteRenderer.sprite = spriteCronometro;
            // Aplicar escala específica para el modo Cronometro
            transform.localScale = new Vector3(47.6455193f, 48.9307747f, 14.8248911f);
        }
    }

    // Cuando la gente haga click en el botón
    public void OnMouseDown()
    {
        Debug.Log("Botón de modo clickeado");
        if (GameModeManager.Instance == null)
        {
            Debug.LogError("GameModeManager.Instance is null!");
            return;
        }

        // Alternar entre modos
        GameModeManager.GameMode newMode;
        if (GameModeManager.Instance.currentMode == GameModeManager.GameMode.Infinito)
        {
            newMode = GameModeManager.GameMode.Cronometro;
        }
        else
        {
            newMode = GameModeManager.GameMode.Infinito;
        }

        // Cambiar el modo
        GameModeManager.Instance.SetGameMode(newMode);
        Debug.Log("Modo cambiado a: " + newMode);
        
        // Actualizar sprite tras cambiar el modo
        UpdateButtonSprite();
    }
}
