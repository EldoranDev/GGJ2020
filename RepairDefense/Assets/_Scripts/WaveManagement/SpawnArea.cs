using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    Vector3 size = new Vector3(1, 0.1f, 1);

    [SerializeField]
    Transform[] pathRoots;

    [SerializeField]
    public bool active = false;

    [SerializeField]
    new AudioSource audio;

    List<Waypoint[]> cachedPaths = new List<Waypoint[]>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, size);
    }

    void Awake()
    {
        foreach(var root in pathRoots)
        {
            cachedPaths.Add(root.GetComponentsInChildren<Waypoint>());
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        var asteroid = collider.GetComponentInParent<Asteroid>();

        Debug.Log(collider.gameObject);

        if (asteroid == null)
        {
            return;
        }

        // TODO: Play Explosion

        var payload = asteroid.GetPayload();

        foreach(KeyValuePair<GameObject, int> spawn in payload)
        {
            for (var i = 0; i < spawn.Value; i++)
            {
                var manager = Instantiate(spawn.Key, transform.position, Quaternion.identity).GetComponent<WaypointManager>();

                foreach(var waypoint in cachedPaths.GetRandom())
                {
                    manager.AddWayPoint(waypoint.gameObject);
                }

                manager.SetCurrentWaypoint(0);
            }
        }

        audio.Play();

        Destroy(asteroid);
        Destroy(asteroid.gameObject, 5f);
    }
}
