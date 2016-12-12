using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
	    gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 60 * Time.deltaTime,0);
	}
}
