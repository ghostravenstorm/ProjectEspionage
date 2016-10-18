using System;
using UnityEngine;

public class SpyAnimationController : MonoBehaviour{
	
	private Animator animator;

	void Start(){
		animator = this.GetComponent<Animator>();

		if(!animator)
			Debug.LogError("Could not find animator componenet!");
	}

	void Update(){
		if(Input.GetAxis("Horizontal") >= 1){
			this.transform.localScale = new Vector3(-1, 1, 1);
			animator.SetBool("IsWalking", true);
		}
		else if(Input.GetAxis("Horizontal") <= -1){
			this.transform.localScale = new Vector3(1, 1, 1);
			animator.SetBool("IsWalking", true);
		}
		else
			animator.SetBool("IsWalking", false);
	}
}