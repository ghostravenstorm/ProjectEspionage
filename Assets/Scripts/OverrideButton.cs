using UnityEngine;

public class OverrideButton : MonoBehaviour, IInputController{
	
	public GameObject objectToOverride;

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			InputManager.instance.secondaryInputController = this;
		}
	}

	void OnTriggerExit(Collider colldier){
		if(GetComponent<Collider>().gameObject.tag == "Player"){
			InputManager.instance.secondaryInputController = new NullInputController();
		}
	}

	public void OnAgentInteract(){
		objectToOverride.GetComponent<IOverrideableObject>().Disable();
		//play agent button pressing animation
	}

	public void OnSubmit(){}
	public void OnUpArrow(){}
	public void OnDownArrow(){}
}