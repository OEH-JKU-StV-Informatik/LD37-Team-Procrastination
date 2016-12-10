using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject fieldPrototype;
    public GameObject wallPrototype;
    public GameObject playingField;

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

        Instantiate(wallPrototype, new Vector3(0, 0, 0), Quaternion.identity, playingField.transform);
        Instantiate(wallPrototype, new Vector3(1, 0, 1), Quaternion.Euler(0,90,0), playingField.transform);
        Instantiate(wallPrototype, new Vector3(1, 0, 1), Quaternion.identity, playingField.transform);
    }
}