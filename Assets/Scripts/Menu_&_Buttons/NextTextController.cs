using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextTextController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshPro textMeshPro;

    [Header("Contenuto")]
    [TextArea(2, 5)]
    public string[] texts;

    public AudioClip[] audioClips;

    [Header("Audio")]
    public AudioSource audioSource;

    private int currentIndex = 0;

    public int changeAfterClicks = 6;   
    public string sceneToLoad = "SceneClient";

    private int clickCount = 0;

    void Start()
    {
        ShowCurrent();
    }

    public void NextText()
    {
        clickCount++;

        // UnityEngine.Debug.Log("Scena precedente: " + SceneTracker.lastScene);
        if(SceneTracker.lastScene == "SceneClient")
        {
            SceneManager.LoadScene(sceneToLoad);
            return;
        }

        if (clickCount == changeAfterClicks)
        {
            // Tutorial completado, marcar que ya no es la primera vez
            if (GameModeManager.Instance != null)
            {
                GameModeManager.Instance.isFirstTime = false;
                Debug.Log("Tutorial finished! isFirstTime set to: " + GameModeManager.Instance.isFirstTime);
            }
            else
            {
                Debug.LogError("GameModeManager.Instance is null when finishing tutorial!");
            }
            
            SceneManager.LoadScene(sceneToLoad);
            return;
        }
        currentIndex++;
        if ((currentIndex >= texts.Length) && SceneManager.GetActiveScene().name == "SceneRestaurantIn")
        {
            SceneManager.LoadScene("SceneDoingPizza");
            return;
        } 
        else if (currentIndex >= texts.Length)
        {
            currentIndex = texts.Length - 1; // Stay on the last text if no scene change
        }

        ShowCurrent();
    }

    void ShowCurrent()
    {
        textMeshPro.text = texts[currentIndex];
        if (audioSource != null && audioClips.Length > currentIndex)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
        }
    }
}