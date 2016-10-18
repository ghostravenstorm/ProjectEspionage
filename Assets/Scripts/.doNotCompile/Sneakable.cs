using UnityEngine;
using System.Collections;

public class Sneakable : MonoBehaviour{

	public bool isSneaking{ private set; get; }
	public float sneakSpeedModifier;
	private float normalSpeed;

	void Update(){
		if(Input.GetButtonDown("Sneak")){
			if(isSneaking){
				DisableSneak();
			}
			else{
				EnableSneak();
			}
		}
	}

	private void EnableSneak(){
		Debug.Log("Sneaking.");
		isSneaking = true;

		if(this.GetComponent<Player>()){
			var player = this.GetComponent<Player>();
			normalSpeed = player.speed;
			player.speed *= sneakSpeedModifier;
		}
	}

	private void DisableSneak(){
		Debug.Log("Not Sneaking.");
		isSneaking = false;

		if(this.GetComponent<Player>()){
			var player = this.GetComponent<Player>();
			player.speed = normalSpeed;
		}
	}
}