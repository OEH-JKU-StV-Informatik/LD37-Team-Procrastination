using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
        PlayingFieldController.currentWalls--;
    }

    public void DestroyAndRecalculate()
    {
        this.Destroy();
        FindObjectOfType<AstarPath>().Scan();
    }
}
