using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyAnimationController : MonoBehaviour{
	
	private Animator animator;
	private SpriteRenderer sprite;

	public bool isFacingRight;
	public float standAnimationSpeed = 1f;
	public float walkAnimationSpeed = 1f;
	public float jumpAnimationSpeed = 1f;
	public float sprintAnimationSpeed = 1f;
	public float sneakAnimationSpeed = 0.6f;

	void Start(){
		animator = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	void Update(){

		var controller = this.GetComponent<MainController>().controller;

		//Debug.Log(controller.state);
		Debug.Log(controller.isGrounded);

		// Moving right. //
		if(Input.GetAxis("Horizontal") >= 1)
			isFacingRight = true;
		// Moving left. //
		else if(Input.GetAxis("Horizontal") <= -1)
			isFacingRight = false;

		if(controller.state == PlayerState.Walking){
			toggleOffOtherBools("IsWalking");
			animator.SetBool("IsWalking", true);
			animator.speed = walkAnimationSpeed;
		}
		else if(controller.state == PlayerState.Running){
			toggleOffOtherBools("IsSprinting");
			animator.SetBool("IsSprinting", true);
			animator.speed = sprintAnimationSpeed;
		}
		else if(controller.state == PlayerState.Standing){
			toggleOffOtherBools("IsStanding");
			animator.SetBool("IsStanding", true);
			animator.speed = standAnimationSpeed;
		}
		else if(controller.state == PlayerState.Sneaking){
			toggleOffOtherBools("IsSneaking");
			animator.SetBool("IsSneaking", true);
			animator.speed = sneakAnimationSpeed;

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}
		else if(controller.state == PlayerState.Jumping){
			animator.SetTrigger("Jump");
			animator.speed = jumpAnimationSpeed;
		}


		/*
		


		if(controller.state == PlayerState.Running){
			animator.SetBool("IsSprinting", true);
			animator.speed = sprintAnimationSpeed;
		}
		else animator.SetBool("IsSprinting", false);


		if(controller.state == PlayerState.Sneaking){
			animator.SetBool("IsSneaking", true);
			animator.speed = sneakAnimationSpeed;

			// Moving right. //
			if(Input.GetAxis("Horizontal") >= 1)
				isFacingRight = true;
			// Moving left. //
			else if(Input.GetAxis("Horizontal") <= -1)
				isFacingRight = false;

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}
		else animator.SetBool("IsSneaking", false);


		if(controller.state == PlayerState.Walking){

			animator.speed = walkAnimationSpeed;

			// Moving right. //
			if(Input.GetAxis("Horizontal") >= 1){
				isFacingRight = true;
				animator.SetBool("IsWalking", true);
			}
			// Moving left. //
			else if(Input.GetAxis("Horizontal") <= -1){
				isFacingRight = false;
				animator.SetBool("IsWalking", true);
			}
		}
		else animator.SetBool("IsWalking", false);


		if(controller.state == PlayerState.Standing){
			animator.speed = standAnimationSpeed;
		}
		*/		

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	private void toggleOffOtherBools(string name){

		for(int i = 0; i < animator.parameters.Length; i++){
			if(animator.parameters[i].name != name)
				animator.SetBool(animator.parameters[i].name, false);
		}
	}

	public IEnumerator playDeath(){

		yield return new WaitForSeconds(1.2f);

		this.GetComponent<MainController>().pauseController();
		animator.SetTrigger("Death");
	}
}