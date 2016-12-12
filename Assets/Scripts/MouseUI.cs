using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    public GameObject gameLogic;
    private Game gameController;

    void Start()
    {
        gameController = gameLogic.GetComponent<Game>();
    }

    void Update()
    {
        if (!gameController.IsGameRunning())
        {
            RaycastHit[] hitInfos;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hitInfos = Physics.RaycastAll(ray);

            foreach (RaycastHit hitInfo in hitInfos)
            {
                MouseUIObject mouseObject = hitInfo.transform.gameObject.GetComponent<MouseUIObject>();
                if (mouseObject != null)
                    mouseObject.OnMouseOver();

                DeleteWall deleteWall = hitInfo.transform.gameObject.GetComponent<DeleteWall>();
                if (deleteWall != null && Input.GetMouseButtonDown(1))
                    deleteWall.DestroyAndRecalculate();

            }
        }
    }
}
