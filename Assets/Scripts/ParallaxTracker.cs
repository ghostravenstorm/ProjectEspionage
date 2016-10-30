using UnityEngine;

public class ParallaxTracker : MonoBehaviour{
	public GameObject playerRef;

	void Update(){
		this.transform.position = new Vector3(playerRef.transform.position.x, this.transform.position.y, this.transform.position.z);
	}
}