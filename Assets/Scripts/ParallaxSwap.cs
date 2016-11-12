using UnityEngine;

public class ParallaxSwap : MonoBehaviour{

	public GameObject old;
	public GameObject neww;
	public GameObject backold;
	public GameObject backnew;

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Player"){
			old.SetActive(false);
			neww.SetActive(true);
			backold.SetActive(false);
			backnew.SetActive(true);
			Destroy(this.gameObject);
		}

	}
}