using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    Vector3 size = new Vector3(1, 0.1f, 1);

    [SerializeField]
    public bool active = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, size);
    }

    void OnTriggerEnter(Collider collider)
    {
        var asteroid = collider.GetComponentInParent<Asteroid>();

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
                Instantiate(spawn.Key, transform.position, Quaternion.identity);
            }
        }

        Destroy(asteroid.gameObject);
    }
}
