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
    }

    void UpdateButtonSprite()
    {
        if (spriteRenderer == null || GameModeManager.Instance == null)
            return;

        if (GameModeManager.Instance.currentMode == GameModeManager.GameMode.Infinito)
        {
            spriteRenderer.sprite = spriteInfinito;
        }
        else if (GameModeManager.Instance.currentMode == GameModeManager.GameMode.Cronometro)
        {
            spriteRenderer.sprite = spriteCronometro;
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
