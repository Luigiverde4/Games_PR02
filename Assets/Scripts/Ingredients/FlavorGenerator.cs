using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlavorGenerator : MonoBehaviour
{
    public static FlavorGenerator Instance;

    [Header("UI Slots (solo para esta escena)")]
    public List<TextMeshPro> orderSlots;

    [Header("Flavors")]

    // FLAVOURS:  Cuales ingredientes que puede tener una pizza
    // public List<string> flavors = new List<string> { "Pepperoni", "Mushroom", "Bacon" };
    public List<string> flavors = new List<string>();
    // Coger esta lista para los ingredientes nuevos -  COGER NOMBRE INGREDIENTES DE LOS TAGS

    // Ingredientes básicos para pizza cuando no se han comprado otros
    private List<string> basicIngredients = new List<string> { "Tomato Sauce", "Queso" };

    public float flavorInterval = 10f;
    public int maxOrders = 3;


    // LISTA DE PEDIDOS ACTIVOS "INGREDIENTE xCANTIDAD, INGREDIENTE xCANTIDAD"
    public static List<string> ActiveOrders = new List<string>();

    float timer = 0f;
    bool firstOrderAfterTutorialDone = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void AutoCreate()
    {
        if (Instance != null) return;

        GameObject go = new GameObject("FlavorGenerator");
        Instance = go.AddComponent<FlavorGenerator>();
        DontDestroyOnLoad(go);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Limpiar pedidos al iniciar
        ActiveOrders.Clear();
        timer = 0f;
        firstOrderAfterTutorialDone = false;
    }

    void Update()
    {
        // No generar pedidos si es la primera vez (tutorial activo)
        if (GameModeManager.Instance != null && GameModeManager.Instance.isFirstTime)
        {
            // Resetear timer mientras está en tutorial
            timer = 0f;
            firstOrderAfterTutorialDone = false;
            return;
        }

        // Crear un pedido inmediatamente al terminar el tutorial
        if (!firstOrderAfterTutorialDone)
        {
            TryCreateOrder();
            firstOrderAfterTutorialDone = true;
            timer = 0f;
        }
        
        timer += Time.deltaTime;

        if (timer >= flavorInterval)
        {
            TryCreateOrder();
            timer = 0f;
        }
    }

    void TryCreateOrder()
    // Crear un nuevo pedido si no se ha alcanzado el máximo
    {
        if (ActiveOrders.Count >= maxOrders)
            return;

        string order = CreateOrder();
        ActiveOrders.Add(order);
        Debug.Log("Nuevo pedido creado: " + order);

        // Cada pedido esta durante 60 segundos
        StartCoroutine(RemoveOrderAfterDelay(order, 60f));
    }

    IEnumerator RemoveOrderAfterDelay(string order, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ActiveOrders.Remove(order);
        Debug.Log("Pedido expirado y removido: " + order);
    }

    string CreateOrder()
    // Crear un pedido aleatorio
    {
        // Coger los ingredientes comprados
        List<string> availableFlavors;
        if (BoughtIngredientTracker.Instance != null)
        {
            availableFlavors = BoughtIngredientTracker.Instance.boughtIngredients;
        }
        else
        {
            availableFlavors = new List<string>();
        }
        
        // Si no hay ingredientes comprados, usar los básicos
        if (availableFlavors.Count == 0)
        {
            availableFlavors = new List<string>(basicIngredients);
        }
        
        // Crear lista de resultados empezando con ingredientes base
        List<string> result = new List<string>();
        result.Add("1 xTomato Sauce");
        result.Add("1 xQueso");
        
        // Crear pool de ingredientes disponibles (sin los básicos)
        List<string> pool = new List<string>(availableFlavors);
        pool.Remove("Tomato Sauce");
        pool.Remove("Queso");
        
        // Si hay ingredientes adicionales, agregar algunos aleatoriamente
        if (pool.Count > 0)
        {
            int nAdditional = Random.Range(0, Mathf.Min(pool.Count, 2) + 1); // 0 a 2 ingredientes extra
            
            for (int i = 0; i < nAdditional && pool.Count > 0; i++)
            {
                int index = Random.Range(0, pool.Count);
                string ingredient = pool[index];
                pool.RemoveAt(index);
                int quantity = Random.Range(1, 4); // 1 a 3 unidades
                result.Add(quantity + " x" + ingredient);
            }
        }

        result.Sort();
        return string.Join(", ", result);
    }
}