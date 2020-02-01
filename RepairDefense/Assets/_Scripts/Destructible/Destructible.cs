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
    ResourceManager rManager;
    private List<Collider> m_acTrigger;

    [SerializeField]
    private Transform m_cHealthFill;

    public UnityEvent OnCollapse { get; } = new UnityEvent();

    public bool Collapsed
    {
        get; private set;
    }

    private void Awake()
    {
        rManager= GameObject.Find("ResourceManager").GetComponent<ResourceManager>();

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
        OnRepairDestructible(5);
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

    public void OnRepairDestructible(int fRepairValue)
    {

        if (/*m_fHealth < m_fInitHealth*/rManager.currentwood >= fRepairValue && rManager.currentstone >= fRepairValue){
            Debug.Log("Repairing. Resources: Wood: " + rManager.currentwood + "Stone: " + rManager.currentstone);
            rManager.currentwood -= 10;
            rManager.currentstone -= 10;
            Debug.Log("RepairedResources: Wood: " + rManager.currentwood + "Stone: " + rManager.currentstone);
            m_fHealth += fRepairValue;

            if(m_fHealth > m_fInitHealth)
            {
                m_fHealth = m_fInitHealth;
            }
            rManager.UpdateGUI();
            UpdateHealthBar();
        } else {
            int requiredwood = fRepairValue - rManager.currentwood;
            int requiredstone = fRepairValue - rManager.currentstone;
            if (requiredwood > 0){
                Debug.Log("You require " + requiredwood + " more Wood!");
            }
            if(requiredstone > 0){
                Debug.Log("You require " + requiredstone + " more Stone!");
            }
        }
    }

    private void OnCollapseDestructible()
    {
        Collapsed = true;
        OnCollapse.Invoke();

        // TODO: Update Model + Collider
        
        // just removing the collider for now
        foreach(var collider in GetComponents<Collider>())
        {
            collider.enabled = false;
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

    void Update(){
        if (Input.GetKeyDown(KeyCode.R)){
            Debug.Log("R pressed");
            OnRepairDestructible(10);
        }
    }
}
