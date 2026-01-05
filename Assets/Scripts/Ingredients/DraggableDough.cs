using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableDough : MonoBehaviour
{
    public Sprite extendedDough;
    public Sprite coockedPizza;
    public Sprite burntPizza;

    public float dragZ = -2f;
    private bool isDragging = false;
    private Vector3 offset;
    private static GameObject doughPlaced;

    public float cookingTime = 8f;
    public float burningTime = 5f;
    public int furnaceCapacity = 1;

    private static int furnaceCounter = 0;
    private Coroutine furnaceRoutine;
    private bool inFurnace = false;

    void OnMouseDown()
    {
        IniciarArrastre();
    }

    public void StartDragging()
    {
        IniciarArrastre();
    }

    void IniciarArrastre()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;

        offset = transform.position - mousePos;
        isDragging = true;
    }

    void enterFurnace()
    {
        if(inFurnace) return;
        if (furnaceCounter >= furnaceCapacity) return;

        PizzaManager pm = GetComponent<PizzaManager>();
        inFurnace = true;
        furnaceCounter += 1;
        // Debug.Log("Pizza metida al horno. Estado: " + pm.estado);

        if (pm.estado == "queso")
        {
            furnaceRoutine = StartCoroutine(CookRoutine());
        }
        else if (pm.estado == "cocinado")
        {
            furnaceRoutine = StartCoroutine(BurnRoutine());
        }
    }

    void exitFurnace()
    {
        if (!inFurnace) return;

        inFurnace = false;
        furnaceCounter = Mathf.Max(0, furnaceCounter - 1);
        if (furnaceRoutine != null)
        {
            StopCoroutine(furnaceRoutine);
            furnaceRoutine = null;
        }
    }

    IEnumerator CookRoutine()
    {
        // Debug.Log("Comenzando coccion...");
        yield return new WaitForSeconds(cookingTime);

        if (!inFurnace) yield break;

        PizzaManager pm = GetComponent<PizzaManager>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = coockedPizza;
        pm.setEstado("cocinado");
        // Debug.Log("Pizza cocinada!");
        furnaceRoutine = StartCoroutine(BurnRoutine());
    }

    IEnumerator BurnRoutine()
    {
        // Debug.Log("Comenzando quemado...");
        yield return new WaitForSeconds(burningTime);

        if (!inFurnace) yield break;

        PizzaManager pm = GetComponent<PizzaManager>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = burntPizza;
        pm.setEstado("quemado");
        // Debug.Log("Pizza quemada!");
    }

    void pizzaPlacing()
    {
        isDragging = false;

        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        Collider2D pizzaZone = null;
        Collider2D furnaceZone = null;
        Collider2D serveZone = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("PizzaZone"))
            {
                pizzaZone = hit;
            }
            else if (hit.CompareTag("Furnace"))
            {
                furnaceZone = hit;
            }
            else if (hit.CompareTag("ServeZone"))
            {
                serveZone = hit;
            }
        }

        if (furnaceZone == null && inFurnace)
        {
            exitFurnace();
        }

        PizzaManager pm = GetComponent<PizzaManager>();
        if (pm == null) return;

        if (pizzaZone != null)
        {
            Vector3 centerPos = pizzaZone.bounds.center;
            centerPos.z = dragZ;
            transform.position = centerPos;

            if (pm.estado == "bola")
            {
                if (doughPlaced != null && doughPlaced != gameObject)
                {
                    Destroy(gameObject);
                    return;
                }

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = extendedDough;
                }
                pm.setEstado("extendido");
                doughPlaced = gameObject;
                // Debug.Log("Masa extendida en la zona de pizza");
            }
            return;
        }

        if (furnaceZone != null)
        {
            if (pm.estado != "queso" && pm.estado != "cocinado")
            {
                Destroy(gameObject);
                return;
            }

            enterFurnace();
            return;
        }

        if (inFurnace)
        {
            exitFurnace();
            return;
        }

        if (serveZone != null)
        {
            if (pm.estado == "cocinado")
            {
                string pizzaOrder = GeneratePizzaOrder(pm);
                if (FlavorGenerator.ActiveOrders.Contains(pizzaOrder))
                {
                    FlavorGenerator.ActiveOrders.Remove(pizzaOrder);
                    int moneyEarned = CalculateMoney(pm);
                    MoneyManager.Instance.sumarDinero(moneyEarned);
                    
                    // Agregar tiempo en modo Cronometro basado en complejidad
                    if (GameModeManager.Instance != null && GameModeManager.Instance.currentMode == GameModeManager.GameMode.Cronometro)
                    {
                        // Calcular complejidad (base 2 + toppings)
                        int complexity = 2; // Tomato Sauce + Queso base
                        complexity += pm.tomatoSauce + pm.queso + pm.pepperoni + pm.mushroom + pm.bacon + pm.egg + pm.olive + pm.onion + pm.pineapple + pm.pepper + pm.shrimp + pm.cheese + pm.anchovies + pm.caper;
                        
                        float timeBonus = complexity * 5f; // 5 segundos por ingrediente
                        
                        TimerDisplay timerDisplay = FindObjectOfType<TimerDisplay>();
                        if (timerDisplay != null)
                        {
                            timerDisplay.AddTime(timeBonus);
                            Debug.Log("Pizza correcta! +$" + moneyEarned + " y +" + timeBonus + "s");
                        }
                    }
                    else
                    {
                        Debug.Log("Pizza correcta servida! Ganaste $" + moneyEarned);
                    }
                }
                else
                {
                    Debug.Log("Pizza no coincide con ningun pedido");
                }
                Destroy(gameObject);
            }
            else if (pm.estado == "quemado")
            {
                Debug.Log("Muy mal, se te ha quemado la pizza");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("La pizza no está lista");
            }
            return;
        }
        
        Destroy(gameObject);
    }

    string GeneratePizzaOrder(PizzaManager pm)
    {
        List<string> ingredients = new List<string>();
        
        // Agregar base si la pizza está cocinada (siempre tiene tomate y queso)
        if (pm.estado == "cocinado" || pm.estado == "quemado")
        {
            ingredients.Add("1 xTomato Sauce");
            ingredients.Add("1 xQueso");
        }
        
        // Agregar toppings adicionales
        if (pm.tomatoSauce > 0) ingredients.Add(pm.tomatoSauce + " xTomato Sauce");
        if (pm.queso > 0) ingredients.Add(pm.queso + " xQueso");
        if (pm.pepperoni > 0) ingredients.Add(pm.pepperoni + " xPepperoni");
        if (pm.mushroom > 0) ingredients.Add(pm.mushroom + " xMushroom");
        if (pm.bacon > 0) ingredients.Add(pm.bacon + " xBacon");
        if (pm.egg > 0) ingredients.Add(pm.egg + " xEgg");
        if (pm.olive > 0) ingredients.Add(pm.olive + " xOlive");
        if (pm.onion > 0) ingredients.Add(pm.onion + " xOnion");
        if (pm.pineapple > 0) ingredients.Add(pm.pineapple + " xPineapple");
        if (pm.pepper > 0) ingredients.Add(pm.pepper + " xPepper");
        if (pm.shrimp > 0) ingredients.Add(pm.shrimp + " xShrimp");
        if (pm.cheese > 0) ingredients.Add(pm.cheese + " xCheese");
        if (pm.anchovies > 0) ingredients.Add(pm.anchovies + " xAnchovies");
        if (pm.caper > 0) ingredients.Add(pm.caper + " xCaper");

        ingredients.Sort();
        return string.Join(", ", ingredients);
    }

    int CalculateMoney(PizzaManager pm)
    {
        int totalUnits = 0;
        
        // Incluir base si está cocinada
        if (pm.estado == "cocinado" || pm.estado == "quemado")
        {
            totalUnits += 1 + 1; // Tomato Sauce + Queso base
        }
        
        // Sumar toppings adicionales
        totalUnits += pm.tomatoSauce + pm.queso + pm.pepperoni + pm.mushroom + pm.bacon + pm.egg + pm.olive + pm.onion + pm.pineapple + pm.pepper + pm.shrimp + pm.cheese + pm.anchovies + pm.caper;
        return totalUnits * 5; // 5 dollars per unit
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = dragZ;
        transform.position = mousePos + offset;

        if (Input.GetMouseButtonUp(0))
        {
            pizzaPlacing();
        }
    }
}