using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour
{
    public float defaultTextDisplayTime = 5.0f;
    public Text errorTextField;
    public Color successMessageColor;
    public Color errorMessageColor;

    private float timeLeft = 0.0f;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0.0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void DisplayMessage(string message, float seconds, Color color)
    {
        errorTextField.color = color;
        errorTextField.text = message;
        timeLeft = seconds;
        gameObject.SetActive(true);
    }

    public void DisplayError(string message)
    {
        DisplayError(message, defaultTextDisplayTime);
    }

    public void DisplayError(string message, float seconds)
    {
        DisplayMessage(message, seconds, errorMessageColor);
    }

    public void DisplaySuccess(string message, float seconds)
    {
        DisplayMessage(message, seconds, successMessageColor);
    }
}
