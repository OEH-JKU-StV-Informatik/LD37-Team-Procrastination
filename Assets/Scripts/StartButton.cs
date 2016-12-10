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
        if (text.text == "Start")
        {
            text.text = "Stop";
            game.StartGame();
        } else {
            text.text = "Start";
            game.StopGame();
        }
    }
}
    