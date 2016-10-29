using UnityEngine;
using System.Collections;

// For use on guards or other entities that interact with the player.
public class PlayerDetector : MonoBehaviour{
	
	public float detectionRange;
	private IEnumerator IEDetectorCheck;

	void Start(){
		this.transform.GetComponent<BoxCollider>().size = new Vector3(detectionRange, 1, 1);
		this.transform.GetComponent<BoxCollider>().center = new Vector3(detectionRange * 0.5f, 0, 0);
		
	}

	void Update(){
		if(this.transform.parent.GetComponent<Rigidbody>().velocity.x >= 1){
			//this.transform.GetComponent<BoxCollider>().center = new Vector3(detectionRange * 0.5f, 0, 0);
		}
		if(this.transform.parent.GetComponent<Rigidbody>().velocity.x <= -1){
			//this.transform.GetComponent<BoxCollider>().center = new Vector3(detectionRange * 0.5f * -1, 0, 0);
		}
	}

	void OnTriggerEnter(Collider collider){
		IEDetectorCheck = detectorCheck(collider.gameObject);
		StartCoroutine(IEDetectorCheck);

		
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "Player") StopCoroutine(IEDetectorCheck);
	}

	private IEnumerator detectorCheck(GameObject obj){
		while(true){

			if(obj.tag == "Player"){
			
				var detector = obj.transform.GetChild(0).GetComponent<LightDetector>();

				if(detector.isDetectable){
					StartCoroutine( obj.GetComponent<SpyAnimationController>().playDeath() );
					this.transform.parent.GetComponent<GuardAnimationController>().playShoot();
					StopCoroutine(IEDetectorCheck);
				}
			}

			yield return null;
		}
	}
}