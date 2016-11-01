using System;
using UnityEngine;

public class ClimbingController : IController{
	
	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }

	private float jumpForce = 3f;
	private GameObject climbable;
	private GameObject ropePoint;

	public ClimbingController(float speed, float modifier, GameObject ropePoint, GameObject climbable){
		if(climbable.tag == "Rope") this.state = PlayerState.ClimbingRope;
		if(climbable.tag == "Ladder") this.state = PlayerState.ClimbingLadder;
		if(climbable.tag == "Ledge") this.state = PlayerState.ClimbingLedge;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = true;
		this.ropePoint = ropePoint;
		this.climbable = climbable;
		//Debug.Log("Climbing controller active.");
	}

	public void Update(Rigidbody rigidbody){

		if(state == PlayerState.ClimbingRope){
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