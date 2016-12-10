using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MonoBehaviour
{
    public GameObject draggableWallPrototype;
    public GameObject playingField;
    public float animationStep = 0.006f;
    public float animationRange = 0.08f;

    private static CornerHighlightController selected;
    private float animationDirection = 1;
    private float animationSize = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // onmousemove -> move wall

    }

    void OnMouseEnter()
    {
//        transform.localScale += Vector3.one * animationRange;
        animationDirection = 1;
    }

    void OnMouseDown()
    {
        selected = this;
        //Instantiate(wallPrototype, position, Quaternion.Euler(0, 270, 0), gameObject.transform)

        //Instantiate(draggableWallPrototype, playingField, playingField.transform);
    }

    void OnMouseUp()
    {
        if (selected != null && selected != this)
        {
            Debug.Log(this.transform.position + " <> " + selected.transform.position);
            // fix wall
        }

        selected = null;
        transform.localScale = Vector3.one;
    }

    void OnMouseExit()
    {
        if (selected != this)
            transform.localScale = Vector3.one;
    }

    void OnMouseOver()
    {
        if (selected == this)
        {
            transform.localScale = Vector3.one * (1+animationRange);
        }
        else
        {
            var change = animationDirection * animationStep;
            animationSize += change;
            transform.localScale = Vector3.one * animationSize;
            if (Mathf.Abs(animationSize - 1) > animationRange)
            {
                animationDirection *= -1;
            }
        }
    }

    void OnMouseDrag()
    {
        transform.localScale = Vector3.one * (1 + animationRange);
    }

    
}
