using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform attackHand;

    [SerializeField]
    float attackRange = 0.1f;

    WaypointManager waypointManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponentInChildren<Animator>().SetTrigger("attack");
        }
    }

    void Attack()
    {
        var hits = Physics.OverlapSphere(attackHand.position, attackRange, LayerMask.NameToLayer("Default"));

        Debug.Log(hits);
    }
}
