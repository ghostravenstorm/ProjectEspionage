using System;
using System.Collections;
using UnityEngine;

// -- Obsolete. Use AgentInputManager.

public class MainController : MonoBehaviour{

	public float movementSpeed = 3f;
	public float climbModifier = 0.8f;
	public float sprintModifier = 1.8f;
	public float sneakModifier = 0.5f;
	public GameObject ropePointRef;
	private GameObject ropePointInstance;

	private new Rigidbody rigidbody;

	public IAgentController controller;
	private IAgentController prevController;

	public float sprintMeter = 1f;

	void Start(){
		rigidbody = this.GetComponent<Rigidbody>();

		controller = new NormalController(movementSpeed, sprintModifier, true);

		StartCoroutine("SprintMeter");
	}

	void Update(){
		controller.UpdateController(rigidbody);

		//Debug.Log("current controller: " + controller);
		//Debug.Log("saved controller: " + prevController);

		if(Input.GetButtonDown("Sneak") && controller.isGrounded)
			ToggleSneakMode();
	}

	void OnCollisionStay(Collision c){
		if(c.gameObject.tag == "Ground" || c.gameObject.tag == "Bridge")
			controller.isGrounded = true;
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "Ground" || c.gameObject.tag == "Bridge"){
			controller.isGrounded = true;
		}
	}

	void OnCollisionExit(Collision c){
		if(c.gameObject.tag == "Ground" || c.gameObject.tag == "Bridge"){
			controller.isGrounded = false;
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Rope" && controller.state != AgentState.Sneaking)
			EnableClimbModeRope(c.gameObject);
		if(c.gameObject.tag == "Ladder" && controller.state != AgentState.Sneaking)
			EnableClimbModeLadder(c.gameObject);
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Rope")
			DisableClimbModeRope(c.gameObject);
		if(c.gameObject.tag == "Ladder")
			DisableClimbModeLadder();
	}

	private void ToggleSneakMode(){
		if(controller is ClimbingController)
			return;
		if(controller is SneakController)
			controller = new NormalController(movementSpeed, sprintModifier, true);
		else
			controller = new SneakController(movementSpeed, sneakModifier);
	}

	private void EnableClimbModeRope(GameObject obj){
		rigidbody.useGravity = false;
		ropePointInstance = (GameObject)Instantiate(ropePointRef, obj.transform.position, obj.transform.rotation);
		this.transform.SetParent(ropePointInstance.transform);
		controller = new ClimbingController(movementSpeed, climbModifier, ropePointInstance, obj);
	}

	private void DisableClimbModeRope(GameObject obj){
		rigidbody.useGravity = true;
		ropePointInstance.transform.DetachChildren();
		Destroy(ropePointInstance);
		controller = new NormalController(movementSpeed, sprintModifier, true);
	}

	private void EnableClimbModeLadder(GameObject obj){
		rigidbody.useGravity = false;
		controller = new ClimbingController(movementSpeed, climbModifier, null, obj);
	}

	private void DisableClimbModeLadder(){
		rigidbody.useGravity = true;
		controller = new NormalController(movementSpeed, sprintModifier, true);
	}

	public void PauseController(AgentState state){
		//prevController = controller;
		controller = new NullController(state);
	}

	public void ResumeController(){
		controller = new NormalController(movementSpeed, sprintModifier, true);
	}

	private IEnumerator SprintMeter(){

		while(true){
			//Debug.Log("SprintMeter is runnning");

			if(Input.GetButton("Sprint") && controller.state == AgentState.Running){
				yield return new WaitForSeconds(0.1f);
				sprintMeter -= 0.06f;
				if(sprintMeter <= 0){
					sprintMeter = 0f;
					controller.isSprintExhausted = true;
				}
			}
			else{
				yield return new WaitForSeconds(0.1f);
				sprintMeter += 0.02f;
				if(sprintMeter >= 1) sprintMeter = 1f;
			}

			GUIManager.instance.UpdateSprintMeter(sprintMeter);
		}
	}
}
