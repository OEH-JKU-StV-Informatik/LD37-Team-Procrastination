using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MonoBehaviour
{
    public GameObject draggableWallPrototype;
    public PlayingFieldController playingField;
    public float animationStep;
    public float animationRange;

    public Material standard;
    public Material valid;
    public Material invalid;

    private bool dragging = false;
    private bool validWall = false;
    private static CornerHighlightController selected;
    private float animationDirection = 1;
    private float animationSize = 1;
    private GameObject newWall;
    private Renderer[] renderers;
    private ErrorText errorText;

    private bool animate = false;

    // Use this for initialization
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        errorText = Resources.FindObjectsOfTypeAll<ErrorText>()[0];
        SetMaterial(standard);
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging && !newWall)
        {
            resetSelections();
        }

        if (dragging)
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 1000, 1 << LayerMask.NameToLayer("Floor")))
            {
                newWall.transform.LookAt(hitInfo.point);
                newWall.transform.localScale = new Vector3(1, 1, (hitInfo.point - transform.position).magnitude);
            }

            if (selected && this != selected)
            {
                validWall = (selected.transform.position - transform.position).magnitude <= 1.1;
                selected.SetMaterial(validWall ? valid : invalid);
            }
            else
            {
                validWall = false;
            }

        }

        //ANIMATION
        if (animate)
        {
            var change = animationDirection * animationStep * Time.deltaTime;
            animationSize += change;
            transform.localScale = Vector3.one * animationSize;
            if (Mathf.Abs(animationSize - 1) > animationRange)
            {
                animationDirection *= -1;
            }
        }
    }

    void OnMouseEnter()
    {
        selected = this;
        startAnimation();
    }

    void OnMouseExit()
    {
        if (selected)
        {
            selected.SetMaterial(standard);
        }
        selected = null;
        if (!dragging)
            stopAnimation(false);
    }

    void OnMouseUp()
    {
        if (dragging)
        {
            if (selected != this && validWall)
            {
                newWall.transform.LookAt(selected.transform.position);
                newWall.transform.localScale = Vector3.one;

                // recalculate pathing
                FindObjectOfType<AstarPath>().Scan();
                if (!playingField.isValidLevel())
                {
                    Destroy(newWall);
                    FindObjectOfType<AstarPath>().Scan();
                    errorText.DisplayError("Path to the exit cannot be fully obstructed");
                }
            }
            else
            {
                Destroy(newWall);
            }
        }

        resetSelections();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;

            newWall = Instantiate(draggableWallPrototype);
            newWall.transform.position = transform.position;
            newWall.transform.localScale = Vector3.zero;
            newWall.GetComponentInChildren<MeshRenderer>().material.color = new Color(0, 0, 0.85f);
        }
        //if (dragging)
        //{
        //}
    }

    void OnMouseDrag()
    {
        stopAnimation(true);
    }

    private void SetMaterial(Material material)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }

    private void resetSelections()
    {
        if (selected)
        {
            selected.SetMaterial(standard);
        }
        selected = null;
        dragging = false;
        transform.localScale = Vector3.one;
        validWall = false;
    }

    private void startAnimation()
    {
        animationDirection = 1;
        animationSize = 1;
        animate = true;
    }

    private void stopAnimation(bool large)
    {
        animate = false;
        if (large)
        {
            transform.localScale = Vector3.one * (1 + animationRange);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

}
