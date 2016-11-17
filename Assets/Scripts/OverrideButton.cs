using UnityEngine;
using System.Collections;

public class OverrideButton : MonoBehaviour, IInputController{
	
	[Tooltip("This being the link to which ever camera or laser grid to be disabled by this button.")]	public GameObject[] objectsToOverride;
	public Sprite active;
	public Sprite disabled;
	public float resetTimer = 2f;
	public bool isResettable = true;

	private bool isActive = true;

	void Start(){
		GetComponent<AudioSource>().volume = 2f;
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Player"){
			InputManager.instance.secondaryInputController = this;
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Player"){
			InputManager.instance.secondaryInputController = new NullInputController();
		}
	}

	public void OnAgentInteract(){
		for(int i = 0; i < objectsToOverride.Length; i++){
			if(isActive){
				GetComponent<AudioSource>().Play();
				GetComponent<SpriteRenderer>().sprite = disabled;
				if(isResettable) StartCoroutine("ResetButton");
				objectsToOverride[i].GetComponent<IOverrideable>().Disable();
			}
		}
		isActive = false;
		//play agent button pressing animation
	}

	private IEnumerator ResetButton(){
		yield return new WaitForSeconds(resetTimer);
		isActive = true;
		GetComponent<SpriteRenderer>().sprite = active;
	}

	public void OnSubmit(){}
	public void OnUpArrow(){}
	public void OnDownArrow(){}
}