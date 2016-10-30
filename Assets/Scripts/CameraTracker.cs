using UnityEngine;

public class CameraTracker : MonoBehaviour{
	
	public GameObject player;
	public GameObject cameraMount;
	public GameObject cam;
	public GameObject cameraPointRef;
	public float horizontalOffset;
	public float moveSpeed;

	private GameObject rightPoint;
	private GameObject leftPoint;
	private Vector3 point;

	void Start(){
		point = new Vector3();

		rightPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		rightPoint.transform.localPosition = new Vector3(horizontalOffset, 0, 0);

		leftPoint = (GameObject)Instantiate(cameraPointRef, cameraMount.transform);
		leftPoint.transform.localPosition = new Vector3(horizontalOffset * -1, 0, 0);
	}

	void Update(){

		float playerPosX = player.transform.position.x;
		cameraMount.transform.position = new Vector3(playerPosX, cameraMount.transform.position.y, cameraMount.transform.position.z);

		if(player.GetComponent<SpyAnimationController>().isFacingRight) point = rightPoint.transform.position;
		else if(!player.GetComponent<SpyAnimationController>().isFacingRight) point = leftPoint.transform.position;

		cam.transform.position = Vector3.Lerp(cam.transform.position, point, moveSpeed * Time.deltaTime);

		cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
	}
}