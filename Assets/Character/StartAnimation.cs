using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartAnimation : MonoBehaviour {

	public GameObject player;

	//public Transform pos;
	public Transform destination;

	public Image gui;

	// Use this for initialization
	void Start () {
		Vector3 pos = destination.position;
		pos.y -= 1;
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {

		Color black = gui.color;
		black.a -= Time.deltaTime;
		gui.color = black;
		/*
		if(pos != null){
			pos.parent = null;
			transform.position = Vector3.Lerp(transform.position, pos.position, 0.006f);
			transform.rotation = Quaternion.Slerp(transform.rotation, pos.rotation, 0.006f);
			if(Vector3.Distance(transform.position, pos.position) < 0.1f){
				Destroy(pos.gameObject);
			}
		}
		*/
		if(Vector3.Distance(transform.position, destination.position) > 0.6f){
			transform.position = Vector3.Lerp(transform.position, destination.position, 0.01f);
			transform.rotation = Quaternion.Slerp(transform.rotation, destination.rotation, 0.1f);
		}else{
			transform.position = Vector3.Lerp(transform.position, destination.position, 0.1f);
			transform.rotation = Quaternion.Slerp(transform.rotation, destination.rotation, 0.1f);
			if(Vector3.Distance(transform.position, destination.position) < 0.1f){
				player.SetActive(true);
				Destroy(gui.gameObject);
				Destroy(gameObject);
			}
		}

	}
}
