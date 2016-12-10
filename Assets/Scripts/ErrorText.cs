﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorText : MonoBehaviour {

    public float defaultTextDisplayTime = 5.0f;
    public Text errorTextField;

    private float timeLeft = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0f)
        {
            gameObject.SetActive(false);
        }
	}

    public void DisplayError(string message)
    {
        DisplayError(message, defaultTextDisplayTime);
    }

    public void DisplayError(string message, float seconds)
    {
        errorTextField.text = message;
        timeLeft = seconds;
        gameObject.SetActive(true);
    }
}
