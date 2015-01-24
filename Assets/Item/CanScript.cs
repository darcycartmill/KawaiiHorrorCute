using UnityEngine;
using System.Collections;

public class CanScript : ItemScript {
	public override void HeldEffect(){}
	
	public override void ApplyEffect(){}

	public override void OnPickup(){

        //If user owns a Can Opener
        if (GameObject.Find("Player").GetComponent<InventoryManager>().KillItemByName("Can Opener"))
        {
            // DESTROY THE CAN

            // EXECUTE EVENT WITH HEAD DROPPING OUT

            // DROP OUT STATUE FROM HEAD
        }
    }

}
