using UnityEngine;

public class ParallaxTracker : MonoBehaviour{
	public GameObject cameraMountRef;

	void Update(){


		this.transform.position = new Vector3(cameraMountRef.transform.position.x, this.transform.position.y, this.transform.position.z);
	}
}