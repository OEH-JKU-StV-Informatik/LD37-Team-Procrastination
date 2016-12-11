using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MouseUIObject
{
    public GameObject draggableWallPrototype;
    public PlayingFieldController playingField;
    public float animationStep;
    public float animationRange;

    public Material standard;
    public Material valid;
    public Material invalid;

    private static CornerHighlightController selected = null;
    private bool animate = false;
    private bool dragging = false;
    private bool validWall = false;
    private float animationDirection = 1;
    private float animationSize = 1;
    private GameObject newWall = null;
    private Renderer[] renderers;
    private ErrorText errorText;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        errorText = Resources.FindObjectsOfTypeAll<ErrorText>()[0];
        SetMaterial(standard);
    }

    public override void Update()
    {
        if (mouseOver && mouseEnter)
        {
            Debug.Log("Mouse Enter");
            selected = this;
            StartAnimation();
        }
        if (mouseExit)
        {
            Debug.Log("Mouse Exit");
            if (selected)
            {
                selected.SetMaterial(standard);
            }
            selected = null;
            if (!dragging)
                StopAnimation(false);
        }

        if (dragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Stop Dragging");
            if (selected != this && validWall)
            {
                Debug.Log("Place New Wall " + selected + "-" + newWall);
                newWall.transform.LookAt(selected.transform.position);
                newWall.transform.localScale = Vector3.one;

                // recalculate pathing
                FindObjectOfType<AstarPath>().Scan();
                if (!playingField.IsValidLevel())
                {
                    Destroy(newWall);
                    FindObjectOfType<AstarPath>().Scan();
                    errorText.DisplayError("Path to the exit cannot be fully obstructed");
                }
                newWall = null;
            }
            else
            {
                Debug.Log("Discard New Wall");
                Destroy(newWall);
            }
            resetSelections();
        }

        if (!dragging && mouseOver && Input.GetMouseButtonDown(0))
        {
            dragging = true;
            Debug.Log("Start Dragging");
        }

        if (dragging && animate)
            StopAnimation(true);

        if (dragging && newWall == null)
        {
            Debug.Log("Make New Wall");
            newWall = Instantiate(draggableWallPrototype);
            newWall.transform.position = transform.position;
            newWall.transform.localScale = Vector3.zero;
        }

        if (dragging && newWall)
        {
            //Debug.Log("Updating Wall");
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
            RunAnimation();

        base.Update();
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
        if (newWall)
        {
            Destroy(newWall);
            newWall = null;
        }
    }

    private void RunAnimation()
    {
        var change = animationDirection * animationStep * Time.deltaTime;
        animationSize += change;
        transform.localScale = Vector3.one * animationSize;
        if (Mathf.Abs(animationSize - 1) > animationRange)
        {
            animationDirection *= -1;
        }
    }

    private void StartAnimation()
    {
        animationDirection = 1;
        animationSize = 1;
        animate = true;
    }

    private void StopAnimation(bool large)
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
