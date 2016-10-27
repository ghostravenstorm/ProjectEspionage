using UnityEngine;

// ** Experimental

// For use on guards or other entities that interact with the player.
public class PlayerDetector : MonoBehaviour{
	
	public float detectionRange;

	void Start(){
		this.transform.GetComponent<BoxCollider>().size = new Vector3(detectionRange, 1, 1);
		this.transform.transform.localPosition = new Vector3(detectionRange * 0.5f, 0, 0);
		
	}

	void OnTriggerEnter(Collider collider){
		var controller = collider.GetComponent<MainController>().controller;
		if(collider.gameObject.tag == "Player"){
			if(controller.state != PlayerState.Sneaking)
				Debug.Log("Player Detected");
		}
	}
}