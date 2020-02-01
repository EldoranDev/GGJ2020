using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AttackSettings")]
public class EnemyAttackSettings : ScriptableObject
{
    [SerializeField]
    public float range = 1f;

    [SerializeField]
    public float strenght = 1f;

    [SerializeField]
    public float cooldown = 1f;

}
