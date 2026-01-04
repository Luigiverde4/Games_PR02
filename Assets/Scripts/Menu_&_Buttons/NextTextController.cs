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

    void Start()
    {
        ShowCurrent();
    }

    public void NextText()
    {
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