using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
    [SerializeField]
    private float m_fHealth;

    private float m_fInitHealth;

    [SerializeField]
    private List<Collider> m_acTrigger;

    [SerializeField]
    private Transform m_cHealthFill;

    private void Awake()
    {
        m_fInitHealth = m_fHealth;

        if (m_acTrigger != null && m_acTrigger.Count > 0)
        {
            foreach (Collider cTrigger in m_acTrigger)
            {
                TriggerBehaviour cBehaviour = cTrigger.gameObject.AddComponent<TriggerBehaviour>();
                cBehaviour.SetBuilding(this);
            }
        }

        UpdateHealthBar();
    }

    [ContextMenu("Damage Destructible")]
    public void TestDamage()
    {
        OnDamageDestructible(10.0f);
    }

    [ContextMenu("Repair Destructible")]
    public void TestRepair()
    {
        OnRepairDestructible(5.0f);
    }

    public void OnDamageDestructible(float fDamage)
    {
        m_fHealth -= fDamage;

        if(m_fHealth < 0.0f)
        {
            m_fHealth = 0.0f;
            OnCollapseDestructible();
        }
        UpdateHealthBar();
    }

    public void OnRepairDestructible(float fRepairValue)
    {
        m_fHealth += fRepairValue;

        if(m_fHealth > m_fInitHealth)
        {
            m_fHealth = m_fInitHealth;
        }
        UpdateHealthBar();
    }

    private void OnCollapseDestructible()
    {

    }

    private void UpdateHealthBar()
    {
        if (m_cHealthFill != null)
        {
            m_cHealthFill.localScale = new Vector3(m_fHealth / m_fInitHealth, 1.0f, 1.0f);
            m_cHealthFill.localPosition = new Vector3((1.0f - m_fHealth / m_fInitHealth) * 0.01f, 0.0f, 0.0f);
        }
    }
}
