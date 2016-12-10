using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour {

    private Game game;
    private StartButton startButton;

    void Start()
    {
        game = FindObjectOfType<Game>();
        startButton = FindObjectOfType<StartButton>();
    }

    public void OnResetLevelClick()
    {
        if (game.IsGameRunning())
        {
            startButton.StartClicked();
        }

        foreach (DeleteWall wall in FindObjectsOfType<DeleteWall>())
        {
            wall.Destroy();
        }
        FindObjectOfType<AstarPath>().Scan();
    }
}
