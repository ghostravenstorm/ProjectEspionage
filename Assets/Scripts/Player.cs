using UnityEngine;

public class Player : MonoBehaviour{

	public float speed;
	public float sprintModifer;
	public float jumpForce;
	public float climbSpeed;

	private float moveX;
	private float moveY;

	private bool isGrounded;
	private bool isSprinting;
	private bool isAtLadder;

	private new Rigidbody rigidbody;
	private Animator animator;

	void Start(){
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}
	
	void Update(){

		/* Left or Right input */
		moveX = Input.GetAxis("Horizontal");

		//Debug.Log(Input.GetAxis("Vertical"));

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

		/* Ladder climb input */
		if(Input.GetAxis("Vertical") != 0 && isAtLadder)
			moveY = Input.GetAxis("Vertical");
		else
			moveY = 0;

		/* Jump input */
		if(Input.GetAxis("Jump") >= 1 && isGrounded){
			Jump();
		}

		/* Sprint input */
		if(Input.GetAxis("Sprint") >=1 ){
			isSprinting = true;
		}
		else	
			isSprinting = false;
	}

	void FixedUpdate(){

		float vectorX;
		float vectorY;

		if(isSprinting){
			vectorX = moveX * speed * sprintModifer;
		}
		else{
			vectorX = moveX * speed;
		}

		if(isAtLadder){
			vectorY = moveY * climbSpeed;
			rigidbody.velocity = new Vector3(vectorX, vectorY, 0);
		}
		else{
			vectorY = rigidbody.velocity.y;
			rigidbody.velocity = new Vector3(vectorX, vectorY, 0);
		}		
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Ground")
			isGrounded = true;

		if(collision.gameObject.tag == "Ladder")
			isGrounded = false;
	}

	void OnColliderExit(Collider collider){
		
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Ladder"){
			EnabledLadderMode();
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "Ladder"){
			DisableLadderMode();
		}
	}

	private void EnabledLadderMode(){
		// switch forward facing animation
		Debug.Log("Ladder mode!");
		rigidbody.useGravity = false;
		isAtLadder = true;
		
	}

	private void DisableLadderMode(){
		// switch to normal animation
		rigidbody.useGravity = true;
		isAtLadder = false;
	}

	private void Jump(){
		isGrounded = false;
		float vectorX = rigidbody.velocity.x;

		rigidbody.AddForce(vectorX, jumpForce, 0, ForceMode.Impulse);
	}
}