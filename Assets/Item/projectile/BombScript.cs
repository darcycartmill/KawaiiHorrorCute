using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col){
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
