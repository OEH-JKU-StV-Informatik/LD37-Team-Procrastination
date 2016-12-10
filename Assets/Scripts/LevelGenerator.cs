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
        var playingFieldController = playingField.GetComponent<PlayingFieldController>();
        float stepSize = playingFieldController.getStepSize();

        for (int x = 0; x < xSize; x += (int)stepSize)
        {
            for (int z = 0; z < zSize; z += (int)stepSize)
            {
                var newField = playingFieldController.placeFloor(new Vector3(x, 0, z));

                // -- Start / End Fields
                if (x == 0 && z == 0)
                {
                    playingFieldController.setStartField(newField);
                }
                if (x == xSize - 1 && z == zSize - 1)
                {
                    playingFieldController.setEndField(newField);
                }

                // --Outer walls--
                if (x == 0)
                {
                    playingFieldController.placeWall(new Vector3(x - 1, 0, z), PlayingFieldController.Direction.xplus);
                }
                if (x == xSize - 1)
                {
                    playingFieldController.placeWall(new Vector3(x, 0, z), PlayingFieldController.Direction.xplus);
                }
                if (z == 0)
                {
                    playingFieldController.placeWall(new Vector3(x, 0, z - 1), PlayingFieldController.Direction.zplus);
                }
                if (z == zSize - 1)
                {
                    playingFieldController.placeWall(new Vector3(x, 0, z), PlayingFieldController.Direction.zplus);
                }


                // -- Corner Highlighter
                playingFieldController.placeCornerHighlight(new Vector3(x - stepSize / 2, 0, z - stepSize / 2));
                if (z == zSize - 1)
                {
                    playingFieldController.placeCornerHighlight(new Vector3(x - stepSize / 2, 0, z + stepSize / 2));
                }
                if (x == xSize - 1)
                {
                    playingFieldController.placeCornerHighlight(new Vector3(x + stepSize / 2, 0, z - stepSize / 2));
                }
                if (z == zSize - 1 && x == xSize - 1)
                {
                    playingFieldController.placeCornerHighlight(new Vector3(x + stepSize / 2, 0, z + stepSize / 2));
                }
            }
        }

        playingFieldController.placeWall(new Vector3(0, 0, 0), PlayingFieldController.Direction.zplus);
        playingFieldController.placeWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.xplus);
        playingFieldController.placeWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.zplus);

        FindObjectOfType<AstarPath>().Scan();
    }
}
