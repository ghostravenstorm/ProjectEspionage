using UnityEngine;

public class CheckPoint : MonoBehaviour{
	
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player")
			GameManager.instance.setCheckPoint(this.transform.position);
	}
}