using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyAnimationController : MonoBehaviour{
	
	private Animator animator;
	private SpriteRenderer sprite;
	private new AudioSource audio;

	public bool isFacingRight;
	public float standAnimationSpeed = 1f;
	public float walkAnimationSpeed = 1f;
	public float jumpAnimationSpeed = 1f;
	public float sprintAnimationSpeed = 1f;
	public float sneakAnimationSpeed = 0.6f;
	public float climbAnimationSpeed = 1f;

	public AudioClip[] sneakingFootsteps;
	public AudioClip[] walkingFootsteps;
	public AudioClip[] spritingFootsteps;
	public AudioClip[] jumpTakeoff;
	public AudioClip[] groundLand;
	public AudioClip[] dying;

	private AudioClip[] currentLoopingAudio;

	private Thread audioPlayer;
	private int audioSleepTimer;

	private System.Random rand;

	void Start(){
		animator = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();
		audio = this.GetComponent<AudioSource>();

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;

		//audioPlayer = new Thread(this.playAudioLooping);
		currentLoopingAudio = walkingFootsteps;
		//audioPlayer.Start();
		//audioSleepTimer = 2000;

		rand = new System.Random();
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
			//audioPlayer.Suspend();
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == PlayerState.ClimbingLadder){
			animator.SetBool("IsClimbingLadder", true);
			//audioPlayer.Suspend();
			if(Input.GetAxis("Vertical") == 0)
				animator.speed = 0;
			else
				animator.speed = climbAnimationSpeed;
		}
		else if(controller.state == PlayerState.Walking){
			toggleOffOtherBools("IsWalking");
			animator.SetBool("IsWalking", true);
			animator.speed = walkAnimationSpeed;
			//audioPlayer.Resume();
			//currentLoopingAudio = walkingFootsteps;
		}
		else if(controller.state == PlayerState.Running){
			toggleOffOtherBools("IsSprinting");
			animator.SetBool("IsSprinting", true);
			animator.speed = sprintAnimationSpeed;
			//audioPlayer.Resume();
			//currentLoopingAudio = spritingFootsteps;
		}
		else if(controller.state == PlayerState.Standing){
			toggleOffOtherBools("IsStanding");
			animator.SetBool("IsStanding", true);
			//audioPlayer.Suspend();
			//animator.speed = standAnimationSpeed;
		}
		else if(controller.state == PlayerState.Sneaking){
			toggleOffOtherBools("IsSneaking");
			animator.SetBool("IsSneaking", true);
			//currentLoopingAudio = sneakingFootsteps;
			//audioPlayer.Resume();

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}
		else if(controller.state == PlayerState.Jumping){
			animator.SetTrigger("Jump");
			animator.speed = jumpAnimationSpeed;
			//audioPlayer.Suspend();
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

	/*
	private IEnumerator playAudioLooping(){
		while(true){
			audio.clip = currentLoopingAudio[rand.Next(0, currentLoopingAudio.Length - 1)];
			audio.Play();
		}
	}*/

	private void playAudio(AudioClip audio){

	}

	public IEnumerator playDeath(){

		yield return new WaitForSeconds(1.2f);

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