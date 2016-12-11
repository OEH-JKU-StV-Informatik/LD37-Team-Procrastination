using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public int level = 1;

	// Use this for initialization
	public void loadScene () {
		SceneManager.LoadScene("Levels/Level_"+level);
	}
}
