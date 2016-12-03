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
	public float deathAniamtionSpeed = 1f;

	void Start(){
		animator = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	void Update(){

		var controller = this.GetComponent<AgentInputController>();

		//Debug.Log(controller.state);
		//Debug.Log(controller.isGrounded);

		// Moving right. //
		if(Input.GetAxis("Horizontal") >= 1)
			isFacingRight = true;
		// Moving left. //
		else if(Input.GetAxis("Horizontal") <= -1)
			isFacingRight = false;


		if(controller.state == AgentState.ClimbingRope){
			animator.SetBool("IsClimbingRope", true);
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == AgentState.ClimbingLadder){
			animator.SetBool("IsClimbingLadder", true);
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == AgentState.Walking){
			toggleOffOtherBools("IsWalking");
			animator.SetBool("IsWalking", true);
			animator.speed = walkAnimationSpeed;
		}
		else if(controller.state == AgentState.Running){
			toggleOffOtherBools("IsSprinting");
			animator.SetBool("IsSprinting", true);
			animator.speed = sprintAnimationSpeed;
		}
		else if(controller.state == AgentState.Standing){
			toggleOffOtherBools("IsStanding");
			animator.SetBool("IsStanding", true);
			animator.speed = standAnimationSpeed;
		}
		else if(controller.state == AgentState.Sneaking){
			toggleOffOtherBools("IsSneaking");
			animator.SetBool("IsSneaking", true);

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}
		else if(controller.state == AgentState.Jumping){
			animator.SetTrigger("Jump");
			animator.speed = jumpAnimationSpeed;
		}	

		if(isFacingRight && controller.state != AgentState.Dead) sprite.flipX = true;
		else sprite.flipX = false;
	}

	private void toggleOffOtherBools(string name){

		for(int i = 0; i < animator.parameters.Length; i++){
			if(animator.parameters[i].name != name)
				animator.SetBool(animator.parameters[i].name, false);
		}
	}

	public void PlayDeath(float delay){
		
		StartCoroutine(PlayDeathCoroutine(delay));
	}

	private IEnumerator PlayDeathCoroutine(float delay){



		yield return new WaitForSeconds(delay);

		//this.GetComponent<MainController>().PauseController(AgentState.Dead);
		InputManager.instance.mainInput = new NullInputController();
		animator.speed = deathAniamtionSpeed;
		animator.SetTrigger("Death");

		yield return new WaitForSeconds(2f);

		ResetAnimation();
		this.transform.position = GameManager.instance.getCheckPoint();
		//this.GetComponent<MainController>().ResumeController();
		InputManager.instance.mainInput = GetComponent<AgentInputController>();

	}

	public void ResetAnimation(){
		animator.SetTrigger("Reset");
	}
}