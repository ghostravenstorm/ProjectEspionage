using UnityEngine;

public class CameraTracker : MonoBehaviour{
	
	public GameObject player;
	public GameObject cam;
	public Transform rightPoint;
	public Transform leftPoint;
	public Transform upPoint;
	public Transform downPoint;
	public float moveSpeed;

	void Update(){

		Vector3 point = new Vector3(0, 0, 0);

		/*
		if     (player.GetComponent<Rigidbody>().velocity.x >=  1) point = rightPoint.position;
		else if(player.GetComponent<Rigidbody>().velocity.x <= -1) point = leftPoint.position;
		else if(player.GetComponent<Rigidbody>().velocity.y >=  1) point = upPoint.position;
		else if(player.GetComponent<Rigidbody>().velocity.y <= -1) point = downPoint.position;
		else point = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
		*/

		if(player.GetComponent<SpyAnimationController>().isFacingRight) point = rightPoint.position;
		else if(!player.GetComponent<SpyAnimationController>().isFacingRight) point = leftPoint.position;
		
		cam.transform.position = Vector3.MoveTowards(
			cam.transform.position, 
			point, 
			moveSpeed * Time.deltaTime
		);

		cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10);
	}
}