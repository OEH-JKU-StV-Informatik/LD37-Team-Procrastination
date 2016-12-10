using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour {

    private Game game;

    void Start()
    {
        game = FindObjectOfType<Game>();
    }

    public void OnResetLevelClick()
    {
        if (game.IsGameRunning())
        {
            game.StopGame();
            foreach(StartButton b in FindObjectsOfType<StartButton>())
            {
                b.updateButton();
            }
            FindObjectOfType<ChangeUI>().changeUI();
        }

        foreach (DeleteWall wall in FindObjectsOfType<DeleteWall>())
        {
            wall.Destroy();
        }
        FindObjectOfType<AstarPath>().Scan();
    }
}
