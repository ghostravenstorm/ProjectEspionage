using System;
using UnityEngine;

public class SneakController : IController{

	public PlayerState state{ private set; get; }
	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }

	public SneakController(float speed, float modifier){
		state = PlayerState.Sneaking;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = true;

		

		//Debug.Log("Sneak Active");
	}

	public void Update(Rigidbody rigidbody){

		float vectorX = Input.GetAxis("Horizontal");

		vectorX *= speed * modifier;
		
		rigidbody.velocity = new Vector3(vectorX, rigidbody.velocity.y, 0);
	}
}