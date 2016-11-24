using UnityEngine;

public class CameraTracker : MonoBehaviour{

	public GameObject player;
	public GameObject cameraMount;
	public GameObject camTracker;
	public GameObject cam;
	public GameObject cameraPointRef;
	public float horizontalOffset;
	public float topOffset;
	public float bottomOffset;
	public float moveSpeed;

	private GameObject rightPoint;
	private GameObject leftPoint;
	private GameObject topPoint;
	private GameObject midPoint;
	private GameObject bottomPoint;
	private Vector3 point;

	public bool isLookingDown = false;

	void Start(){
		point = new Vector3();

		rightPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		rightPoint.transform.localPosition = new Vector3(horizontalOffset, 0, 0);

		leftPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		leftPoint.transform.localPosition = new Vector3(horizontalOffset * -1, 0, 0);

		topPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		topPoint.transform.localPosition = new Vector3(0, topOffset, 0);

		bottomPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		bottomPoint.transform.localPosition = new Vector3(0, bottomOffset * -1, 0);

		midPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		midPoint.transform.localPosition = new Vector3(0, 0, 0);
	}

	void Update(){

		var agentInput = Agent.instance.GetComponent<AgentInputController>();
		var agentAnim = Agent.instance.GetComponent<SpyAnimationController>();

		float camTrackerPosX = camTracker.transform.position.x;

		if(isLookingDown){
			point = bottomPoint.transform.position;
		}
		else if( (agentInput.state == AgentState.ClimbingRope ||
			 agentInput.state == AgentState.ClimbingLadder ||
			 agentInput.state == AgentState.ClimbingLedge) &&
			 Input.GetButton("Vertical") ){

			cameraMount.transform.position = Vector3.Lerp(cameraMount.transform.position, camTracker.transform.position, moveSpeed * Time.deltaTime);

				if(Input.GetAxis("Vertical") > 0)
					point = topPoint.transform.position;
				else if(Input.GetAxis("Vertical") < 0)
					point = midPoint.transform.position;
		}
		else if(agentAnim.isFacingRight && agentInput.state != AgentState.Dead){
			cameraMount.transform.position = new Vector3(camTrackerPosX, cameraMount.transform.position.y, cameraMount.transform.position.z);
			//cameraMount.transform.position = Vector3.Lerp(cameraMount.transform.position, camTracker.transform.position, moveSpeed * Time.deltaTime);
			point = rightPoint.transform.position;
		}
		else if(!agentAnim.isFacingRight && agentInput.state != AgentState.Dead){
			cameraMount.transform.position = new Vector3(camTrackerPosX, cameraMount.transform.position.y, cameraMount.transform.position.z);
			//cameraMount.transform.position = Vector3.Lerp(cameraMount.transform.position, camTracker.transform.position, moveSpeed * Time.deltaTime);
			point = leftPoint.transform.position;
		}

		cam.transform.position = Vector3.Lerp(cam.transform.position, point, moveSpeed * Time.deltaTime);
		cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);

		if(agentInput.isGrounded)
			cameraMount.transform.position = Vector3.Lerp(cameraMount.transform.position, camTracker.transform.position, moveSpeed * Time.deltaTime);
	}
}
