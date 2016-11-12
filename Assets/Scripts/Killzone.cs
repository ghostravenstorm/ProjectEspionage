using UnityEngine;
using System.Collections;

public class Killzone : MonoBehaviour{

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			StartCoroutine(Respawn(collider.gameObject));
		}
	}

	private IEnumerator Respawn(GameObject agent){
		agent.GetComponent<AgentSoundController>().PlayDying();
		yield return new WaitForSeconds(1f);
		agent.transform.position = GameManager.instance.getCheckPoint();
	}
}