using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour{
	
	public float patrolSpeed;
	public float patrolRadius;
	public float detectionRange;

	public GameObject playerPrefab;

	private Vector3 origin;
	private Vector3 originalScale;

	private IEnumerator IEPatrol;

	void Start(){
		origin = this.transform.position;
		originalScale = this.transform.localScale;

		IEPatrol = Patrol();
		StartCoroutine(IEPatrol);

		this.transform.GetChild(0).GetComponent<BoxCollider>().size = new Vector3(detectionRange, 1, 1);
		this.transform.GetChild(0).transform.localPosition = new Vector3(detectionRange * 0.5f, 0, 0);
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			bool detector = collider.gameObject.transform.GetChild(0).GetComponent<LightDetector>().isDetectable;
			if(detector){
				Debug.Log("Player detected.");
				RespawnPlayer(collider.gameObject);
			}
		}
	}

	private IEnumerator Patrol(){

		var rigidbody = this.GetComponent<Rigidbody>();

		//float waitTime = 1f;

		while(true){

			if(Vector3.Distance(origin, this.transform.position) >= patrolRadius){
				patrolSpeed *= -1;
				this.transform.localScale = new Vector3(this.transform.localScale.x * -1, originalScale.y, originalScale.z);
			}

			rigidbody.velocity = new Vector3(patrolSpeed, rigidbody.velocity.y, 0);

			yield return null; /*new WaitForSeconds(waitTime);*/
		}
	}

	private void RespawnPlayer(GameObject player){
		Object.Instantiate(playerPrefab, player.GetComponent<Player>().spawnPoint, player.transform.rotation);
		Object.Destroy(player);
	}
}