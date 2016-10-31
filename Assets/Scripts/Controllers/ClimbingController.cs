using System;
using UnityEngine;

public class ClimbingController : IController{
	
	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }

	private float jumpForce = 3f;

	public ClimbingController(float speed, float modifier){
		this.state = PlayerState.Climbing;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = true;
		Debug.Log("Climbing controller active.");
	}

	public void Update(Rigidbody rigidbody){

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