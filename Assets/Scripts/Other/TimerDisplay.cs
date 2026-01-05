using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Asignar en inspector
    public float timerDuration = 60f; // Duración del cronómetro en segundos
    
    private float currentTime = 0f;
    private bool isRunning = false;
    private GameModeManager.GameMode lastMode = GameModeManager.GameMode.Infinito;
    
    // Evento cuando el tiempo se acaba
    public delegate void TimerEndedDelegate();
    public event TimerEndedDelegate OnTimerEnded;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Asigna el TextMeshPro en el inspector");
        }
        
        // Suscribirse al evento de fin de tiempo
        OnTimerEnded += HandleTimerEnded;
        
        UpdateDisplay();
    }

    void OnDestroy()
    {
        // Desuscribirse del evento al destruir
        OnTimerEnded -= HandleTimerEnded;
    }

    // Manejador cuando el tiempo se acaba
    void HandleTimerEnded()
    {
        // Cambiar modo a Infinito
        if (GameModeManager.Instance != null)
        {
            GameModeManager.Instance.currentMode = GameModeManager.GameMode.Infinito;
            GameModeManager.Instance.cronoInitialized = false;
            GameModeManager.Instance.cronoCurrentTime = 0f;
            Debug.Log("Tiempo acabado! Cambiando a modo Infinito y regresando al menú...");
        }
        
        // Cargar escena del menú
        SceneManager.LoadScene("SceneRestaurantOut");
    }

    void Update()
    {
        if (GameModeManager.Instance == null)
            return;

        // Verificar que estamos en una escena válida para el timer
        string currentScene = SceneManager.GetActiveScene().name;
        bool isValidScene = currentScene == "SceneDoingPizza" || currentScene == "SceneClient";

        // Detectar cambio de modo
        if (GameModeManager.Instance.currentMode != lastMode)
        {
            lastMode = GameModeManager.Instance.currentMode;
            
            if (lastMode == GameModeManager.GameMode.Cronometro && !GameModeManager.Instance.isFirstTime && isValidScene)
            {
                // Entró en modo Cronometro, tutorial terminado y escena válida
                // Si es la primera vez que iniciamos el crono, usar timerDuration
                // Si ya estaba iniciado, usar el tiempo guardado
                if (!GameModeManager.Instance.cronoInitialized)
                {
                    StartTimer(timerDuration);
                    GameModeManager.Instance.cronoInitialized = true;
                }
                else
                {
                    // Recuperar el tiempo guardado
                    currentTime = GameModeManager.Instance.cronoCurrentTime;
                    isRunning = true;
                }
            }
            else
            {
                // Salió de modo Cronometro o no es escena válida
                StopTimer();
            }
        }

        // Si el timer está corriendo, restar tiempo (solo si estamos en escena válida)
        if (isRunning && isValidScene && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            // Guardar el tiempo actual en GameModeManager
            GameModeManager.Instance.cronoCurrentTime = currentTime;
            
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                Debug.Log("¡Se acabó el tiempo!");
                OnTimerEnded?.Invoke();
            }
            
            UpdateDisplay();
        }
        else if (isRunning && !isValidScene)
        {
            // Si estamos fuera de las escenas válidas, pausar el timer pero mantener el tiempo
            GameModeManager.Instance.cronoCurrentTime = currentTime;
            isRunning = false;
        }
        
        // Actualizar visibilidad (solo si modo es Cronometro Y tutorial terminado Y escena válida)
        bool shouldBeActive = GameModeManager.Instance.currentMode == GameModeManager.GameMode.Cronometro 
                            && !GameModeManager.Instance.isFirstTime
                            && isValidScene;
        gameObject.SetActive(shouldBeActive);
    }

    // Iniciar el countdown con X segundos
    public void StartTimer(float seconds)
    {
        currentTime = Mathf.Max(0, seconds);
        isRunning = true;
        UpdateDisplay();
    }

    // Pausar/reanudar
    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    // Parar el timer
    public void StopTimer()
    {
        isRunning = false;
        currentTime = 0;
        UpdateDisplay();
    }

    public void AddTime(float seconds)
    {
        currentTime += seconds;
        if (currentTime < 0) currentTime = 0;
        UpdateDisplay();
    }

    public void SubtractTime(float seconds)
    {
        currentTime -= seconds;
        if (currentTime < 0) currentTime = 0;
        UpdateDisplay();
    }

    public float GetTime()
    {
        return currentTime;
    }

    void UpdateDisplay()
    {
        if (timerText == null)
            return;

        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


