using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    public Text levelText;

    public LevelGenerator levelGenerator;

    public int minLevel = 1;
    public int maxLevel = 3;

	// Use this for initialization
	void Start () {
        UpdateLevelText();
	}

    void UpdateLevelText()
    {
        levelText.text = levelGenerator.selectedLevel.ToString();
        FindObjectOfType<Game>().initIngameUI();
    }

    public void NextLevel()
    {
        if (levelGenerator.selectedLevel < maxLevel)
        {
            levelGenerator.selectedLevel++;
        }
        UpdateLevelText();
    }

    public void PreviousLevel()
    {
        if (levelGenerator.selectedLevel > minLevel)
        {
            levelGenerator.selectedLevel--;
        }
        UpdateLevelText();
    }

}
