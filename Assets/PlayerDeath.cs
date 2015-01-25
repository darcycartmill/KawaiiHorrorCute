using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	CharacterController controller;
	InventoryManager inventory;
	MouseLook look;

	float timeToRestart = 3;
	bool dead = false;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		inventory = GetComponent<InventoryManager>();
		look = GetComponent<MouseLook>();
	}
	
	// Update is called once per frame
	void Update () {
		if(dead == true){
			timeToRestart -= Time.deltaTime;
			if(timeToRestart < 0){
				Application.LoadLevel(0);
			}
			return;
		}

		Collider[] found = Physics.OverlapSphere(transform.position, 1);
		foreach(Collider col in found){
			if(col.tag == "Enemy"){
				Kill(col);
			}
		}
	}

	void Kill(Collider col){
		if(col.gameObject.tag == "Enemy"){
			controller.enabled = false;
			inventory.enabled = false;
			rigidbody.isKinematic = false;
			look.enabled = false;
			rigidbody.AddExplosionForce(999, col.transform.position + Random.insideUnitSphere, 15);
			dead = true;
		}

	}
}
