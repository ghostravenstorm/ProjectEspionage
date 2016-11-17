using UnityEngine;

public class DisableSprites : MonoBehaviour{
	void Start(){
		GetComponent<SpriteRenderer>().enabled = false;
	}
}