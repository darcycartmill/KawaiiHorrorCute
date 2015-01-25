using UnityEngine;
using System.Collections;

public class BlasterScript : ItemScript {

	GameObject powerbar;
	
	public GameObject shootMe;

	public AudioSource mySource;

	float _energy;
	float _energyLossRate = 5;
	float _energyGainRate = 2;

	float _energyPerShot = 20;

	void Start(){
		powerbar = GameObject.Find("Bar");
		_energy = 100;
	}
	
	public override void HeldEffect(){

	}
	
	public override void OnPickup ()
	{
	
	}
	
	public override void OnDrop ()
	{

	}
	
	public override void ApplyEffect(){
		if(_energy > _energyPerShot){
			_energy -= _energyPerShot;
			mySource.Play();
		}else{
			return;
		}

		GameObject projectile = Instantiate(shootMe, transform.position, transform.rotation) as GameObject;
		projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 700);
	}

	public override void OnUpdate(){
		_energy += Time.deltaTime * _energyGainRate;
		_energy = Mathf.Clamp(_energy, 0, 100);

		Vector3 scale = new Vector3(1, _energy / 100f, 1);
		if(powerbar != null){
			powerbar.transform.localScale = scale;
		}
	}
}
