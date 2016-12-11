using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHighscore : MonoBehaviour {

    public void resetHighscore()
    {
        PlayerPrefs.SetFloat("Highscore_" + FindObjectOfType<LevelGenerator>().selectedLevel, 0f);
        FindObjectOfType<Game>().initIngameUI();
    }
}
