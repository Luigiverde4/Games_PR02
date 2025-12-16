using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string gameSceneName = "SceneRestaurantIn";

    void OnMouseDown()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
