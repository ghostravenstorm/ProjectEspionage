using UnityEngine;

public class WinPoint : MonoBehaviour{
	
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player")
			StartCoroutine(GameManager.instance.runDemoEndScreen());
	}
}