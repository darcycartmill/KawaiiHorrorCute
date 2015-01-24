using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
	//where we hold an item in front of us
	public Transform heldItemTrans;
	//where we shove an item while applying it
	public Transform useItemTrans;
	//where we bring an item to while putting it away
	public Transform swapItemTrans;
	//parent all picked up items to this and it will guid them around
	public Transform itemSlot;

	List<ItemScript> _inventory;
	int _heldItemIndex;
	int _inventorySlots = 2;
	float _pickupRadious = 3;

	Transform _itemSlotDestination;
	float _itemSlotSpeed = 10;
	// Use this for initialization
	void Start () {
		_inventory = new List<ItemScript>();
	}
	//pull item out of camera view, then change it to whatever else
	IEnumerator SwapItem(){
		return null;
	}

	//push the item to the active item position, then do whatever effect this item does
	IEnumerator UseItem(){
		return null;
	}

	void UpdateItem(){
		//if we don't have any items nothing to do here
		if(_inventory.Count == 0){
			return;
		}

		//apply held effects, like the lighter remaining on
		if(_inventory[_heldItemIndex] != null){
			_inventory[_heldItemIndex].HeldEffect();
		}
		             
		float activateItemButton = Input.GetAxis("Fire1");
	    float switchItemButton = Input.GetAxis("Mouse ScrollWheel");
	}

	void PrepareItemForStorage(ItemScript newItem){
		newItem.collider.isTrigger = true;
		newItem.rigidbody.isKinematic = true;

		//put the item slot on the new item so we can pick it up nicely
		itemSlot.transform.position = newItem.transform.position;
		itemSlot.transform.rotation = newItem.transform.rotation;

		//parent the new item to the slot
		newItem.transform.parent = itemSlot;

		_inventory.Add(newItem);
	}

	void DropItem(ItemScript oldItem){
		oldItem.enabled = true;
		oldItem.collider.isTrigger = true;
		oldItem.rigidbody.isKinematic = false;
		oldItem.transform.parent = null;

		_inventory.Remove(oldItem);
	}

	void PickupItem(){
		//cast a sphere around our hand, and check all the found colliders for takeable items
		Collider[] foundColliders = Physics.OverlapSphere(heldItemTrans.position, _pickupRadious);  
		ItemScript foundScript = null;
		foreach(Collider iter in foundColliders){
			foundScript = iter.GetComponent<ItemScript>();
			//if we find an item we can take, break
			if(foundScript != null){
				break;
			}
		}

		//nothing to pick up, nothing to do here. 
		if(foundScript == null){
			return;
		}

		PrepareItemForStorage(foundScript);

		//if we don't have any items yet, just pick the thing up
		if(_inventory.Count == 0){
			PrepareItemForStorage(foundScript);
			return;
		}

		//if out inventory isn't full yet, add the new item to our inventory and then switch to it
		if(_inventory.Count < _inventorySlots){
			//disable the item we're currently holding
			_inventory[_heldItemIndex].enabled = false;
			//pick up the new item
			PrepareItemForStorage(foundScript);
			//set our held index to our new item
			_heldItemIndex = _inventory.Count - 1;
			return;
		}

		//finally, if our inventory is full drop the current item and pick up the new one
		DropItem(_inventory[_heldItemIndex]);
		//pick up the new item
		PrepareItemForStorage(foundScript);
		//set our held index to our new item
		_heldItemIndex = _inventory.Count - 1;

	}

	void MoveItemSlot(){
		itemSlot.position = Vector3.Lerp(itemSlot.position, _itemSlotDestination.position, Time.deltaTime * _itemSlotSpeed);
		itemSlot.rotation = Quaternion.Slerp(itemSlot.rotation, _itemSlotDestination.rotation, Time.deltaTime * _itemSlotSpeed);
	}

	// Update is called once per frame
	void Update () {
		_itemSlotDestination = heldItemTrans;

		UpdateItem();

		if(Input.GetAxis("Fire2") != null){
			PickupItem();
		}

		MoveItemSlot();
	}
}
