using UnityEngine;
using UnityEngine.UI;

public class LightDetector : MonoBehaviour{

	public bool isDetectable{ private set; get; }
	public bool isInLightMain{ private set; get; }
	public bool isInLightFallOff{ private set; get; }

	public GameObject GUI;

	void Start(){
		isDetectable = true;
	}

	void Update(){
		var player = transform.parent.GetComponent<Sneakable>();

		if(Input.GetButtonDown("Sneak") && !player.isSneaking){
			isDetectable = false;
			Debug.Log("Is detectable: " + isDetectable);
		}
		
		if(Input.GetButtonDown("Sneak") && player.isSneaking){
			if(!isInLightMain){
				isDetectable = true;
			}
			Debug.Log("Is detectable: " + isDetectable);
		}

		if(isDetectable)
			GUI.GetComponent<Text>().text = "Is Detectable.";
		else
			GUI.GetComponent<Text>().text = "Is NOT Detectable.";
	}

	void OnTriggerEnter2D(Collider2D collider) {

		var player = transform.parent.GetComponent<Sneakable>();

		if(collider.gameObject.tag == "LightMain"){
			isDetectable = true;
			isInLightMain = true;
			//Debug.Log("Is detectable: " + isDetectable);
			//Debug.Log("Is in main light: " + isInLightMain);
		}

		if(collider.gameObject.tag == "LightFallOff"){
			isInLightFallOff = true;

			if(!player.isSneaking){
				isDetectable = true;
				//Debug.Log("Is detectable: " + isDetectable);
			}
			//Debug.Log("Is in fall off light: " + isInLightFallOff);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.tag == "LightMain"){
			isInLightMain = false;
			//Debug.Log("Is in main light: " + isInLightMain);
		}

		if(collider.gameObject.tag == "LightFallOff"){
			isInLightFallOff = false;
			//Debug.Log("Is in fall off light: " + isInLightFallOff);
			//Debug.Log("Is detectable: " + isDetectable);
		}
	}
}