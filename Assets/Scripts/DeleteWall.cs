using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    public void DestroyAndRecalculate()
    {
        this.Destroy();
        FindObjectOfType<AstarPath>().Scan();
    }
}
