using UnityEngine;
using System.Collections;
//an atempt to make a player that looks like it has footsteps
public class MovementScript : MonoBehaviour {
	public Transform cameraTrans;

	float _maxSpeed;
	float _accelSpeed;

	float _stepHeight;

	bool _stepping = false;
	Vector3 _moveVec;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float _horizontal = Input.GetAxis("Horizontal");
		float _vertical = Input.GetAxis("Vertical");
		Vector3 inputVec = new Vector3(_horizontal, 0, _vertical);
		//this is an experiment in character movment
		if(!_stepping){
			_moveVec = inputVec;
		}else{

		}
	}
}
