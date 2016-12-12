using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject playingField;

    public float xSize = 5;
    public float zSize = 5;

    public int selectedLevel = 1;
    public float selectedLevelTimeWin = 10f;

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

        switch (levelId)
        {
            case 1:
                PlayingFieldController.maxWalls = 10;
                PlayingFieldController.currentWalls = 0;
                playingFieldController.PlaceWall(GetScaledVector(0, 0), PlayingFieldController.Direction.zplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 1), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 1), PlayingFieldController.Direction.zplus);
                buffConftroller.placeBuffTrigger(GetScaledVector(1, 1), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(3, 2), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(3, 3), buffConftroller.slowBuffPrototype);
                break;
            case 2:
                PlayingFieldController.maxWalls = 10;
                PlayingFieldController.currentWalls = 0;
                playingFieldController.PlaceWall(GetScaledVector(3, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(3, 3), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 3), PlayingFieldController.Direction.xplus);
                buffConftroller.placeBuffTrigger(GetScaledVector(4, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(3, 4), buffConftroller.slowBuffPrototype);
                break;
            case 3:
                PlayingFieldController.maxWalls = 10;
                PlayingFieldController.currentWalls = 0;
                playingFieldController.PlaceWall(GetScaledVector(3, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 2), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 1), PlayingFieldController.Direction.xplus);
                playingFieldController.PlaceWall(GetScaledVector(1, 1), PlayingFieldController.Direction.zplus);

                buffConftroller.placeBuffTrigger(GetScaledVector(3, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(2, 2), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(1, 1), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(2, 3), buffConftroller.slowBuffPrototype);
                buffConftroller.placeBuffTrigger(GetScaledVector(3, 1), buffConftroller.slowBuffPrototype);
                break;
            default:
                break;
        }

        FindObjectOfType<AstarPath>().Scan();
    }

    public void generateCorners()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                playingFieldController.PlaceCornerHighlight(GetScaledVector(x - 0.5f, z - 0.5f));
                if (z == zSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(GetScaledVector(x - 0.5f, z + 0.5f));
                }
                if (x == xSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(GetScaledVector(x + 0.5f, z - 0.5f));
                }
                if (z == zSize - 1 && x == xSize - 1)
                {
                    playingFieldController.PlaceCornerHighlight(GetScaledVector(x + 0.5f, z + 0.5f));
                }
            }
        }
    }

    private void generateBaseLevel()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                var newField = playingFieldController.PlaceFloor(GetScaledVector(x, z));

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
                    playingFieldController.PlaceWall(GetScaledVector(x - 1, z), PlayingFieldController.Direction.xplus);
                }
                if (x == xSize - 1)
                {
                    playingFieldController.PlaceWall(GetScaledVector(x, z), PlayingFieldController.Direction.xplus);
                }
                if (z == 0)
                {
                    playingFieldController.PlaceWall(GetScaledVector(x, z - 1), PlayingFieldController.Direction.zplus);
                }
                if (z == zSize - 1)
                {
                    playingFieldController.PlaceWall(GetScaledVector(x, z), PlayingFieldController.Direction.zplus);
                }
            }
        }
        generateCorners();
    }

    private Vector3 GetScaledVector(float x, float z)
    {
        return new Vector3(x * playingFieldController.GetStepSize(), 0, z * playingFieldController.GetStepSize());
    }
}
