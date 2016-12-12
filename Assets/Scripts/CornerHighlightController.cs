using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerHighlightController : MouseUIObject
{
    public GameObject draggableWallPrototype;
    public PlayingFieldController playingFieldController;
    public float animationStep;
    public float animationRange;
    public float validWallSize;

    public Material standard;
    public Material valid;
    public Material invalid;

    private float absoluteValidWallSize;
    private static CornerHighlightController selected = null;
    private bool animate = false;
    private bool dragging = false;
    private bool validWall = false;
    private bool validLongWall = false;
    private float animationDirection = 1;
    private float animationSize = 1;
    private GameObject newWall = null;
    private Renderer[] renderers;
    private ErrorText errorText;
    private Vector3 draggableWallScale;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        errorText = Resources.FindObjectsOfTypeAll<ErrorText>()[0];
        absoluteValidWallSize = validWallSize * playingFieldController.GetStepSize();
        draggableWallScale = Vector3.one + Vector3.forward * (playingFieldController.GetStepSize() - 1);
        SetMaterial(standard);
    }

    public override void Update()
    {
        if (mouseOver && mouseEnter)
        {
            //Debug.Log("Mouse Enter");
            selected = this;
            StartAnimation();
        }
        if (mouseExit)
        {
            //Debug.Log("Mouse Exit");
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
            //Debug.Log("Stop Dragging");
            if (selected != this && (validWall || validLongWall))
            {

                if (validWall)
                {
                    if (PlayingFieldController.maxWalls - PlayingFieldController.currentWalls > 0)
                    {
                        //Debug.Log("Place New Wall " + selected + "-" + newWall);
                        newWall.transform.LookAt(selected.transform.position);
                        newWall.transform.localScale = draggableWallScale;
                        PlayingFieldController.currentWalls++;

                        // recalculate pathing
                        FindObjectOfType<AstarPath>().Scan();
                        if (!playingFieldController.IsValidLevel())
                        {
                            PlayingFieldController.currentWalls--;
                            Destroy(newWall);

                            FindObjectOfType<AstarPath>().Scan();
                            errorText.DisplayError("Path to the exit cannot be fully obstructed");
                        }
                        newWall = null;
                    }
                    else
                    {
                        errorText.DisplayError("Too Many Walls", 2);
                        // send to many walls errormessage
                    }
                }
                else if (validLongWall)
                {
                    int count = Mathf.RoundToInt((selected.transform.position - transform.position).magnitude / playingFieldController.GetStepSize());
                    if (PlayingFieldController.maxWalls - PlayingFieldController.currentWalls - count >= 0)
                    {
                        List<GameObject> newWalls = new List<GameObject>();
                        Destroy(newWall);
                        newWall = null;
                        //Debug.Log("Place New Walls " + selected + "-" + newWall + " x" + count);

                        for (int i = 0; i < count; i++)
                        {
                            newWall = Instantiate(draggableWallPrototype);
                            newWall.transform.position = transform.position; //+i
                            newWall.transform.LookAt(selected.transform.position);
                            newWall.transform.localPosition += i * playingFieldController.GetStepSize() * newWall.transform.forward;
                            newWall.transform.localScale = draggableWallScale;
                            //newWall.GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 1, 0);
                            newWalls.Add(newWall);
                        }
                        PlayingFieldController.currentWalls += newWalls.Count;

                        // recalculate pathing
                        FindObjectOfType<AstarPath>().Scan();
                        if (!playingFieldController.IsValidLevel())
                        {
                            foreach (GameObject newWall in newWalls)
                                Destroy(newWall);

                            FindObjectOfType<AstarPath>().Scan();
                            errorText.DisplayError("Path to the exit cannot be fully obstructed");
                            PlayingFieldController.currentWalls -= newWalls.Count;
                        }
                        newWall = null;
                    }
                    else
                    {
                        errorText.DisplayError("Too Many Walls", 2);
                        // send to many walls errormessage
                    }
                }
            }
            //Debug.Log("Discard New Wall");
            Destroy(newWall);
            resetSelections();
        }

        if (!dragging && mouseOver && Input.GetMouseButtonDown(0))
        {
            if (PlayingFieldController.currentWalls < PlayingFieldController.maxWalls)
            {
                dragging = true;
                //Debug.Log("Start Dragging");
            }
            else
            {
                errorText.DisplayError("All walls are already used!");
            }
        }

        if (dragging && animate)
            StopAnimation(true);

        if (dragging && newWall == null)
        {
            //Debug.Log("Make New Wall");
            newWall = Instantiate(draggableWallPrototype);
            newWall.transform.position = transform.position;
            newWall.transform.localScale = Vector3.zero;
            //newWall.GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 0, 0);
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
                validWall = (selected.transform.position - transform.position).magnitude <= absoluteValidWallSize;
                validLongWall = (Mathf.Abs(selected.transform.position.x - transform.position.x) < (playingFieldController.GetStepSize() / 10)
                              || Mathf.Abs(selected.transform.position.z - transform.position.z) < (playingFieldController.GetStepSize() / 10));

                selected.SetMaterial(validWall || validLongWall ? valid : invalid);
            }
            else
            {
                validWall = false;
                validLongWall = false;
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
        transform.localScale = Vector3.one + (Vector3.up * animationSize) - Vector3.up;
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
            transform.localScale = Vector3.one + Vector3.up * (1 + animationRange) - Vector3.up;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
