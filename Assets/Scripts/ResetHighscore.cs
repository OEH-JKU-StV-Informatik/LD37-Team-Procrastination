using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHighscore : MonoBehaviour {

    public void resetHighscore()
    {
        PlayerPrefs.SetFloat("Highscore_" + SceneManager.GetActiveScene().name, 0f);
    }
}
