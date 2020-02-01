using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject enemy;

	public int spawnTime = 5;


	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Update () {

	}

	void Spawn () {

		Transform spawnPoint = spawnPoints[Random.Range (0, spawnPoints.Length)];

		Instantiate (enemy, spawnPoint);
	}

}