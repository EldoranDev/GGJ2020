using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harvestable : MonoBehaviour
{

	public enum ResourceType{
		Wood,
		Stone,
		Metal,
		Food,
		Water,
		None
	}


	[SerializeField]
	private int Amount;

	private int MaxAmount;

	public int harvestedAmount;

	public ResourceType type;

	[SerializeField]
	private List<Collider> m_acTrigger;

	[SerializeField]
	private Transform AmountFill;

	private void Awake()
	{
		MaxAmount = Amount;

		if (m_acTrigger != null && m_acTrigger.Count > 0)
		{
			foreach (Collider cTrigger in m_acTrigger)
			{
				TriggerBehaviour cBehaviour = cTrigger.gameObject.AddComponent<TriggerBehaviour>();
			
			}
		}

		UpdateAmountBar();
	}

	[ContextMenu("Harvested")]
	public void TestHarvest()
	{
		OnHarvest(harvestedAmount);
	}
		

	public void OnHarvest(int fDamage)
	{
		Amount -= fDamage;

		if(Amount < 0)
		{
			Amount = 0;
			OnFullyHarvested();
		}
		UpdateAmountBar();
	}
		
	private void OnFullyHarvested(){

	}

	private void UpdateAmountBar()
	{
		if (AmountFill != null)
		{
			AmountFill.localScale = new Vector3(Amount / MaxAmount, 1.0f, 1.0f);
			AmountFill.localPosition = new Vector3((1 - Amount / MaxAmount) * 0.01f, 0.0f, 0.0f);
		}
	}
}
