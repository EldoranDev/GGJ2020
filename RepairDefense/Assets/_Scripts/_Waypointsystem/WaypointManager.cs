using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> waypoints;
    [SerializeField][Range(0, 5)]
    private float waypointsize;
    [SerializeField]
    private GameObject currentWaypoint;
    [SerializeField]
    Color gizmoColor;

    private void Awake()
    {
        if (waypoints.Count > 0 && waypoints[0] != null) 
        { 
            currentWaypoint = waypoints[0];
        }
    }
    public GameObject GetCurrentWaypoint()
    {
        return currentWaypoint;
    }

    public List<GameObject> GetWaypoints()
    {
        return waypoints;
    }

    public void AddWayPoint(GameObject waypoint)
    {
        waypoints.Add(waypoint);
    }


    public void SetCurrentWaypoint(int index)
    {
        if(waypoints.Count > index)
        {
            currentWaypoint = waypoints[index];
        }
        else
        {
            Debug.LogError("Waypoint not available! Waypointsize: " + waypoints.Count + "||" + "Wanted index: " + index);
        }
    }
    private void OnDrawGizmos()
    {
        foreach (var waypoint in waypoints)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(waypoint.transform.position, waypointsize);
        }
    }
}
