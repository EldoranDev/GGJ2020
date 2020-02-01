using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    Vector3 target;

    Dictionary<GameObject, int> payload = new Dictionary<GameObject, int>();

    // Update is called once per frame
    void Update()
    {
        var newPos = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = newPos;
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }

    public void AddToPayload(GameObject enemy, int count)
    {
        this.payload.Add(enemy, count);
    }

    public Dictionary<GameObject, int> GetPayload()
    {
        return payload;
    }
}
