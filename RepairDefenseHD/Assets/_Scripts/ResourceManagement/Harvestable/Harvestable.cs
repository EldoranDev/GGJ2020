using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Harvestable : MonoBehaviour
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

	// public ResourceType GetResourceType(){
	// 	return type;
	// }

	private void Awake()
	{
		rManager = GameObject.FindObjectOfType<ResourceManager>();
	}

	public void OnHarvest()
	{
		rManager.UpdateResource(this, 5);
	}


	private void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			other.gameObject.GetComponentInChildren<Interaction>().unityEventInteraction.AddListener(OnHarvest);
		}

	}

	public void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			other.gameObject.GetComponentInChildren<Interaction>().unityEventInteraction.RemoveListener(OnHarvest);
		}
	}

}