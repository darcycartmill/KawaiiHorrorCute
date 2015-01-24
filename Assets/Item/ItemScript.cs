using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public virtual void HeldEffect(){}

	public virtual void ApplyEffect(){}

	private string _itemName;

	public string ItemName {
		get { return _itemName; }
		set { _itemName = value; }
	}

	// Use this for initialization
	void Start () {
		_itemName = "Empty Item";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
