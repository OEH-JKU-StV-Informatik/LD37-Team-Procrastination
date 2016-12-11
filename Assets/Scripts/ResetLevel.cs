using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour {

    private Game game;

    void Awake()
    {
        game = Resources.FindObjectsOfTypeAll<Game>()[0];
    }

    public void OnResetLevelClick()
    {
        if (game == null)
        {
            game = Resources.FindObjectsOfTypeAll<Game>()[0];
        }

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
