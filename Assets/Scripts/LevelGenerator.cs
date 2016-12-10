using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject playingField;

    public float xSize = 5;
    public float zSize = 5;

    // Use this for initialization
    void Start()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                var newField = playingField.GetComponent<PlayingFieldController>().placeFloor(new Vector3(x, 0, z));
                if (x == 1 && z == 1)
                {
                    playingField.GetComponent<PlayingFieldController>().setStartField(newField);
                }
                if (x == 5 && z == 5)
                {
                    playingField.GetComponent<PlayingFieldController>().setEndField(newField);
                }
            }
        }

        playingField.GetComponent<PlayingFieldController>().placeWall(new Vector3(0, 0, 0), PlayingFieldController.Direction.zplus);
        playingField.GetComponent<PlayingFieldController>().placeWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.xplus);
        playingField.GetComponent<PlayingFieldController>().placeWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.zplus);
    }
}