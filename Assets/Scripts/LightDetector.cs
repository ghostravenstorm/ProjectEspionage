using UnityEngine;
using System.Collections;

public class LightDetector : MonoBehaviour{

	public bool isDetectable;
	public LightState state;

	void Start(){
		StartCoroutine("StateCheck");
	}

	private IEnumerator StateCheck(){

		var controller = this.GetComponent<MainController>().controller;

		while(true){
			if(state == LightState.Darkness) isDetectable = false;
			if(state == LightState.SemiLight && controller.state == PlayerState.Sneaking) isDetectable = false;
			if(state == LightState.SemiLight && controller.state != PlayerState.Sneaking) isDetectable = true;
			if(state == LightState.FullLight) isDetectable = true; 
			return null;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "FullLight") state = LightState.FullLight;
		if(collider.gameObject.tag == "SemiLight") state = LightState.SemiLight;
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "FullLight") state = LightState.Darkness;
		if(collider.gameObject.tag == "SemiLight") state = LightState.Darkness;
	}

	/*
	public bool isDetectable{ private set; get; }
	public bool isInLightMain{ private set; get; }
	public bool isInLightFallOff{ private set; get; }

	void Start(){
		isDetectable = true;
	}

	void Update(){
		var player = transform.parent.GetComponent<Sneakable>();

		if(Input.GetButtonDown("Sneak") && !player.isSneaking){
			isDetectable = false;
			Debug.Log("Is detectable: " + isDetectable);
		}
		
		if(Input.GetButtonDown("Sneak") && player.isSneaking){
			if(!isInLightMain){
				isDetectable = true;
			}
			Debug.Log("Is detectable: " + isDetectable);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {

		var player = transform.parent.GetComponent<Sneakable>();

		if(collider.gameObject.tag == "LightMain"){
			isDetectable = true;
			isInLightMain = true;
			//Debug.Log("Is detectable: " + isDetectable);
			//Debug.Log("Is in main light: " + isInLightMain);
		}

		if(collider.gameObject.tag == "LightFallOff"){
			isInLightFallOff = true;

			if(!player.isSneaking){
				isDetectable = true;
				//Debug.Log("Is detectable: " + isDetectable);
			}
			//Debug.Log("Is in fall off light: " + isInLightFallOff);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "LightMain"){
			isInLightMain = false;
			//Debug.Log("Is in main light: " + isInLightMain);
		}

		if(collider.gameObject.tag == "LightFallOff"){
			isInLightFallOff = false;
			//Debug.Log("Is in fall off light: " + isInLightFallOff);
			//Debug.Log("Is detectable: " + isDetectable);
		}
	}
	*/
}