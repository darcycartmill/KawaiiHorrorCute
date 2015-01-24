using UnityEngine;
using System.Collections;

public class LighterScript : ItemScript {

	public Light myLight;

	float _minIntensity = 0.6f;
	float _maxIntensity = 1;

	void Start(){
		myLight.enabled = false;
	}

	public override void HeldEffect(){
		//add some flicker to the light if it's enabled
		float flicker = Random.Range(-0.1f, 0.1f);
		myLight.intensity += flicker;
		myLight.intensity = Mathf.Clamp(myLight.intensity, _minIntensity, _maxIntensity);
	}
	
	public override void ApplyEffect(){
		//applying the lighter turns it on or off
		myLight.enabled = !myLight.enabled;
	}
}
