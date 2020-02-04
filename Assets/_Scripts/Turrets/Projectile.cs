using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10;

    bool fired = false;
    Vector3 target;

    private void Update()
    {
        if (fired)
        {
            transform.right = Vector3.Slerp(transform.right, -(target - transform.position), Time.deltaTime * 10);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
    public void Fire(Transform _Enemy)
    {
        
        fired = true;
        target = _Enemy.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().OnDamage(10);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Vector3.left * 100);
    }
}
