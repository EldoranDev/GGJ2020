using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WaypointMovement : MonoBehaviour
{
    [SerializeField]
    private WaypointManager waypointManager;
    [SerializeField]
    private bool moveToWaypoint;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private int waypointIndex;

    private Animator animator;

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
        var target = waypointManager.GetCurrentWaypoint().transform;
        float distance = movementSpeed * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, target.position, distance);

        var relativePosition = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Update Animator stuff
        animator.SetFloat("speed", distance);
    }

    public void SetWaypointManager(WaypointManager manager)
    {
        waypointManager = manager;
    }

    public void SetNextMovement()
    {
        waypointIndex++;
        waypointManager.SetCurrentWaypoint(waypointIndex);
    }
    private void Awake()
    {
        this.gameObject.GetComponent<WaypointManager>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("speed", 0);

        if (moveToWaypoint)
        {
            Move();
        }
    }
}
