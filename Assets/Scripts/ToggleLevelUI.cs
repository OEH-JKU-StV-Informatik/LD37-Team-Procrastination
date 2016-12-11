using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLevelUI : MonoBehaviour {

    private bool state = false; 
	// Use this for initialization
	public void ToggleUI () {
        gameObject.SetActive(state);
        state = !state;
	}
}
