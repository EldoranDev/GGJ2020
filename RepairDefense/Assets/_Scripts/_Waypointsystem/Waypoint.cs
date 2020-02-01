using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Waypoint : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            AdvanceWaypoint(other.gameObject);
        }   
    }
    
    protected virtual void AdvanceWaypoint(GameObject enemy)
    {
        if (enemy.GetComponent<WaypointManager>().GetCurrentWaypoint() == gameObject)
        {
            enemy.GetComponent<WaypointMovement>().SetNextMovement();
        }
    }
}
