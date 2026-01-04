using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public NextTextController textController;

    void OnMouseDown()
    {
        textController.NextText();
    }
}
