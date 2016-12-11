using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldController : MonoBehaviour
{
    private GameObject startField;
    private GameObject endField;
    public GameObject wallPrototype;
    public GameObject fieldPrototype;
    public GameObject cornerHighlightPrototype;


    public PathHelper pathHelper;

    private float stepSize = 1.0f;

    public enum Direction
    {
        xplus, xminus, zplus, zminus
    }

    public GameObject getEndField()
    {
        return endField;
    }

    public void setStartField(GameObject startField)
    {
        this.startField = startField;
    }

    public Vector3 getStartPosition()
    {
        return startField.transform.position;
    }

    public float getStepSize()
    {
        return stepSize;
    }

    public void setEndField(GameObject endField)
    {
        this.endField = endField;
    }

    // Enemy needs what movement is possible
    public bool canGo(Vector3 position, Direction direction)
    {
        // (x+, x-, z+, z-)
        //ToDo: implement
        return true;
    }

    // Enemy needs isEndCondition
    public bool isEndCondition(Vector3 position)
    {
        return isInside(position, endField);
    }

    private bool isInside(Vector3 position, GameObject comparator)
    {
        //ToDo: implement
        return false;
    }

    internal GameObject placeFloor(Vector3 position)
    {
        return Instantiate(fieldPrototype, position, Quaternion.identity, gameObject.transform);
    }

    public GameObject placeWall(Vector3 position, Direction direction)
    {
        GameObject wall;
        switch (direction)
        {
            case Direction.xminus:
                wall = Instantiate(wallPrototype, position, Quaternion.Euler(0, 270, 0), gameObject.transform);
                break;
            case Direction.xplus:
                wall = Instantiate(wallPrototype, position, Quaternion.Euler(0, 90, 0), gameObject.transform);
                break;
            case Direction.zminus:
                wall = Instantiate(wallPrototype, position, Quaternion.Euler(0, 180, 0), gameObject.transform);
                break;
            case Direction.zplus:
                wall = Instantiate(wallPrototype, position, Quaternion.Euler(0, 0, 0), gameObject.transform);
                break;
            default:
                throw new Exception("undefined state!");
        }
        return wall;
    }

    public GameObject placeCornerHighlight(Vector3 position)
    {
        GameObject newHighlight = Instantiate(cornerHighlightPrototype, position, Quaternion.identity, gameObject.transform);
        newHighlight.GetComponent<CornerHighlightController>().playingField = this;
        return newHighlight;
    }

    public bool isValidLevel()
    {
        return pathHelper.isReachableFrom(startField.transform.position, endField.transform.position);
    }
}
