using UnityEngine;

public class Agent : MonoBehaviour {
	
	public static GameObject instance;

	void Awake(){
		instance = this.gameObject;
	}
}