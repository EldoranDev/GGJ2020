using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointManager))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform attackPosition;

    [SerializeField]
    Animator animator;

    [SerializeField]
    EnemyAttackSettings attack;

    [SerializeField]
    EnemyAudioClips clips;

    [SerializeField]
    AudioSource audioSource;

    Destructible target;

    float currentCooldown;

    void Start()
    {
        currentCooldown = attack.cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                currentCooldown = attack.cooldown;

                animator.SetTrigger("attack");
            }


            var relativePosition = target.transform.position - transform.position;
            var lookRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1f);
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
        var hits = Physics.OverlapSphere(attackPosition.position, attack.range, LayerMask.GetMask("Default"));

        foreach(var hit in hits)
        {
            audioSource.PlayOneShot(clips.punch);
            var target = hit.GetComponent<Destructible>();
            target?.OnDamageDestructible(attack.strenght);   
        }

        
    }

    void Step()
    {
        audioSource.PlayOneShot(clips.footstep);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attack.range);
    }
}
