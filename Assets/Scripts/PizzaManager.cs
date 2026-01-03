using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaManager : MonoBehaviour
{
    public string estado = "bola";

    public int pepperoni = 0;
    public int mushroom = 0;
    public int bacon = 0;
    public int egg = 0;
    public int olive = 0;
    public int onion = 0;
    public int pineapple = 0;
    public int pepper = 0;
    public int shrimp = 0;
    public int cheese = 0;
    public int anchovies = 0;
    public int caper = 0;

    public void setEstado(string nuevo)
    {
        estado = nuevo;
    }

    public string getEstado()
    {
        return estado;
    }
}
