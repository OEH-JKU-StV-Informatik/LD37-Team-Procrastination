using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathHelper : MonoBehaviour
{
    private Seeker seeker;
    private float validDistance = 1f;

    // Use this for initialization
    void Start()
    {
        seeker = GetComponent<Seeker>();
    }

    public bool isReachableFrom(Vector3 start, Vector3 end)
    {
        Path path = seeker.GetNewPath(start, end);
        AstarPath.StartPath(path, true);
        AstarPath.WaitForPath(path);
        float distance = (end - path.vectorPath[path.vectorPath.Count - 1]).magnitude;

        bool isReachable = !path.error && distance < validDistance;
        Debug.Log("Path Distance:" + distance + " < " + validDistance + " -> " + (distance < validDistance));
        Debug.Log("Path Error:" + path.error);
        Debug.Log("Result:" + isReachable);
        return isReachable;
    }
}
