using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerBehaviour : MonoBehaviour
{
    private Destructible m_cBuilding;

    public void SetBuilding(Destructible cBuilding)
    {
        m_cBuilding = cBuilding;
    }

    private void OnColliderEnter(Collision collision)
    {
        if(m_cBuilding != null)
        {
            // TODO: Add Damage from Enemy which is stored in collision
            m_cBuilding.OnDamageDestructible(10.0f);
        }
    }
}
