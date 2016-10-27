using UnityEngine;
using System.Collections;

public class LightDetector : MonoBehaviour{

	public bool isDetectable;
	public LightState state;

	private MainController maincontroller;

	void Start(){
		StartCoroutine("StateCheck");
		maincontroller = (GetComponentInParent(typeof(MainController)) as MainController);
		//Debug.Log(maincontroller.controller);
	}

	private IEnumerator StateCheck(){

		while(true){
			if(state == LightState.Darkness) isDetectable = false;
			if(state == LightState.SemiLight && maincontroller.controller.state == PlayerState.Sneaking) isDetectable = false;
			if(state == LightState.SemiLight && maincontroller.controller.state != PlayerState.Sneaking) isDetectable = true;
			if(state == LightState.FullLight) isDetectable = true;
			//Debug.Log("Detectable: " + isDetectable);
			
			yield return null;
		}
	}

	void Update(){
		
	}

	void OnTriggerEnter2D(Collider2D collider){

		if(collider.gameObject.tag == "FullLight"){
			state = LightState.FullLight;
			//Debug.Log("Light State: Full");
			//Debug.Log("Detectable:" + isDetectable);
		}
		if(collider.gameObject.tag == "SemiLight"){
			state = LightState.SemiLight;
			//Debug.Log("Light State: Semi");
			//if(maincontroller.controller.state == PlayerState.Sneaking) Debug.Log("Stealth: Not Detectable");
			//else Debug.Log("Detectable: " + isDetectable);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "FullLight"){
			state = LightState.Darkness;
			//Debug.Log("Light State: Darkness");
		}
		if(collider.gameObject.tag == "SemiLight"){
			state = LightState.Darkness;
			//Debug.Log("Light State: Darkness");
		}
		//Debug.Log("Detectable: " + isDetectable);
	}
}