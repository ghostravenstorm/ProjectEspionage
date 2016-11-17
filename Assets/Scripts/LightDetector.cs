using UnityEngine;
using System.Collections;

public class LightDetector : MonoBehaviour{

	public bool isDetectable;
	public LightState state;

	private MainController maincontroller;

	void Start(){
		StartCoroutine("StateCheck");
		maincontroller = (GetComponentInParent(typeof(MainController)) as MainController);
	}

	private IEnumerator StateCheck(){

		if(state == LightState.Darkness) isDetectable = false;
		if(state == LightState.SemiLight && maincontroller.controller.state == PlayerState.Sneaking) isDetectable = false;
		if(state == LightState.SemiLight && maincontroller.controller.state != PlayerState.Sneaking) isDetectable = true;
		if(state == LightState.FullLight) isDetectable = true;
			
		yield return StartCoroutine("StateCheck");
	}

	void Update(){
		if(isDetectable) GUIManager.instance.setDetector(true);
		else GUIManager.instance.setDetector(false);
		Debug.Log(state);		
	}

	//void OnTriggerStay2D
	// TODO: light state needs to update if/when flickering is enabled.

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "FullLight" /*&& c.GetComponent<LightProjector>().isActive*/){
			state = LightState.FullLight;
		}
		if(c.gameObject.tag == "SemiLight" /*&& c.GetComponent<LightProjector>().isActive*/){
			state = LightState.SemiLight;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.tag == "FullLight"){
			state = LightState.Darkness;
		}
		if(c.gameObject.tag == "SemiLight"){
			state = LightState.Darkness;
		}
	}
}