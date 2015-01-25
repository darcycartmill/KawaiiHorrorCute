using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LighterScript : ItemScript {

	GameObject powerbar;

	public Light myLight;

	public float minIntensity = 0.6f;
	public float maxIntensity = 1;

	float _energy;
	float _energyLossRate = 5;
	float _energyGainRate = 2;

	void Start(){
		powerbar = GameObject.Find("Bar");
		myLight.enabled = false;
		_energy = 100;
	}

	public override void HeldEffect(){

		Vector3 scale = new Vector3(1, _energy / 100f, 1);
		if(powerbar != null){
			powerbar.transform.localScale = scale;
		}

		if(myLight.enabled == true){
			_energy -= Time.deltaTime * _energyLossRate;
		}

		//add some flicker to the light if it's enabled
		float flicker = Random.Range(-0.1f, 0.1f);
		myLight.intensity += flicker;
		myLight.intensity = Mathf.Clamp(myLight.intensity, minIntensity, maxIntensity);
	}

	public override void OnPickup ()
	{
		myLight.enabled = true;
	}

	public override void OnDrop ()
	{
		myLight.enabled = false;
	}

	public override void ApplyEffect(){
		//applying the lighter turns it on or off
		myLight.enabled = !myLight.enabled;
	}

	void Update(){
		_energy += Time.deltaTime * _energyGainRate;
		_energy = Mathf.Clamp(_energy, 0, 100);
	}
}
