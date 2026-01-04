using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject tutorial; // trascina l'oggetto Tutorial nel Inspector

    void Start()
    {
        // Stampa in console la scena da cui provieni
        UnityEngine.Debug.Log("Scena precedente: " + SceneTracker.lastScene);

        // Controlla se la scena precedente è "SceneClient"
        if(SceneTracker.lastScene == "SceneClient")
        {
            if (tutorial != null)
                tutorial.SetActive(false);
        }
        else
        {
            if (tutorial != null)
                tutorial.SetActive(true);
        }
    }
}