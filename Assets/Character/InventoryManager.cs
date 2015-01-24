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
	int _inventorySlots = 6;
	float _pickupRadious = 3;

	Transform _itemSlotDestination;
	float _itemSlotSpeed = 10;

	bool _runningCoroutine;

	float _prevFire2;
	float _prevFire1;

	bool _incrementInventorySlot;

	InventoryBoxManager inventoryBoxManager;

	// Use this for initialization
	void Start () {
		inventoryBoxManager = GameObject.Find("InventoryBox").GetComponent<InventoryBoxManager>();
		_inventory = new List<ItemScript>();
		RecalculateGUI();
	}
	//pull item out of camera view, then change it to whatever else
	IEnumerator SwapItem(){

		//move our held item to the bag of holding on our back
		while(Vector3.SqrMagnitude(itemSlot.position - swapItemTrans.position) > 0.01f){
			_runningCoroutine = true;
			_itemSlotDestination = swapItemTrans;
			yield return null;
		}
		//once it gets there change it to the next item
		if(_incrementInventorySlot){
			_heldItemIndex++;
			if(_heldItemIndex >= _inventory.Count){
				_heldItemIndex = 0;
			}
		}else{
			_heldItemIndex--;
			if(_heldItemIndex < 0){
				_heldItemIndex = _inventory.Count - 1;
			}
		}
		HighlightEquiptItem();
		_runningCoroutine = false;
		_itemSlotDestination = null;
		EnableItem(_heldItemIndex);

		

		yield break;
	}

	//push the item to the active item position, then do whatever effect this item does
	IEnumerator UseItem(){
		while(Vector3.SqrMagnitude(itemSlot.position - useItemTrans.position) > 0.01f){
			_runningCoroutine = true;
			_itemSlotDestination = useItemTrans;
			yield return null;
		}
		//once we're in position use our item
		_inventory[_heldItemIndex].ApplyEffect();
		_runningCoroutine = false;
		_itemSlotDestination = null;
		yield break;
	}

	void UpdateItem(){
		//if we don't have any items nothing to do here
		if(_inventory.Count == 0){
			return;
		}

		//kinda hackfixy. if the index is invalid (usually after dropping something) just set it to zero
		if(_heldItemIndex < 0 || _heldItemIndex >= _inventory.Count){
			_heldItemIndex = 0;
		}

		//apply held effects, like the lighter remaining on
		if(_inventory[_heldItemIndex] != null){
			_inventory[_heldItemIndex].HeldEffect();
		}

		//don't let the player do any new actions while a coroutine is running
		if(_runningCoroutine){
			return;
		}
		             
		float activateItemButton = Input.GetAxis("Fire1");
	    float switchItemButton = Input.GetAxis("Mouse ScrollWheel");
		float dropItemButton = Input.GetAxis("Fire3");

		if(switchItemButton != 0){
			if(switchItemButton > 0){
				_incrementInventorySlot = true;
			}else{
				_incrementInventorySlot = false;
			}
			StartCoroutine("SwapItem");
			return;
		}

		if(activateItemButton != 0 && _prevFire1 == 0){
			StartCoroutine("UseItem");
			_prevFire1 = activateItemButton;
			return;
		}
		_prevFire1 = activateItemButton;

		if(dropItemButton != 0){
			DropItem(_inventory[_heldItemIndex]);
	
			//if we're holding any other items swap to them
			if(_inventory.Count != 0){
				_heldItemIndex--;
				StartCoroutine("SwapItem");
			}
		}
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
		oldItem.collider.isTrigger = false;
		oldItem.rigidbody.isKinematic = false;
		oldItem.transform.parent = null;

		_inventory.Remove(oldItem);

		RecalculateGUI();
	}

	void EnableItem(int index){
		int curIndex = 0;
		foreach(ItemScript iter in _inventory){
			if(curIndex == index){
				iter.gameObject.SetActive(true);
			}else{
				iter.gameObject.SetActive(false);
			}
			curIndex++;
		}
	}

	void PickupItem(){
		//don't let players do this if we're swapping or using an item
		if(_runningCoroutine){
			return;
		}

		//cast a sphere around our hand, and check all the found colliders for takeable items
		Collider[] foundColliders = Physics.OverlapSphere(heldItemTrans.position, _pickupRadious);  
		ItemScript closestFoundScript = null;

		foreach(Collider iter in foundColliders){
			ItemScript foundScript = iter.GetComponent<ItemScript>();

			if(foundScript != null){
				//make sure this item isn't one of the ones we're holding
				foreach(ItemScript item in _inventory){
					if(item == foundScript){
						foundScript = null;
						break;
					}
				}

				if(foundScript == null){
					continue;
				}

				//I'm assuming the player will usually want to pick up the item closest to their pickup point
				if(closestFoundScript == null){
					closestFoundScript = foundScript;
				}else if(Vector3.SqrMagnitude(foundScript.transform.position - heldItemTrans.position) < Vector3.SqrMagnitude(closestFoundScript.transform.position - heldItemTrans.position)){
					closestFoundScript = foundScript;
				}
			}
		}

		//nothing to pick up, nothing to do here. 
		if(closestFoundScript == null){
			return;
		}
        
        // Check to see item matches!
        // If item matches another corresponding item
        // execute the item event.


		//if we don't have any items yet, just pick the thing up
		if(_inventory.Count == 0){
			_heldItemIndex = 0;
			PrepareItemForStorage(closestFoundScript);
			RecalculateGUI();
			return;
		}

		//if out inventory isn't full yet, add the new item to our inventory and then switch to it
		if(_inventory.Count < _inventorySlots){
			//pick up the new item
			PrepareItemForStorage(closestFoundScript);
			//set our held index to our new item
			_heldItemIndex = _inventory.Count - 1;

			EnableItem(_heldItemIndex);
			RecalculateGUI();
			return;
		}

		//finally, if our inventory is full drop the current item and pick up the new one
		DropItem(_inventory[_heldItemIndex]);
		//pick up the new item
		PrepareItemForStorage(closestFoundScript);
		//set our held index to our new item
		_heldItemIndex = _inventory.Count - 1;

		closestFoundScript.OnPickup();

		EnableItem(_heldItemIndex);
		
		RecalculateGUI();

	}
	

	//changes the images in the inventory box based on heald items
	void RecalculateGUI(){
		for(int i = 0; i < inventoryBoxManager.guiImages.Count; i++){
			if(i < _inventory.Count){
				inventoryBoxManager.guiImages[i].sprite = _inventory[i].mySprite;
				inventoryBoxManager.guiImages[i].enabled = true;
			}else{
				inventoryBoxManager.guiImages[i].enabled = false;
			}
		}

		HighlightEquiptItem();
	}

	//make the item we're currently holding more obvious
	void HighlightEquiptItem(){
		inventoryBoxManager.HighlightImage(_heldItemIndex);
	}

	void MoveItemSlot(){
		if(_itemSlotDestination == null){
			_itemSlotDestination = heldItemTrans;
		}

		itemSlot.position = Vector3.Lerp(itemSlot.position, _itemSlotDestination.position, Time.deltaTime * _itemSlotSpeed);
		itemSlot.rotation = Quaternion.Slerp(itemSlot.rotation, _itemSlotDestination.rotation, Time.deltaTime * _itemSlotSpeed);
	}

	// Update is called once per frame
	void Update () {
		UpdateItem();

		//if the fire button was pressed this frame try to pick up an item
		float curFire2 = Input.GetAxis("Fire2");
		if(curFire2 != 0 && _prevFire2 == 0){
			PickupItem();
		}
		_prevFire2 = curFire2;

		MoveItemSlot();
	}
}
