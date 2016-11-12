using System;
using System.Collections;
using UnityEngine;

public class NormalController : IController{

	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }
	public bool isSprintExhausted{set; get;}

    private float jumpForce = 5f;

    public NormalController(float speed, float modifier, bool groundState){
		this.state = PlayerState.Standing;
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

		if(Input.GetButtonDown("Jump") /*&& rigidbody.velocity.y > 0*/){
			state = PlayerState.Jumping;
		}
		else if(Input.GetButton("Sprint") && rigidbody.velocity.x != 0 && !isSprintExhausted){
			if(Input.GetButtonDown("Jump"))
				state = PlayerState.Jumping;
			else
				state = PlayerState.Running;
		}
		else if(rigidbody.velocity.x != 0){
			if(Input.GetButtonDown("Jump"))
				state = PlayerState.Jumping;
			else
				state = PlayerState.Walking;
		}
		else{
			state = PlayerState.Standing;
		}
	}
}