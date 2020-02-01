using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointManager))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform attackPosition;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float attackRange = 1f;

    [SerializeField]
    float attackStrength = 1f;

    [SerializeField]
    float attackCooldown = 1f;

    Destructible target;

    float currentCooldown;

    void Start()
    {
        currentCooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;

                animator.SetTrigger("attack");
            }
        }
    }

    public void SetTarget(Destructible target)
    {
        this.target = target;
    }

    public void ClearTarget()
    {
        this.target = null;
    }

    void Attack()
    {
        var hits = Physics.OverlapSphere(attackPosition.position, attackRange, LayerMask.GetMask("Default"));

        Debug.Log(hits.Length);

        foreach(var hit in hits)
        {
            var target = hit.GetComponent<Destructible>();
            target?.OnDamageDestructible(attackStrength);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
