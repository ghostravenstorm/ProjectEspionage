using System;
using UnityEngine;

public class MainController : MonoBehaviour{

	public float movementSpeed = 3f;
	public float climbModifier = 0.8f;
	public float sprintModifier = 1.5f;
	public float sneakModifier = 0.5f;

	private new Rigidbody rigidbody;

	public IController controller;

	private IController prevController;

	void Start(){
		rigidbody = this.GetComponent<Rigidbody>();

		controller = new NormalController(movementSpeed, sprintModifier);
	}

	void Update(){
		controller.Update(rigidbody);

		if(Input.GetButtonDown("Sneak")) ToggleSneakMode();

		StateCheck();
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Ground")
			controller.isGrounded = true;
	}

	void OnCollisionExit(Collision collision){

	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Climbable" && controller.state != PlayerState.Sneaking)
			EnableClimbMode();
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "Climbable")
			DisableClimbMode();
	}

	private void ToggleSneakMode(){
		if(controller is ClimbingController) return;
		if(controller is SneakController) controller = new NormalController(movementSpeed, sprintModifier);
		else controller = new SneakController(movementSpeed, sneakModifier);
	}

	private void EnableClimbMode(){
		rigidbody.useGravity = false;
		controller = new ClimbingController(movementSpeed, climbModifier);
	}

	private void DisableClimbMode(){
		rigidbody.useGravity = true;
		controller = new NormalController(movementSpeed, sprintModifier);
	}

	public void pauseController(){
		prevController = controller;
		controller = new NullController(PlayerState.Dead);
	}

	public void resumeController(){
		controller = prevController;
	}

	private void StateCheck(){
		/*
		if(rigidbody.velocity.y <= 0 && controller is ClimbingController) 
			controller.state = PlayerState.Falling;
		if(rigidbody.velocity.y <= 0 && controller is ClimbingController)
			controller.state = PlayerState.Jumping;

		if(controller.state == PlayerState.Falling) controller = new NormalController(movementSpeed, sprintModifier);
		*/
	}
}
