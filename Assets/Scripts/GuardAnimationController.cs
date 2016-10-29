using UnityEngine;

public class GuardAnimationController : MonoBehaviour{
	
	private Animator animator;
	private SpriteRenderer sprite;
	private new Rigidbody rigidbody;

	//public GuardType guardType;
	public bool isFacingRight;
	public float walkAnimationSpeed = 0.5f;

	private Vector3 rightScale;
	private Vector3 leftScale;

	void Start(){
		animator = this.GetComponent<Animator>();
		sprite = this.GetComponent<SpriteRenderer>();
		rigidbody = this.GetComponent<Rigidbody>();

		animator.speed = walkAnimationSpeed;

		if(isFacingRight) rightScale = this.transform.localScale;
		if(isFacingRight) leftScale = new Vector3(rightScale.x * -1, rightScale.y, rightScale.z);
		if(!isFacingRight) rightScale = this.transform.localScale * -1;
		if(!isFacingRight) leftScale = new Vector3(rightScale.x * -1, rightScale.y, rightScale.z);;
	}

	void Update(){

		if(rigidbody.velocity.x == 0)
			animator.SetBool("IsWalking", false);
		else
			animator.SetBool("IsWalking", true);

		if(rigidbody.velocity.x >= 1) isFacingRight = true;
		if(rigidbody.velocity.x <= -1) isFacingRight = false;

		//if(isFacingRight) sprite.flipX = false;
		//else sprite.flipX = true;

		if(isFacingRight) this.transform.localScale = rightScale;
		else this.transform.localScale = leftScale;

		//Debug.Log(rigidbody.velocity);
	}

	public void playShoot(){
		this.GetComponent<Guard>().isPatrolling = false;
		animator.SetTrigger("Shoot");
	}
}