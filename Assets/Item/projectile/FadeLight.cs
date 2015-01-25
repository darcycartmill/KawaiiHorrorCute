using UnityEngine;
using System.Collections;

public class FadeLight : MonoBehaviour {

	public Light myLight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		myLight.intensity -= Time.deltaTime;
		if(myLight.intensity <= 0){
			Destroy(gameObject);
		}
	}
}
