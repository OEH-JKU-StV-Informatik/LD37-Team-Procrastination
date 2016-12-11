using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prebuildLevel_1 : MonoBehaviour
{

    private PlayingFieldController field;

	// Use this for initialization
	void Start ()
	{
	    field = gameObject.GetComponent<PlayingFieldController>();

	    field.PlaceWall(new Vector3(0,0,0), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(0, 0, 1), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(0, 0, 2), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(0, 0, 3), PlayingFieldController.Direction.xplus);

        field.PlaceWall(new Vector3(1, 0, 4), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(1, 0, 3), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(1, 0, 2), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(1, 0, 1), PlayingFieldController.Direction.xplus);

        field.PlaceWall(new Vector3(2, 0, 0), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(2, 0, 1), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(2, 0, 2), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(2, 0, 3), PlayingFieldController.Direction.xplus);

        //field.placeWall(new Vector3(3, 0, 4), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(3, 0, 3), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(3, 0, 2), PlayingFieldController.Direction.xplus);
        field.PlaceWall(new Vector3(3, 0, 1), PlayingFieldController.Direction.xplus);
    }
}
