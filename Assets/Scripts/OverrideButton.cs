using UnityEngine;

public class OverrideButton : MonoBehaviour, IInputController{
	
	[Tooltip("This being the link to which ever camera or laser grid to be disabled by this button.")]
	public GameObject[] objectsToOverride;

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
		for(int i = 0; i < objectsToOverride.Length; i++){
			objectsToOverride[i].GetComponent<IOverrideable>().Disable();
		}
		//play agent button pressing animation
	}

	public void OnSubmit(){}
	public void OnUpArrow(){}
	public void OnDownArrow(){}
}