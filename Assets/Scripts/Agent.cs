using UnityEngine;

public class Agent : MonoBehaviour {
	
	public static Agent instance;

	void Awake(){
		instance = this;
	}

	public void Kill(float delay){
		GetComponent<SpyAnimationController>().PlayDeath(delay);
	}
}