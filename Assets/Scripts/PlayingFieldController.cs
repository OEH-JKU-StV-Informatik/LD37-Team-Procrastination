using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldController : MonoBehaviour
{
    public static int maxWalls;
    public static int currentWalls;

    public GameObject wallPrototype;
    public GameObject fieldPrototype;
    public GameObject cornerHighlightPrototype;
    public Material endFieldMaterial;
    public PathHelper pathHelper;

    private GameObject startField;
    private GameObject endField;
    private float stepSize = 10.0f;

    public enum Direction
    {
        xplus, xminus, zplus, zminus
    }

    public void SetStartField(GameObject startField)
    {
        this.startField = startField;
    }

    public Vector3 GetStartPosition()
    {
        return startField.transform.position;
    }

    public float GetStepSize()
    {
        return stepSize;
    }

    public GameObject GetEndField()
    {
        return endField;
    }

    public void SetEndField(GameObject endField)
    {
        this.endField = endField;
        SetFieldMaterial(endField, endFieldMaterial);
    }

    // Enemy needs isEndCondition
    public bool IsEndCondition(Vector3 position)
    {
        return IsInside(position, endField);
    }

    private bool IsInside(Vector3 position, GameObject comparator)
    {
        //ToDo: implement
        return false;
    }

    internal GameObject PlaceFloor(Vector3 position)
    {
        return Instantiate(fieldPrototype, position, Quaternion.identity, gameObject.transform);
    }

    public GameObject PlaceWall(Vector3 position, Direction direction)
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

    public GameObject PlaceCornerHighlight(Vector3 position)
    {
        GameObject newHighlight = Instantiate(cornerHighlightPrototype, position, Quaternion.identity, gameObject.transform);
        newHighlight.GetComponent<CornerHighlightController>().playingFieldController = this;
        return newHighlight;
    }

    public bool IsValidLevel()
    {
        return pathHelper.isReachableFrom(startField.transform.position, endField.transform.position);
    }

    private void SetFieldMaterial(GameObject field, Material material)
    {
        field.GetComponent<FieldController>().quad.GetComponent<Renderer>().material = material;
        //endField.GetComponent<Renderer>().material = endFieldMaterial;
    }
}
