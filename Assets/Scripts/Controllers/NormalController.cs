using System;
using System.Collections;
using UnityEngine;

public class NormalController : IAgentController{

	public AgentState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }
	public bool isSprintExhausted{set; get;}
    public bool didJump{set; get;}

    private float jumpForce = 5f;

    public NormalController(float speed, float modifier, bool groundState){
		this.state = AgentState.Standing;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = groundState;
        //Debug.Log("Normal controller active.");
    }

    public void UpdateController(Rigidbody rigidbody){

        float vectorX = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Sprint"))
        	isSprintExhausted = false;

		if(Input.GetButton("Sprint") && !isSprintExhausted)
			vectorX *= this.speed * this.modifier;
		else
			vectorX *= this.speed;
		
		rigidbody.velocity = new Vector3(vectorX, rigidbody.velocity.y, 0);

		if(Input.GetButtonDown("Jump") && isGrounded){
			isGrounded = false;
			vectorX = rigidbody.velocity.x;
			rigidbody.AddForce(vectorX, jumpForce, 0, ForceMode.Impulse);
		}

		//Debug.Log(rigidbody.velocity.y);

		if(Input.GetButtonDown("Jump")){
			state = AgentState.Jumping;
		}
		else if(Input.GetButton("Sprint") && rigidbody.velocity.x != 0 && !isSprintExhausted){
			if(Input.GetButtonDown("Jump"))
				state = AgentState.Jumping;
			else
				state = AgentState.Running;
		}
		else if(rigidbody.velocity.x != 0){
			if(Input.GetButtonDown("Jump"))
				state = AgentState.Jumping;
			else
				state = AgentState.Walking;
		}
		else{
			state = AgentState.Standing;
		}
	}
}




