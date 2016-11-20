using UnityEngine;
using System.Collections;

public class AgentInputController : InputController{

	public float movementSpeed = 3f;
	public float jumpForce = 5f;
	public float climbModifier = 0.8f;
	public float sprintModifier = 1.8f;
	public float sneakModifier = 0.5f;
	public float sprintMeter = 1f;
	public bool isGrounded = false;

	public AgentState state;

	private float currentSpeed;

	void Start(){
		//state = AgentState.Standing;
		currentSpeed = movementSpeed;
		InputManager.instance.mainInput = this;
	}

	void Update(){
		Debug.Log(state);
		//Debug.Log(isGrounded);
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "Ground"){
			isGrounded = true;
			state = AgentState.Standing;
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Ladder"){
			
		}
	}

	public override void OnMoveRight(){
		if(state != AgentState.Jumping)
			state = AgentState.Walking;
		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(currentSpeed, rb.velocity.y, 0);
	}

	public override void OffMoveRight(){
		if(state == AgentState.Walking)
			state = AgentState.Standing;

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
	}

	public override void OnMoveLeft(){
		if(state != AgentState.Jumping)
			state = AgentState.Walking;
		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(currentSpeed * -1, rb.velocity.y, 0);	
	}

	public override void OffMoveLeft(){
		if(state == AgentState.Walking)
			state = AgentState.Standing;
			
		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
	}

	public override void OnJump(){
		if(state == AgentState.Sneaking)
			return;
		else if(isGrounded){
			isGrounded = false;
			state = AgentState.Jumping;
			var rb = GetComponent<Rigidbody>();
			rb.AddForce(rb.velocity.x, jumpForce, 0, ForceMode.Impulse);
		}
	}

	public override void OnSprint(){
		var rb = GetComponent<Rigidbody>();

		if(state == AgentState.Jumping)
			return;
		else if(rb.velocity != Vector3.zero)
			state = AgentState.Running;
		else
			state = AgentState.Standing;

		currentSpeed = movementSpeed * sprintModifier;
	}

	public override void OffSprint(){
		currentSpeed = movementSpeed;
	}

	public override void OnSneak(){
		if(state == AgentState.Sneaking || state == AgentState.Walking || state == AgentState.Standing){
			state = AgentState.Sneaking;
			currentSpeed = movementSpeed * sneakModifier;
		}
	}

	public override void OffSneak(){
		state = AgentState.Standing;
		currentSpeed = movementSpeed;
	}
}	