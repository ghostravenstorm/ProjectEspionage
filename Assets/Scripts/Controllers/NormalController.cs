using System;
using UnityEngine;

public class NormalController : IController{

	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }

	private float jumpForce = 5f;

	public NormalController(float speed, float modifier){
		this.state = PlayerState.Standing;
		this.speed = speed;
		this.modifier = modifier;
		//Debug.Log("Normal Active");
	}

	public void Update(Rigidbody rigidbody){

		float vectorX = Input.GetAxis("Horizontal");

		if(Input.GetButton("Sprint")) vectorX *= this.speed * this.modifier;
		else vectorX *= this.speed;
		
		rigidbody.velocity = new Vector3(vectorX, rigidbody.velocity.y, 0);

		if(Input.GetButtonDown("Jump") && isGrounded){
			isGrounded = false;
			vectorX = rigidbody.velocity.x;
			rigidbody.AddForce(vectorX, jumpForce, 0, ForceMode.Impulse);
		}

		if(rigidbody.velocity == Vector3.zero) state = PlayerState.Standing;
		else if(Input.GetButton("Sprint")) state = PlayerState.Running;
		else state = PlayerState.Walking;
	}
}