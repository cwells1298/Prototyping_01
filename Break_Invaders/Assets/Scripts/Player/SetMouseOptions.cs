using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMouseOptions : MonoBehaviour
{
    public void SetMouseInvertedtext(GameObject text)
    {
        MouseOptions.mouseInvertedText = text.GetComponent<TextMeshProUGUI>();

        if (MouseOptions.mouseInverted)
        {
            MouseOptions.mouseInvertedText.text = "Inverted Mouse : True";
        }
        else
        {
            MouseOptions.mouseInvertedText.text = "Inverted Mouse : False";
        }
    }

    public void OnToggle()
    {
        MouseOptions.mouseInverted = !MouseOptions.mouseInverted;

        if (MouseOptions.mouseInvertedText != null)
        {
            if (MouseOptions.mouseInverted)
            {
                MouseOptions.mouseInvertedText.text = "Inverted Mouse : True";
            }
            else
            {
                MouseOptions.mouseInvertedText.text = "Inverted Mouse : False";
            }
        }
    }
}
