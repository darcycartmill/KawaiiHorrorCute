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

		Collider[] found = Physics.OverlapSphere(transform.position, 10);
		foreach(Collider iter in found){
			EnemyScript script = iter.GetComponent<EnemyScript>();
			if(script != null){
				script.Kill();
			}
		}
	}
}
