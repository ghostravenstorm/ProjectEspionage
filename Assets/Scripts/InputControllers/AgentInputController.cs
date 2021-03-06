using UnityEngine;
using System.Collections;

public class AgentInputController : InputController{

	public float movementSpeed = 3f;
	public float jumpForce = 5f;
	public float climbModifier = 0.8f;
	public float sprintModifier = 1.8f;
	public float sneakModifier = 0.5f;
	public float sprintMeter = 1f;
	public float sprintRecharge = 0.02f;
	public float sprintDecay = 0.06f;
	private bool isSprintExhausted;

	// -- Used to prevent double jumping.
	public bool isGrounded = false;

	public AgentState state;

	private float currentSpeed;
	public bool isAtLadder = false;
	public bool isAtRope = false;

	// -- Used to identify when the agent is actually on the ground.
	private bool isTouchingGround = false;

	private GameObject ropeRef;

	private Vector3 originalScale;
	private Quaternion originalRotation;

	void Start(){
		state = AgentState.Standing;
		currentSpeed = movementSpeed;
		originalScale = transform.localScale;
		originalRotation = transform.localRotation;

		StartCoroutine("SprintMeter");

		// -- Gives default control over the Agent when the scene starts.
		InputManager.instance.mainInput = this;
	}

	void Update(){
		Debug.Log(state);
		//Debug.Log("Is Grounded: " + isGrounded);
		//Debug.Log("Is Touching Ground: " + isTouchingGround);
		//Debug.Log("Is at rope: " + isAtRope);

		// if(ropeRef == null) return;

		// Transform ropeAnchor = ropeRef.transform.parent;
		// Transform bottomPoint = ropeAnchor.Find("BottomPoint");
		// float rotation = ropeAnchor.localRotation.eulerAngles.z;
		// float dist = Vector3.Distance(ropeAnchor.position, transform.position);
		// float length = Vector3.Distance(ropeAnchor.position, bottomPoint.position);
		// float modifier = ((dist * 100f) / length) / 100;
		// float velx = modifier * (length / 2f);
		// float vely = Mathf.Abs(Mathf.Sin(rotation)) + (currentSpeed * climbModifier);
		// if(rotation > 0 && rotation < 180) velx *= -1;

		//Debug.Log(new Vector3(velx, vely, 0));
		//Debug.Log("curSpeed: " + currentSpeed);
		//Debug.Log("rotation: " + rotation);
		//Debug.Log("velx: " + velx);
		//Debug.Log("pendV: " + pvel);
		//Debug.Log("dist: " + dist);
		//Debug.Log("length: " + length);
		//Debug.Log(bottomPoint.gameObject.GetComponent<Rigidbody>().velocity.x);
		//Debug.Log(GetComponent<Rigidbody>().velocity);
	}

	void OnCollisionEnter(Collision c){
		if(c.gameObject.tag == "Ground"){
			isGrounded = true;
			isTouchingGround = true;
			state = AgentState.Standing;
		}
	}

	void OnCollisionExit(Collision c){
		if(c.gameObject.tag == "Ground"){
			isTouchingGround = false;
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Ladder"){
			isAtLadder = true;
		}
		if(c.gameObject.tag == "Rope"){
			isAtRope = true;
			ropeRef = c.gameObject;
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Ladder"){
			isAtLadder = false;
		}
		if(c.gameObject.tag == "Rope"){
			// -- Remove Agent form rope and restore default scaling.
			isAtRope = false;
			c.gameObject.transform.DetachChildren();
			transform.localScale = originalScale;
			transform.localRotation = originalRotation;
		}
	}

	private IEnumerator SprintMeter(){

		while(true){
			//Debug.Log("SprintMeter is runnning");

			if(Input.GetButton("Sprint") && state == AgentState.Running){
				yield return new WaitForSeconds(0.1f);
				sprintMeter -= 0.06f;
				if(sprintMeter <= 0){
					sprintMeter = 0f;
					isSprintExhausted = true;
				}
			}
			else{
				yield return new WaitForSeconds(0.1f);
				sprintMeter += 0.02f;
				if(sprintMeter >= 1) sprintMeter = 1f;
			}

			GUIManager.instance.UpdateSprintMeter(sprintMeter);
		}
	}

	// -- Movement logic for climable things.
	public override void OnMoveUp(){
		if((!isAtLadder && !isAtRope) && state != AgentState.Sneaking){
			// -- Agent shouldn't be ale to climb while sneaking or not standing at 
			//    a climbable object.
			GetComponent<Rigidbody>().useGravity = true;
			return;
		}

		if(isAtLadder)
			state = AgentState.ClimbingLadder;
		else if(isAtRope){
			if(state != AgentState.ClimbingRope){
				// -- Attach Agent to the rope as a child transform.
				transform.parent = ropeRef.transform;
			}
			state = AgentState.ClimbingRope;
		}
		else
			state = AgentState.Jumping;

		if(!isTouchingGround) isGrounded = false;

		var rb = GetComponent<Rigidbody>();
		if(state == AgentState.Jumping)
			rb.velocity = new Vector3(rb.velocity.x, 0, 0);

		GetComponent<Rigidbody>().useGravity = false;

		// -- Movement logic for swinging ropes. WIP.
		if(state == AgentState.ClimbingRope){
			Transform ropeAnchor = ropeRef.transform.parent;
			Transform bottomPoint = ropeAnchor.Find("BottomPoint");
			float rotation = ropeAnchor.localRotation.eulerAngles.z;
			float dist = Vector3.Distance(ropeAnchor.position, transform.position);
			float length = Vector3.Distance(ropeAnchor.position, bottomPoint.position);
			float modifier = ((dist * 100f) / length) / 100;
			float velx = modifier * (1f);
			float vely = Mathf.Abs(Mathf.Sin(rotation)) + (currentSpeed * climbModifier);
			if(rotation > 0 && rotation < 180) velx *= -1;
			// -- TODO: X vector still doesn't track with the rope properly.
			rb.velocity = new Vector3(velx, vely, 0);
		}
		else
			rb.velocity = new Vector3(rb.velocity.x, currentSpeed * climbModifier, 0);
	}

	public override void OffMoveUp(){

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, 0, 0);
	}

	// -- Movement logic for climable things.
	public override void OnMoveDown(){
		if((!isAtLadder && !isAtRope) && state == AgentState.Sneaking && isGrounded){
			GetComponent<Rigidbody>().useGravity = true;
			return;
		}
		if(state != AgentState.ClimbingLadder || state != AgentState.ClimbingRope)
			return;

		if(isAtLadder)
			state = AgentState.ClimbingLadder;
		else if(isAtRope)
			state = AgentState.ClimbingRope;
		else
			state = AgentState.Jumping;

		if(!isTouchingGround) isGrounded = false;

		var rb = GetComponent<Rigidbody>();
		if(state == AgentState.Jumping)
			rb.velocity = new Vector3(rb.velocity.x, 0, 0);

		GetComponent<Rigidbody>().useGravity = false;
		if(state == AgentState.ClimbingRope){
			Transform ropeAnchor = ropeRef.transform.parent;
			Transform bottomPoint = ropeAnchor.Find("BottomPoint");
			float rotation = ropeAnchor.localRotation.eulerAngles.z;
			float dist = Vector3.Distance(ropeAnchor.position, transform.position);
			float length = Vector3.Distance(ropeAnchor.position, bottomPoint.position);
			float modifier = ((dist * 100f) / length) / 100;
			float velx = modifier * (1f);
			float vely = Mathf.Abs(Mathf.Sin(rotation)) + (currentSpeed * climbModifier);
			if(rotation > 0 && rotation < 180) velx *= -1;
			rb.velocity = new Vector3(velx, vely * -1, 0);
		}
		else
			rb.velocity = new Vector3(rb.velocity.x, currentSpeed * climbModifier * -1, 0);
	}

	public override void OffMoveDown(){

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, 0, 0);
	}

	// -- Movement logic for left and right.
	public override void OnMoveRight(){
		if(state != AgentState.Jumping)
			state = AgentState.Walking;

		if(!isAtLadder || !isAtRope)
			GetComponent<Rigidbody>().useGravity = true;
		else if(!isTouchingGround && isAtRope){
			GetComponent<Rigidbody>().useGravity = false;
		}

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(currentSpeed, rb.velocity.y, 0);
	}

	public override void OffMoveRight(){
		if(state == AgentState.Walking || state == AgentState.Running)
			state = AgentState.Standing;

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
	}

	public override void OnMoveLeft(){
		if(state != AgentState.Jumping)
			state = AgentState.Walking;

		if(!isAtLadder || !isAtRope)
			GetComponent<Rigidbody>().useGravity = true;
		else if(!isTouchingGround && isAtRope){
			GetComponent<Rigidbody>().useGravity = false;
		}

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(currentSpeed * -1, rb.velocity.y, 0);
	}

	public override void OffMoveLeft(){
		if(state == AgentState.Walking || state == AgentState.Running)
			state = AgentState.Standing;

		var rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0, rb.velocity.y, 0);
	}

	public override void OnJump(){
		if(state == AgentState.Sneaking)
			return;
		else if(isGrounded){
			isGrounded = false;
			state = AgentState.Jumping;
			var rb = GetComponent<Rigidbody>();
			rb.AddForce(rb.velocity.x, jumpForce, 0, ForceMode.Impulse);
		}
	}

	public override void OnSprint(){
		if(isSprintExhausted){
			currentSpeed = movementSpeed;
			return;
		}

		isSprintExhausted = false;
		var rb = GetComponent<Rigidbody>();

		if(state == AgentState.Jumping)
			return;
		else if(state == AgentState.ClimbingLadder)
			return;
		else if(state == AgentState.ClimbingRope)
			return;
		else if(rb.velocity != Vector3.zero)
			state = AgentState.Running;
		else
			state = AgentState.Standing;

		currentSpeed = movementSpeed * sprintModifier;
	}

	public override void OffSprint(){
		if(state == AgentState.ClimbingLadder)
			return;
		if(state == AgentState.ClimbingRope)
			return;

		currentSpeed = movementSpeed;
	}

	public override void OnSneak(){
		if(state == AgentState.ClimbingLadder)
			return;
		if(state == AgentState.ClimbingRope)
			return;

		if(state == AgentState.Sneaking || state == AgentState.Walking || state == AgentState.Standing){
			state = AgentState.Sneaking;
			currentSpeed = movementSpeed * sneakModifier;
		}
	}

	public override void OffSneak(){
		if(state == AgentState.ClimbingLadder)
			return;
		if(state == AgentState.ClimbingRope)
			return;

		state = AgentState.Standing;
		currentSpeed = movementSpeed;
	}
}
