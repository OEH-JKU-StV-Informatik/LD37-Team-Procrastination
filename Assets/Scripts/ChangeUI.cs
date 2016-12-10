using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ChangeUI : MonoBehaviour
{
    public GameObject ingameUI;
    public GameObject menuUI;
    public GameObject mainCamera;
    public GameObject menuCamera;
    
    private static bool displayMenu = false;

	// Use this for initialization
	void Start () {
		ingameUI.SetActive(!displayMenu);
        menuUI.SetActive(displayMenu);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Cancel"))
	    {
	        changeUI();
	    }
    }

    public void changeUI()
    {
        //Time.timeScale = displayMenu ? 1.0f : 0.0f; //cannot stop time properly
        if (!FindObjectOfType<Game>().IsGameRunning())
        {
            toggleInGameUI();
        }
        displayMenu = !displayMenu;
        menuUI.SetActive(displayMenu);
        if (displayMenu)
        {
            mainCamera.SetActive(!displayMenu);
            menuCamera.SetActive(displayMenu);
        }
        else
        {
            menuCamera.SetActive(displayMenu);
            mainCamera.SetActive(!displayMenu);
        }
    }

    public void toggleInGameUI()
    {
        ingameUI.SetActive(displayMenu);
    }
}
