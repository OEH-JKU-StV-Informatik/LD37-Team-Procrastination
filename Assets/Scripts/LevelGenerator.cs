using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject fieldPrototype;
    public GameObject playingField;

    //private float fieldSize = 1f;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                var newField = Instantiate(fieldPrototype, new Vector3(i, 0, j), Quaternion.identity, playingField.transform);
                if (i == 1 && j == 1)
                {
                    playingField.GetComponent<PlayingFieldController>().setStartField(newField);
                }
                if (i == 5 && j == 5)
                {
                    playingField.GetComponent<PlayingFieldController>().setEndField(newField);
                }
            }
        }
    }
}