using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;

	GameObject player;
	float currentSpawnrate = 30;
	float spawnrateTickdown = 0.3f;
	float minSpawnRate = 2;
	float spawntime;
	float spawnDistance = 20;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		spawntime = currentSpawnrate;
	}
	
	// Update is called once per frame
	void Update () {
		spawntime -= Time.deltaTime;
		if(spawntime < 0){
			if(currentSpawnrate > minSpawnRate){
				currentSpawnrate -= spawnrateTickdown;
			}
			spawntime = currentSpawnrate;
			Vector3 spawnpos = player.transform.position;
			spawnpos -= player.transform.forward * spawnDistance;

			Instantiate(enemy, spawnpos, player.transform.rotation);
		}
	}
}
