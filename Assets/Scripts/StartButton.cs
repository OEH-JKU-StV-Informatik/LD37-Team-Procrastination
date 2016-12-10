using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    public Game game;
    [SerializeField]
    private Text text;

    public void StartClicked()
    {
        if (game.IsGameRunning())
        {
            game.StopGame();
        } else
        {
            game.StartGame();
        }
        updateButton();
    }

    public void updateButton()
    {
        text.text = game.IsGameRunning() ? "Stop" : "Start";
    }
}
    