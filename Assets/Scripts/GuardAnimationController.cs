using UnityEngine;
using System;
using System.Collections;

public class GuardAnimationController : MonoBehaviour{
	
	private Animator animator;
	private new Rigidbody rigidbody;
	private new AudioSource audio;

	public bool isFacingRight;
	public float walkAnimationSpeed = 0.5f;
	public AudioClip[] footsteps;
	public AudioClip[] gunshot;

	private Vector3 rightScale;
	private Vector3 leftScale;

	private bool isWalking;

	private System.Random rand;

	void Start(){
		animator = this.GetComponent<Animator>();
		audio = this.GetComponent<AudioSource>();
		rigidbody = this.GetComponent<Rigidbody>();

		animator.speed = walkAnimationSpeed;

		if(isFacingRight) rightScale = this.transform.localScale;
		if(isFacingRight) leftScale = new Vector3(rightScale.x * -1, rightScale.y, rightScale.z);
		if(!isFacingRight) rightScale = this.transform.localScale * -1;
		if(!isFacingRight) leftScale = new Vector3(rightScale.x * -1, rightScale.y, rightScale.z);;

		rand = new System.Random();

		//StartCoroutine(playFootstepAudio());
	}

	void Update(){

		if(rigidbody.velocity.x == 0){
			animator.SetBool("IsWalking", false);
			isWalking = false;
		}
		else{
			animator.SetBool("IsWalking", true);
			isWalking = true;
		}

		if(rigidbody.velocity.x >= 1) isFacingRight = true;
		if(rigidbody.velocity.x <= -1) isFacingRight = false;

		//if(isFacingRight) sprite.flipX = false;
		//else sprite.flipX = true;

		if(isFacingRight) this.transform.localScale = rightScale;
		else this.transform.localScale = leftScale;

		//Debug.Log(rigidbody.velocity);
	}

	public void playShoot(){
		animator.SetTrigger("Shoot");
		StartCoroutine(playGunshotAudio());
	}

	private IEnumerator playFootstepAudio(){
		while(true){
			if(isWalking){
				yield return new WaitForSeconds(2f);
				audio.clip = footsteps[rand.Next(0, footsteps.Length - 1)];
				audio.Play();
			}
		}
	}

	private IEnumerator playGunshotAudio(){
		yield return new WaitForSeconds(2f);
		audio.clip = gunshot[rand.Next(0, gunshot.Length - 1)];
		audio.Play();
	}
}