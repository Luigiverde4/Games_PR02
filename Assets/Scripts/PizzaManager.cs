using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaManager : MonoBehaviour
{
    public string estado = "bola";

    public void setEstado(string nuevo)
    {
        estado = nuevo;
    }

    public string getEstado()
    {
        return estado;
    }
}
