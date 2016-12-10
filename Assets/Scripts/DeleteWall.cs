using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour {

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
