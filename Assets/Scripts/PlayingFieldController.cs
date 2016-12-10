using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldController : MonoBehaviour
{
    private GameObject startField;
    private GameObject endField;

    public enum DIRECTION
    {
        xplus, xminus, zplus, zminus
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void setStartField(GameObject startField)
    {
        this.startField = startField;
    }

    public Vector3 getStartPosition()
    {
        return startField.transform.position;
    }

    public void setEndField(GameObject endField)
    {
        this.endField = endField;
    }

    // Enemy needs what movement is possible
    public bool canGo(Vector3 position, DIRECTION direction)
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
}
