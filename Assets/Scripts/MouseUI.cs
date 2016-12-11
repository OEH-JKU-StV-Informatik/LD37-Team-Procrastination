using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUI : MonoBehaviour
{
    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 1000, 1 << LayerMask.NameToLayer("MouseUI")))
        {
            hitInfo.transform.gameObject.GetComponent<MouseUIObject>().OnMouseOver();
        }
    }
}
