using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Infinito,
        Cronometro,
        
    }

    // Modo por defecto
    public GameMode currentMode = GameMode.Infinito; 

    // Singleton
    public static GameModeManager Instance { get; private set; }

    // Revisar si es la primera vez que juega para darle la intro o no
    public bool isFirstTime;

    void Awake()
    {
        // Que solo exista una instancia de GameModeManager entre escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            // Initialize first time flag
            isFirstTime = true;
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    public void SetGameMode(GameMode newMode)
    {
        // Logica extra para cambiar el modo de juego
        currentMode = newMode;
    }

    public GameMode GetCurrentMode()
    {
        return currentMode;
    }
}
