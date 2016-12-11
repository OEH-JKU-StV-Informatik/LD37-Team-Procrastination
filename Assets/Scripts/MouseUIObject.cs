using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseUIObject : MonoBehaviour
{
    protected bool mouseEnter = true;
    protected bool mouseOver = false;
    protected bool mouseExit = false;

    public virtual void Update()
    {
        if (mouseOver)
        {
            mouseEnter = false;
            mouseOver = false;
            mouseExit = true;
            //Debug.Log("MouseUIObject Update - " + mouseEnter + mouseOver + mouseExit);
        }
        else
        {
            mouseEnter = true;
            mouseExit = false;
        }
    }

    public void OnMouseOver()
    {
        mouseOver = true;
        mouseExit = false;
        //Debug.Log("MouseUIObject OnMouseOver - " + mouseEnter + mouseOver + mouseExit);
    }
}
