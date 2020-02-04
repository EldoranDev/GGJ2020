using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedWaypoint : Waypoint
{
    [SerializeField]
    Destructible blocker;

    List<GameObject> blockedEnemies = new List<GameObject>();

    void Start()
    {
        blocker.OnCollapse.AddListener(BlockerCollapsed);
    }

    protected override void AdvanceWaypoint(GameObject enemy)
    {
        var manager = enemy.GetComponent<WaypointManager>();
        var movement = enemy.GetComponent<WaypointMovement>();

        if (blocker.Collapsed)
        {
            if (manager.GetCurrentWaypoint() == gameObject)
            {
                movement.SetNextMovement();
            }

            return;
        }

        movement.SetMove(false);
        blockedEnemies.Add(enemy);

        //TODO: Force enemy to attack the Blocker
        enemy.GetComponent<Enemy>().SetTarget(blocker);
    }

    private void BlockerCollapsed()
    {
        blockedEnemies.ForEach((enemy) =>
        {
            AdvanceWaypoint(enemy);
            enemy.GetComponent<Enemy>().ClearTarget();
            enemy.GetComponent<WaypointMovement>().SetMove(true);
        });
    }
}
