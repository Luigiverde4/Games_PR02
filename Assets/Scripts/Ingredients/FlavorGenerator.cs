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

    public float flavorInterval = 15f;
    public int maxOrders = 3;


    // LISTA DE PEDIDOS ACTIVOS "INGREDIENTE xCANTIDAD, INGREDIENTE xCANTIDAD"
    public static List<string> ActiveOrders = new List<string>();

    float timer = 0f;

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
    }

    void Update()
    {
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
        
        // Decidir cuántos sabores tendrá el pedido
        int nFlavors = Random.Range(1, availableFlavors.Count + 1);
        // Crear el pedido
        List<string> pool = new List<string>(availableFlavors);
        List<string> result = new List<string>();

        // Seleccionar sabores aleatoriamente
        for (int i = 0; i < nFlavors; i++)
        {
            int index = Random.Range(0, pool.Count);
            string ingredient = pool[index];
            pool.RemoveAt(index);
            int quantity = (ingredient == "Tomato Sauce" || ingredient == "Queso") ? 1 : Random.Range(1, 6);
            result.Add(quantity + " x" + ingredient);
        }

        // Asegurar que si hay queso, también haya salsa de tomate
        bool hasCheese = result.Exists(s => s.Contains("Queso"));
        bool hasTomato = result.Exists(s => s.Contains("Tomato Sauce"));
        if (hasCheese && !hasTomato)
        {
            result.Add("1 xTomato Sauce");
        }

        // Asegurar que TODOS los pedidos tengan salsa de tomate y queso
        if (!hasTomato)
        {
            result.Add("1 xTomato Sauce");
        }
        if (!hasCheese)
        {
            result.Add("1 xQueso");
        }

        result.Sort();
        return string.Join(", ", result);
    }
}