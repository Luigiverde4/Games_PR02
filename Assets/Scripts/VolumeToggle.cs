using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VolumeToggleSprite : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;

    [Header("Audio")]
    public AudioSource menuMusic;

    private SpriteRenderer spriteRenderer;
    private bool isMuted = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    void OnMouseDown()
    {
        ToggleVolume();
    }

    void ToggleVolume()
    {
        isMuted = !isMuted;

        menuMusic.mute = isMuted;

        UpdateSprite();
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = isMuted ? volumeOffSprite : volumeOnSprite;
    }
}
