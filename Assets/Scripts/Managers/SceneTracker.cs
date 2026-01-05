using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    // Variabile statica che rimane tra le scene
    public static string lastScene = "";
    void Start()
    {
        SaveCurrentScene();
    }   

    public static void SaveCurrentScene()
    {
        lastScene = SceneManager.GetActiveScene().name;
        // UnityEngine.Debug.Log("Scena attuale: " + lastScene);

    }
}
