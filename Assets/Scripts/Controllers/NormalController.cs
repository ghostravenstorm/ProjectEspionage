using System;
using UnityEngine;

public class NormalController : IController{

	public PlayerState state{ private set; get; }

	public float speed{ private set; get; }
	public float modifier{ private set; get; }
	public bool isGrounded{ set; get; }

    private float jumpForce = 5f;

    public NormalController(float speed, float modifier, bool groundState){
		this.state = PlayerState.Standing;
		this.speed = speed;
		this.modifier = modifier;
		this.isGrounded = groundState;
        //Debug.Log("Normal controller active.");
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

		//Debug.Log(rigidbody.velocity.y);

		if(Input.GetButtonDown("Jump") /*&& rigidbody.velocity.y > 0*/){
			state = PlayerState.Jumping;
		}
		else if(Input.GetButton("Sprint") && rigidbody.velocity.x != 0){
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