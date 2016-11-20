using System;
using UnityEngine;

public class NullController : IAgentController{
	
	public AgentState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }
	public bool isSprintExhausted{set; get;}
	public bool didJump{get; set;}

	public NullController(AgentState state){
		this.state = state;
	}

	public void UpdateController(Rigidbody rigidbody){

	}
}