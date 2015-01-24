using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

	public Transform heldItemSlot;

	ItemScript heldItem;

	int inventorySlots = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//apply held effects, like the lighter remaining on
		if(heldItem != null){
			heldItem.HeldEffect();
		}

		float activateItemButton = Input.GetAxis("Fire1");
		float switchItemButton = Input.GetAxis("Mouse ScrollWheel");
	}
}
