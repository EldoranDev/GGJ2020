using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ResourceManager : MonoBehaviour
{
	public TMP_Text resourcetext;

	public int currentwood;
	public int currentstone;
	public int currentmetal;
	public int currentfood;
	public int currentwater;

	private int result;

	private string h_type;


	void Start (){

	}
	
	public void UpdateResource(Harvestable harv, int amount){
		Debug.Log("Resource Added: " + harv.r_type + amount);
		h_type = harv.r_type.ToString();
		switch (h_type){
			case "Wood": 
			currentwood += amount;
			break;
			case "Stone": 
			currentstone += amount;
			break;
			case "Metal": 
			currentmetal += amount;
			break;
			case "Water": 
			currentwater += amount;
			break;
			case "Food": 
			currentfood += amount;
			break;
		}
		UpdateGUI();
	}

	public int GetResourceAmount(Harvestable harv){
		h_type = harv.r_type.ToString();
		switch (h_type){
			case "Wood": 
				result = currentwood;
				break;
			case "Stone": 
				result = currentstone;
				break;
			case "Metal": 
				result = currentmetal;
				break;
			case "Water": 
				result = currentwater;
				break;
			case "Food": 
				result = currentfood;
				break;
		}
		Debug.Log(h_type + " , " + result);
		return result;
	}

	public void UpdateGUI(){
		resourcetext.text = "Wood: " + currentwood + " \nStone: " + currentstone + " \nMetal: " + currentmetal + " \nFood: " + currentfood + " \nWater: " + currentwater;
	}

}

