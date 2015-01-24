using UnityEngine;
using System.Collections;

public class CanopenerScript : ItemScript {
	public override void HeldEffect(){}
	
	public override void ApplyEffect(){
		Collider[] foundColliders = Physics.OverlapSphere(transform.position, 4);
		foreach(Collider col in foundColliders){
			CanScript can = col.GetComponent<CanScript>();
			if(can != null){
				can.Kill();
				//presumably instantiating whatever was in the can goes here
			}
		}
	}

	public override void OnPickup(){}

}
