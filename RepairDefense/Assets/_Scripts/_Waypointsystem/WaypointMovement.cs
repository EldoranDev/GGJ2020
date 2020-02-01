<<<<<<< Updated upstream
﻿using System.Collections;
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
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField]
    private Waypoint waypoint;
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
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, waypoint.GetCurrentWaypoint().transform.position, distance);
        Debug.Log(distance);
    }

    public void SetNextMovement()
    {
        waypointIndex++;
        waypoint.SetCurrentWaypoint(waypointIndex);
    }
    private void Awake()
    {
        this.gameObject.GetComponent<Waypoint>();
    }

    private void Update()
    {
        if (moveToWaypoint)
        {
            Move();
        }
    }
}
>>>>>>> Stashed changes
