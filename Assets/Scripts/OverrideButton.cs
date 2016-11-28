using UnityEngine;
using System.Collections;

public class OverrideButton : InputController{

	[Tooltip("This being the link to which ever camera or laser grid to be disabled by this button.")]
	public GameObject[] objectsToOverride;
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
			InputManager.instance.secondaryInput = this;
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Player"){
			InputManager.instance.secondaryInput = new NullInputController();
		}
	}

	public override void OnAgentInteract(){

		if(isActive){
			GetComponent<AudioSource>().Play();
			GetComponent<SpriteRenderer>().sprite = disabled;
			if(isResettable) StartCoroutine("ResetButton");
			for(int i = 0; i < objectsToOverride.Length; i++){
				objectsToOverride[i].GetComponent<IOverrideable>().Disable();
			}
			isActive = false;
		}
	}
		//play agent button pressing animation

	private IEnumerator ResetButton(){
		yield return new WaitForSeconds(resetTimer);
		isActive = true;
		GetComponent<SpriteRenderer>().sprite = active;
	}
}
