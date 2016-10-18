using System;
using UnityEngine;

public interface IController{

	PlayerState state{ get; }
	float speed{ get; }
	float modifier{ get; }
	bool isGrounded{ set; get; }
	
	void Update(Rigidbody rigidbody);
}