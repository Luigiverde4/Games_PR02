using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScale : MonoBehaviour
{
    public float amplitude = 0.03f;   
    public float speed = 2.3f;      

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale; 
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localScale = startScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);
    }
}