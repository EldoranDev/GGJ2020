﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Destructible : MonoBehaviour
{
    [SerializeField]
    private float m_fHealth;

    private float m_fInitHealth;

    [SerializeField]
    private List<Collider> m_acTrigger;

    [SerializeField]
    private Transform m_cHealthFill;

    [SerializeField]
    Material colapseMaterial;

    [SerializeField]
    float dissolveSpeed = 0.2f;

    public UnityEvent OnCollapse { get; } = new UnityEvent();

    public bool Collapsed
    {
        get; private set;
    }

    private bool fullyHidden = false;

    void Awake()
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

    void Update()
    {
        if (Collapsed && !fullyHidden)
        {
            Debug.Log(colapseMaterial.shaderKeywords);
            var current = colapseMaterial.GetFloat("Dissolve");

            colapseMaterial.SetFloat("Dissolve", current + dissolveSpeed * Time.deltaTime);

            if (current >= 1.5)
            {
                fullyHidden = true;
            }
        }
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
        Collapsed = true;
        OnCollapse.Invoke();

        foreach(var collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }

        var renderer = GetComponent<Renderer>();

        for (var i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i] = colapseMaterial;
        }
    }

    private void UpdateHealthBar()
    {
        if (m_cHealthFill != null)
        {
            m_cHealthFill.localScale = new Vector3(m_fHealth / m_fInitHealth, 1.0f, 1.0f);
            m_cHealthFill.localPosition = new Vector3((1.0f - m_fHealth / m_fInitHealth) * 0.01f, 0.0f, 0.0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<Interaction>().unityEventInteraction.AddListener(() => OnRepairDestructible(10));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<Interaction>().unityEventInteraction.RemoveAllListeners();
        }
    }
}
