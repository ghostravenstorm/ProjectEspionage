using System.Threading;
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
	public float climbAnimationSpeed = 1f;

	void Start(){
		animator = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	void Update(){

		var controller = this.GetComponent<MainController>().controller;

		//Debug.Log(controller.state);
		//Debug.Log(controller.isGrounded);

		// Moving right. //
		if(Input.GetAxis("Horizontal") >= 1)
			isFacingRight = true;
		// Moving left. //
		else if(Input.GetAxis("Horizontal") <= -1)
			isFacingRight = false;


		if(controller.state == PlayerState.ClimbingRope){
			animator.SetBool("IsClimbingRope", true);
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == PlayerState.ClimbingLadder){
			animator.SetBool("IsClimbingLadder", true);
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == PlayerState.Walking){
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
		}
		else if(controller.state == PlayerState.Sneaking){
			toggleOffOtherBools("IsSneaking");
			animator.SetBool("IsSneaking", true);

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}
		else if(controller.state == PlayerState.Jumping){
			animator.SetTrigger("Jump");
			animator.speed = jumpAnimationSpeed;
		}	

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	private void toggleOffOtherBools(string name){

		for(int i = 0; i < animator.parameters.Length; i++){
			if(animator.parameters[i].name != name)
				animator.SetBool(animator.parameters[i].name, false);
		}
	}

	public IEnumerator playDeath(float delay){

		yield return new WaitForSeconds(delay);

		this.GetComponent<MainController>().pauseController();
		animator.SetTrigger("Death");

		yield return new WaitForSeconds(2f);

		this.transform.position = GameManager.instance.getCheckPoint();
		this.GetComponent<SpyAnimationController>().ResetAnimation();
		this.GetComponent<MainController>().resumeController();

	}

	public void ResetAnimation(){
		animator.SetTrigger("Reset");
	}
}