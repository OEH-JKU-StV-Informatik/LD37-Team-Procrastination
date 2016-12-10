using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathHelper : MonoBehaviour {
    private Seeker seeker;

	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
	}
	
    public bool isReachableFrom(Vector3 start, Vector3 end)
    {
        Path path = seeker.GetNewPath(start, end);
        AstarPath.StartPath(path, true);
        AstarPath.WaitForPath(path);
        return !path.error;
    }
}
