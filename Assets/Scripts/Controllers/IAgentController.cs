using System;
using UnityEngine;

public interface IAgentController{

	AgentState state{ get; }
	float speed{ get; }
	float modifier{ get; }
	bool isGrounded{ set; get; }
	bool isSprintExhausted{set; get;}
	bool didJump{set; get;}
	
	void UpdateController(Rigidbody rigidbody);
}