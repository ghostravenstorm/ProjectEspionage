using UnityEngine;
using System.Collections;

public class SecurityCameraAgentDetector : MonoBehaviour{
	
	public GameObject securityCamera;

	private bool isDetectorRunning;
	private GameObject agentRef;

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			if(agentRef == null)
				agentRef = collider.gameObject;
			isDetectorRunning = true;
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "Player")
			isDetectorRunning = false;
	}

	private IEnumerator Detector(){

		while(true){
			if(isDetectorRunning && agentRef.tag == "Player"){
				isDetectorRunning = false;
				securityCamera.GetComponent<SecurityCamera>().PauseRotation(5f);
				//play camera's shoot animation
			}

			yield return null;
		}
	}
}