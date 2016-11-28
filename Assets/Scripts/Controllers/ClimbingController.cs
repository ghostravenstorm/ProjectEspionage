using System;
using UnityEngine;

// -- Obsolete. Use AgentInputManager.

public class ClimbingController : IAgentController{
	
	public AgentState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }
	public bool isSprintExhausted{set; get;}
	public bool didJump{set; get;}

	private float jumpForce = 3f;
	private GameObject climbable;
	private GameObject ropePoint;

	public ClimbingController(float speed, float modifier, GameObject ropePoint, GameObject climbable){
		if(climbable.tag == "Rope") this.state = AgentState.ClimbingRope;
		if(climbable.tag == "Ladder") this.state = AgentState.ClimbingLadder;
		if(climbable.tag == "Ledge") this.state = AgentState.ClimbingLedge;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = true;
		this.ropePoint = ropePoint;
		this.climbable = climbable;
		//Debug.Log("Climbing controller active.");
	}

	public void UpdateController(Rigidbody rigidbody){

		if(state == AgentState.ClimbingRope){
			ropePoint.transform.position = new Vector3(
				climbable.transform.position.x, climbable.transform.position.y, ropePoint.transform.position.z
			);
		}

		float vectorX = Input.GetAxis("Horizontal");
		float vectorY = Input.GetAxis("Vertical");

		vectorX *= speed * modifier;
		vectorY *= speed * modifier;

		rigidbody.velocity = new Vector3(vectorX, vectorY, 0);

		if(Input.GetButtonDown("Jump") && isGrounded){
			isGrounded = false;
			vectorX = rigidbody.velocity.x;
			rigidbody.AddForce(vectorX, jumpForce, 0, ForceMode.Impulse);
		}

	}
}