using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerDeath : MonoBehaviour {

	CharacterController controller;
	InventoryManager inventory;
	MouseLook look;
	Image deathOverlay;

	Transform killerTrans;

	float timeToRestart = 3;
	bool dead = false;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		inventory = GetComponent<InventoryManager>();
		look = GetComponent<MouseLook>();
		deathOverlay = GameObject.Find("DeathOverlay").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(dead == true){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(killerTrans.position - transform.position), 0.2f);

			Color red = deathOverlay.color;
			red.a += Time.deltaTime;
			deathOverlay.color = red;

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
			killerTrans = col.transform;
			controller.enabled = false;
			inventory.enabled = false;
			rigidbody.isKinematic = false;
			look.enabled = false;
			rigidbody.AddForceAtPosition(Random.insideUnitSphere * 40, Random.insideUnitSphere);
			dead = true;
		}

	}
}
