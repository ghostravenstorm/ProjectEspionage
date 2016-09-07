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

	void Start(){
		rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update(){

		/* Left or Right input */
		moveX = Input.GetAxis("Horizontal");

		/* Ladder climb input */
		if(Input.GetAxis("Vertical") != 0 && isAtLadder){
			moveY = Input.GetAxis("Vertical");
		}

		/* Jump input */
		if(Input.GetAxis("Jump") >= 1 && isGrounded){
			Jump();
		}

		/* Sprint input */
		if(Input.GetAxis("Sprint") >=1 ){
			Debug.Log("Sprinting");
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

		if(isAtLadder)
			vectorY = moveY * climbSpeed;
		else{
			vectorY = rigidbody.velocity.y;
			rigidbody.velocity = new Vector3(vectorX, vectorY, 0);
		}

		
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Ground")
			isGrounded = true;
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Ladder"){
			EnabledLadderMode();
		}
	}

	void OnColliderExit(Collider collider){
		if(collider.gameObject.tag == "Ladder"){
			DisableLadderMode();
		}
	}

	private void EnabledLadderMode(){
		// switch forward facing animation
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