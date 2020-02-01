using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField]
    private WaypointManager waypointManager;
    [SerializeField]
    private bool moveToWaypoint;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private int waypointIndex;

    public WaypointMovement()
    {
        moveToWaypoint = false;
        waypointIndex = 0;
    }

    public void SetMove(bool moveToWaypoint)
    {
        this.moveToWaypoint = moveToWaypoint;
    }
    private void Move()
    {
        float distance = movementSpeed * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, waypointManager.GetCurrentWaypoint().transform.position, distance);
    }

    public void SetNextMovement()
    {
        waypointIndex++;
        waypointManager.SetCurrentWaypoint(waypointIndex);
    }
    private void Awake()
    {
        this.gameObject.GetComponent<WaypointManager>();
    }

    private void Update()
    {
        if (moveToWaypoint)
        {
            Move();
        }
    }
}
