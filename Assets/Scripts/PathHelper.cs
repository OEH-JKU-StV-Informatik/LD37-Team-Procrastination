using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathHelper : MonoBehaviour
{
    private Seeker seeker;
    public float validDistance = 0.4f;

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
        return isReachable;
    }
}
