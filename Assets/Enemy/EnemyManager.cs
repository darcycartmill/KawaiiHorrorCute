using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;

	GameObject player;
	float currentSpawnrate = 30;
	float spawnrateTickdown = 1.5f;
	float minSpawnRate = 4;
	float spawntime;
	float spawnDistance = 50;

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
			if(currentSpawnrate > 20){
				currentSpawnrate = 20;
			}

			spawntime = currentSpawnrate;
			Vector3 spawnpos = player.transform.position;
			Vector2 circle = Random.insideUnitCircle * spawnDistance;
			spawnpos += new Vector3(circle.x, 0, circle.y);

			//spawnpos = Quaternion.AngleAxis(Random.Range(36 * spawnpos;

			Instantiate(enemy, spawnpos, player.transform.rotation);
		}
	}
}
