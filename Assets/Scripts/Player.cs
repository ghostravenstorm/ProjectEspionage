using UnityEngine;

public class Player : MonoBehaviour{

	public float XDistanceFromSpawn;
	public Vector3 spawnPoint;

	void Start(){
		this.spawnPoint = this.transform.position;
		GameManager.instance.setCheckPoint(this.transform.position);
	}

	void Update(){
		XDistanceFromSpawn = (spawnPoint.x - this.transform.position.x);
	}
	
	void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Ground"){
			var camtracker = this.transform.Find("CameraTracker");
			camtracker.transform.localPosition = new Vector3(
				camtracker.transform.localPosition.x,
				2.5f,
				camtracker.transform.localPosition.z
			);
		}

		if(collision.gameObject.tag == "Bridge"){
			Debug.Log("Bridge?");
			var camtracker = this.transform.Find("CameraTracker");
			camtracker.transform.localPosition = new Vector3(
				camtracker.transform.localPosition.x,
				0.5f,
				camtracker.transform.localPosition.z
			);
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "DropDownZone"){
			transform.Find("CameraTracker").GetComponent<CameraTracker>().isLookingDown = true;
		}
	}

	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "DropDownZone"){
			transform.Find("CameraTracker").GetComponent<CameraTracker>().isLookingDown = false;
		}
	}

	void OnTriggerStay(Collider c){

		

		if(c.gameObject.tag == "Ladder" ||
		   c.gameObject.tag == "Rope"   ||
		   c.gameObject.tag == "Ladder"    ){

				var camtracker = this.transform.Find("CameraTracker");
				camtracker.transform.localPosition = new Vector3(
					camtracker.transform.localPosition.x,
					0.5f,
					camtracker.transform.localPosition.z
				);
		}
	}
}