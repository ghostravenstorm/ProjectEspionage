using UnityEngine;

public class RopeLogic : MonoBehaviour{

	private bool isAtRope;

	void Update(){
		if(isAtRope){
			
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Rope"){
			Debug.Log("Rope collided!");

			var body = this.GetComponent<Rigidbody>();
			body.useGravity = false;
			body.velocity = new Vector3(body.velocity.x, 0, 0);

			isAtRope = true;
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "Rope"){

			var body = this.GetComponent<Rigidbody>();
			body.useGravity = true;

			isAtRope = false;
		}
	}
}