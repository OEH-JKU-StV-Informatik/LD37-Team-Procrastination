using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject playingField;

    public float xSize = 5;
    public float zSize = 5;

    public int selectedLevel = 1;

    private PlayingFieldController playingFieldController;
    private BuffController buffConftroller;

    private int loadedLevel = -1;

    // Use this for initialization
    void Start()
    {
        playingFieldController = playingField.GetComponent<PlayingFieldController>();
        buffConftroller = gameObject.GetComponent<BuffController>();
    }

    void Update()
    {
        if (loadedLevel != selectedLevel)
        {
            if (loadedLevel > 0)
            {
                unloadLevel();
            }
            loadLevel(selectedLevel);
            loadedLevel = selectedLevel;
        }
    }

    private void unloadLevel()
    {
        Resources.FindObjectsOfTypeAll<ResetLevel>()[0].OnResetLevelClick();
        for (int i = playingFieldController.transform.childCount - 1; i > 0; --i)
        {
            Destroy(playingFieldController.transform.GetChild(i).gameObject);
        }
    }

    public void loadLevel(int levelId)
    {
        generateBaseLevel();

        // Buff sample
        buffConftroller.placeBuffTrigger(new Vector3(1, 0, 1), buffConftroller.slowBuffPrototype);


        switch (levelId)
        {
            case 1:
                playingFieldController.PlaceWall(new Vector3(0, 0, 0), PlayingFieldController.Direction.zplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.zplus);
                buffConftroller.placeBuffTrigger(new Vector3(1, 0, 1), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(3, 0, 2), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(3, 0, 3), buffConftroller.slowBuffPrototype);
                break;
            case 2:
                playingFieldController.PlaceWall(new Vector3(3, 0, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(3, 0, 3), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 3), PlayingFieldController.Direction.xplus);

                buffConftroller.placeBuffTrigger(new Vector3(4, 0, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(3, 0, 4), buffConftroller.slowBuffPrototype);
                break;
            case 3:
                playingFieldController.PlaceWall(new Vector3(3, 0, 3), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.zplus);

                buffConftroller.placeBuffTrigger(new Vector3(3, 0, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(2, 0, 2), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(1, 0, 1), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(2, 0, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(new Vector3(3, 0, 1), buffConftroller.slowBuffPrototype);
                break;
            default:
                break;
        }

        FindObjectOfType<AstarPath>().Scan();
    }

    private void generateBaseLevel()
    {
        float stepSize = playingFieldController.GetStepSize();

        for (int x = 0; x < xSize; x += (int)stepSize)
        {
            for (int z = 0; z < zSize; z += (int)stepSize)
            {
                var newField = playingFieldController.PlaceFloor(new Vector3(x, 0, z));

                // -- Start / End Fields
                if (x == 0 && z == 0)
                {
                    playingFieldController.SetStartField(newField);
                }
                if (x == xSize - 1 && z == zSize - 1)
                {
                    playingFieldController.SetEndField(newField);
                }

                // --Outer walls--
                if (x == 0)
                {
                    playingFieldController.PlaceWall(new Vector3(x - 1, 0, z), PlayingFieldController.Direction.xplus);
                }
                if (x == xSize - 1)
                {
                    playingFieldController.PlaceWall(new Vector3(x, 0, z), PlayingFieldController.Direction.xplus);
                }
                if (z == 0)
                {
                    playingFieldController.PlaceWall(new Vector3(x, 0, z - 1), PlayingFieldController.Direction.zplus);
                }
                if (z == zSize - 1)
                {
                    playingFieldController.PlaceWall(new Vector3(x, 0, z), PlayingFieldController.Direction.zplus);
                }


                // -- Corner Highlighter
                playingFieldController.PlaceCornerHighlight(new Vector3(x - stepSize / 2, 0, z - stepSize / 2));
                if (z == zSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(new Vector3(x - stepSize / 2, 0, z + stepSize / 2));
                }
                if (x == xSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(new Vector3(x + stepSize / 2, 0, z - stepSize / 2));
                }
                if (z == zSize - 1 && x == xSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(new Vector3(x + stepSize / 2, 0, z + stepSize / 2));
                }
            }
        }
    }
}
