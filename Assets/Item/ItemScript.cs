using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public Sprite mySprite;

	public virtual void HeldEffect(){}

	public virtual void ApplyEffect(){}

	public virtual void OnPickup(){}

	public virtual void OnDrop(){}

	public virtual void Kill(){
		Destroy(gameObject);
	}
}
