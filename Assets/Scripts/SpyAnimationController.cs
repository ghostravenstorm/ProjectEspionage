using System.Collections;
using UnityEngine;

public class SpyAnimationController : MonoBehaviour{
	
	private Animator animator;
	private SpriteRenderer sprite;

	public bool isFacingRight;
	public float standAnimationSpeed = 1f;
	public float walkAnimationSpeed = 1f;
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

		if(controller.state == PlayerState.Walking){

			animator.speed = walkAnimationSpeed;

			/* Moving right. */
			if(Input.GetAxis("Horizontal") >= 1){
				isFacingRight = true;
				animator.SetBool("IsWalking", true);
			}
			/* Moving left. */
			else if(Input.GetAxis("Horizontal") <= -1){
				isFacingRight = false;
				animator.SetBool("IsWalking", true);
			}
		}

		if(controller.state == PlayerState.Sneaking){
			animator.SetBool("IsSneaking", true);
			animator.speed = sneakAnimationSpeed;

			/* Moving right. */
			if(Input.GetAxis("Horizontal") >= 1)
				isFacingRight = true;
			/* Moving left. */
			else if(Input.GetAxis("Horizontal") <= -1)
				isFacingRight = false;

			if(Input.GetAxis("Horizontal") == 0)
				animator.speed = 0f;
			else
				animator.speed = sneakAnimationSpeed;
		}

		if(controller.state == PlayerState.Standing){
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsSneaking", false);
			animator.speed = standAnimationSpeed;
		}

		if(isFacingRight) sprite.flipX = true;
		else sprite.flipX = false;
	}

	public IEnumerator playDeath(){

		yield return new WaitForSeconds(1.2f);

		this.GetComponent<MainController>().pauseController();
		animator.SetTrigger("Death");
	}
}