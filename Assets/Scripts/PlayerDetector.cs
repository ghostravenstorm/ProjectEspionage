using UnityEngine;
using System.Collections;

// For use on guards or other entities that interact with the player.
public class PlayerDetector : MonoBehaviour{
	
	public float detectionRange;
	//private IEnumerator IEDetectorCheck;
	private Coroutine detectorCheck;
	private bool isDetetorRunning;
	private GameObject playerRef;

	void Start(){
		this.transform.GetComponent<BoxCollider>().size = new Vector3(detectionRange, 1, 1);
		this.transform.GetComponent<BoxCollider>().center = new Vector3(detectionRange * 0.5f, 0, 0);
		StartCoroutine(detector());
		
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
		//detectorCheck = StartCoroutine(detectorCheckRoutine(collider.gameObject));
		if(collider.gameObject.tag == "Player"){
			if(playerRef == null)
				playerRef = collider.gameObject;
			isDetetorRunning = true;
		}
	}

	void OnTriggerExit(Collider collider){
		//if(collider.gameObject.tag == "Player") StopCoroutine(detectorCheck);
		if(collider.gameObject.tag == "Player")
			isDetetorRunning = false;
	}

	private IEnumerator detector(){
		while(true){

			if(isDetetorRunning){

				if(playerRef.tag == "Player"){
				
					var detector = playerRef.transform.GetChild(0).GetComponent<LightDetector>();
					
					if(detector.isDetectable){
						isDetetorRunning = false;
						//StartCoroutine( obj.GetComponent<SpyAnimationController>().playDeath() );
						//this.transform.parent.GetComponent<GuardAnimationController>().playShoot();
						//StopCoroutine(IEDetectorCheck);

						Debug.Log("Beep");
						

						//yield return new WaitForSeconds(2f);
						//Debug.Log("Reached");

						StartCoroutine(deathSequence());
						//StopCoroutine(detectorCheck);
						//this.transform.parent.GetComponent<Guard>().isPatrolling = true;	
					}
				}
			}

			yield return null;
		}
	}

	private IEnumerator deathSequence(){
		StartCoroutine( playerRef.GetComponent<SpyAnimationController>().playDeath() );
		this.transform.parent.GetComponent<Guard>().isPatrolling = false;
		this.transform.parent.GetComponent<GuardAnimationController>().playShoot();

		//Debug.Log("check1");
		yield return new WaitForSeconds(3f);
		//Debug.Log("check2");
		this.transform.parent.GetComponent<Guard>().isPatrolling = true;
		//yield return new WaitForSeconds(0.5f);
		//detectorCheck = StartCoroutine(detectorCheckRoutine(GetComponent<Collider>().gameObject));
	}
}