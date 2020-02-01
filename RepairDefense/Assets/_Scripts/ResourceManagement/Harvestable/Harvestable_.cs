using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Harvestable_ : MonoBehaviour
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
	public int harvestedAmount;
	ResourceManager rManager;
	public ResourceType r_type;

	[SerializeField]
	private List<Collider> m_acTrigger;

	// public ResourceType GetResourceType(){
	// 	return type;
	// }

	private void Awake()
	{
		rManager= GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
		
	}

	public void OnHarvest()
	{
		rManager.AddResource(this, 5);

	}

	private void OnTriggerEnter(Collider other){
		if(other.GameObject.tag == "Player"){
			other.GetComponentInChildren<Interaction>().UnityEventInteraction.AddListener(OnHarvest);
		}

	}
}