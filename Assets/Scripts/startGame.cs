using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string gameSceneName;
    void OnMouseDown()
    {
        if (GameModeManager.Instance.isFirstTime)
        {
            Debug.Log("Primera vez - isFirstTime a false");
            GameModeManager.Instance.isFirstTime = false;
            SceneManager.LoadScene("SceneRestaurantIn");
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
