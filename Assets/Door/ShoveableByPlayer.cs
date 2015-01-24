using UnityEngine;
using System.Collections;

public class ShoveableByPlayer : MonoBehaviour {

	bool unlocked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			Vector3 playerPos = GameObject.Find("Player").transform.position;
			rigidbody.AddExplosionForce(20, playerPos, 99);
		}
	}
	
}
