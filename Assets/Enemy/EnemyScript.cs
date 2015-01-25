﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	NavMeshAgent myAgent;
	GameObject player;

	bool alive;

	public AudioSource mysource;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		myAgent = GetComponent<NavMeshAgent>();

		NavMeshHit hit;

		NavMesh.SamplePosition(transform.position, out hit, 99999, 9999);
		transform.position = hit.position;
		/*
		myAgent.FindClosestEdge(out hit);
		transform.position = hit.position;
		*/
	}

	public void Kill(){
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {

		if(Vector3.Magnitude(player.transform.position - transform.position) < 10){
			if(!mysource.isPlaying){
				mysource.Play();
			}
		}

		if(!myAgent.SetDestination(player.transform.position)){
			Kill();
		}

	}
}
