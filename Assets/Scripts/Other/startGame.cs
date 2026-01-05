using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string gameSceneName;
    public bool isDifferentButton;
    void OnMouseDown()
    {
        if (GameModeManager.Instance.isFirstTime && !isDifferentButton)
        {
            // Ir al tutorial (isFirstTime se cambia a false al terminar el tutorial)
            SceneManager.LoadScene("SceneRestaurantIn");
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
