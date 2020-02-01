using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            if (enemy.GetComponent<WaypointManager>().GetCurrentWaypoint() == this.gameObject)
                enemy.GetComponent<WaypointMovement>().SetNextMovement();
        }   
    }
}