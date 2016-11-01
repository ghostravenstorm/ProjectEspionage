using UnityEngine;

public class Killzone : MonoBehaviour{

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			collider.gameObject.transform.position = GameManager.instance.getCheckPoint();
		}
	}
}