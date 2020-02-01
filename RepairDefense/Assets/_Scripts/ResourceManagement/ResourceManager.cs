using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ResourceManager : MonoBehaviour
{
	private int currentwood;
	private int currentstone;
	private int currentmetal;
	private int currentfood;
	private int currentwater;
	
	public void AddResource(Harvestable_ harv, int amount){
		Debug.Log("Resource Added: " + harv.r_type + amount);

	}


}

