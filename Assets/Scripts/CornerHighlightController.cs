using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.2F, 0.2F, 0.2F);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnMouseOver()
    {

    }
}
