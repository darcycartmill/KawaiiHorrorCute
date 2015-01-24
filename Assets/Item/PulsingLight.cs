using UnityEngine;
using System.Collections;

public class PulsingLight : MonoBehaviour {
	Light myLight;
	float _minIntensity = 0;
	float _maxIntensity = 0.1f;

	bool intensifying = false;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!intensifying){
			myLight.intensity -= Time.deltaTime;
		}else{
			myLight.intensity += Time.deltaTime;
		}


		if(myLight.intensity <= _minIntensity || myLight.intensity >= _maxIntensity){
			intensifying = !intensifying;
		}
	}
}
