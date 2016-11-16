using System;
using UnityEngine;

public class NullController : IController{
	
	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }
	public bool isSprintExhausted{set; get;}
	public bool didJump{get; set;}

	public NullController(PlayerState state){
		this.state = state;
	}

	public void UpdateController(Rigidbody rigidbody){

	}
}