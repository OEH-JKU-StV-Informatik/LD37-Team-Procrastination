using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MonoBehaviour
{
    public GameObject draggableWallPrototype;
    public GameObject playingField;
    public float animationStep = 0.006f;
    public float animationRange = 0.08f;

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

    // Use this for initialization
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        SetMaterial(standard);
    }

    // Update is called once per frame
    void Update()
    {
        if (!newWall)
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
            } else
            {
                validWall = false;
            }

        }
    }

    void OnMouseEnter()
    {
        selected = this;
        animationDirection = 1;
    }

    void OnMouseExit()
    {
        if (selected)
        {
            selected.SetMaterial(standard);
        }
        selected = null;
        if (!dragging)
            transform.localScale = Vector3.one;
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
                //FindObjectOfType<AstarPath>().
            }
            else
            {
                Destroy(newWall);
            }
        }

        resetSelections();
    }

    void OnMouseDown()
    {
        dragging = true;
        //Instantiate(wallPrototype, position, Quaternion.Euler(0, 270, 0), gameObject.transform)

        newWall = Instantiate(draggableWallPrototype);
        newWall.transform.position = transform.position;
        newWall.transform.localScale = Vector3.zero;
    }

    void OnMouseOver()
    {
        if (dragging)
        {
            transform.localScale = Vector3.one * (1 + animationRange);
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

    private void SetMaterial(Material material)
    {
        Debug.Log("ChangeColorTo " + material.name);
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
}
