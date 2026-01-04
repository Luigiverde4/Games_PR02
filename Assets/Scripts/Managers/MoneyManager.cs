using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    private int dineroActual = 100;

    public TMP_Text moneyText; // Asigna en el Inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateMoneyUI();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Dar dinero al jugador
    public void sumarDinero(int cantidad)
    {
        if (cantidad > 0)
        {
            dineroActual += cantidad;
            UpdateMoneyUI();
        }
    }

    // Quitar dinero, true si lo pudo hacer
    public bool quitarDinero(int cantidad)
    {
        if (cantidad > 0 && dineroActual >= cantidad)
        {
            dineroActual -= cantidad;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }

    // Getter del dinero actual
    public int GetMoney()
    {
        return dineroActual;
    }

    // Actualizar la UI del dinero
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = dineroActual.ToString();
        }
    }

    // Cuando se carga una escena, buscar el texto de dinero
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject moneyTextObj = GameObject.Find("cantidadDinero");
        if (moneyTextObj != null)
        {
            moneyText = moneyTextObj.GetComponent<TMP_Text>();
            UpdateMoneyUI();
        }
    }

    public void setMoney(int cantidad)
    {
        dineroActual = cantidad;
        UpdateMoneyUI();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
