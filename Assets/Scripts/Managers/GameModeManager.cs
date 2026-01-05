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
    private bool _isFirstTime = true;
    public bool isFirstTime
    {
        get { return _isFirstTime; }
        set 
        { 
            if (_isFirstTime != value)
            {
                Debug.Log("isFirstTime changed from " + _isFirstTime + " to " + value);
                UnityEngine.Debug.LogWarning("Stack trace: " + UnityEngine.StackTraceUtility.ExtractStackTrace());
            }
            _isFirstTime = value;
        }
    }

    void Awake()
    {
        // Que solo exista una instancia de GameModeManager entre escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Forzar que sea true al crear la instancia, ignorando el inspector
            _isFirstTime = true;
            Debug.Log("GameModeManager created. isFirstTime: " + isFirstTime);
        }
        else
        {
            Debug.Log("Duplicate GameModeManager destroyed. Its isFirstTime was: " + isFirstTime);
            Destroy(gameObject); 
        }
    }

    // Para testing: resetear el tutorial
    public void ResetFirstTime()
    {
        isFirstTime = true;
        Debug.Log("FirstTime flag reset to true");
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
