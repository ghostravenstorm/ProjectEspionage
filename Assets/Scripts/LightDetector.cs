using UnityEngine;
using System.Collections;

public class LightDetector : MonoBehaviour{

	public bool isDetectable;
	// -- Change this variable in editor to set global light state
	public LightState defaultLightState;
	public LightState currentLightState;

	private AgentInputController controller;

	void Start(){
		controller = (GetComponentInParent(typeof(AgentInputController)) as AgentInputController);
	}

	void Update(){

		if(controller.state == AgentState.Running)
			isDetectable = true;
		else if(currentLightState == LightState.Darkness)
			isDetectable= false;
		//else if(currentLightState == LightState.Darkness && controller.state != AgentState.Sneaking)
		//	isDetectable = true;
		//else if(currentLightState == LightState.Darkness && controller.state == AgentState.Sneaking)
		//	isDetectable = false;
		else if(currentLightState == LightState.SemiLight && controller.state == AgentState.Sneaking)
			isDetectable = false;
		else if(currentLightState == LightState.SemiLight && controller.state != AgentState.Sneaking)
			isDetectable = true;
		else if(currentLightState == LightState.FullLight)
			isDetectable = true;

		if(isDetectable) GUIManager.instance.setDetector(true);
		else GUIManager.instance.setDetector(false);
		
		//Debug.Log(state);		
	}

	//void OnTriggerStay2D
	// -- TODO: light state needs to update if/when flickering is enabled.

	void OnTriggerEnter2D(Collider2D c){

		if(c.gameObject.tag == "FullLight" /*&& c.GetComponent<LightProjector>().isActive*/){
			currentLightState = LightState.FullLight;
		}
		if(c.gameObject.tag == "SemiLight" /*&& c.GetComponent<LightProjector>().isActive*/){
			currentLightState = LightState.SemiLight;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.gameObject.tag == "FullLight"){
			currentLightState = defaultLightState;
		}
		if(c.gameObject.tag == "SemiLight"){
			currentLightState = defaultLightState;
		}
	}
}