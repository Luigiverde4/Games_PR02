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
    public List<string> flavors = new List<string> { "Pepperoni", "Mushroom", "Bacon" };
    public float flavorInterval = 10f;
    public int maxOrders = 3;

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
    {
        if (ActiveOrders.Count >= maxOrders)
            return;

        string order = CreateOrder();
        ActiveOrders.Add(order);

        StartCoroutine(RemoveOrderAfterDelay(order, 30f));
    }

    IEnumerator RemoveOrderAfterDelay(string order, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ActiveOrders.Remove(order);
    }

    string CreateOrder()
    {
        int nFlavors = Random.Range(1, flavors.Count + 1);
        List<string> pool = new List<string>(flavors);
        List<string> result = new List<string>();

        for (int i = 0; i < nFlavors; i++)
        {
            int index = Random.Range(0, pool.Count);
            string ingredient = pool[index];
            pool.RemoveAt(index);
            int quantity = Random.Range(1, 6);
            result.Add(quantity + " x" + ingredient);
        }

        return string.Join(", ", result);
    }
}