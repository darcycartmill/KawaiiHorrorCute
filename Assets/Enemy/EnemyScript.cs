using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	NavMeshAgent myAgent;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		myAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		myAgent.SetDestination(player.transform.position);
	}
}
