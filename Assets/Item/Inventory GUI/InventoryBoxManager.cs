using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryBoxManager : MonoBehaviour {

	public List<Image> guiImages;

	public void HighlightImage(int index){
		for(int i = 0; i < guiImages.Count; i++){
			if(i != index){
				guiImages[i].color = Color.gray;
			}else{
				guiImages[i].color = Color.white;
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
