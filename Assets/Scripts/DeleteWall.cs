using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour {

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.Destroy();
        }
    }
}
