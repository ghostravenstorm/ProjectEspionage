using UnityEngine;
using System.Collections;

public class RopeSwing : MonoBehaviour{

	public float swingAmount;
	public float swingRate;
	private float interpolatedRotation;

	public GameObject bottomPoint;

	void Start(){
		StartCoroutine("Swing");
	}

	void Update(){
		//Debug.Log(this.transform.rotation.eulerAngles);
	}

	private IEnumerator Swing(){
		var rb = GetComponent<Rigidbody>();

		interpolatedRotation += swingRate;
		// -- Do not use physics unless Agent has a way to translate with the rope.
		//rb.rotation = Quaternion.Euler(0, 0, interpolatedRotation);
		transform.rotation = Quaternion.Euler(0, 0, interpolatedRotation);

		if(interpolatedRotation >= swingAmount){
			swingRate *= -1;
		}
		else if(interpolatedRotation <= 0 - swingAmount){
			swingRate *= -1;
		}

		yield return new WaitForSeconds(Time.deltaTime);
		yield return StartCoroutine("Swing");
	}

	// private IEnumerator Swing(){
	//
	// 	float interpolatedRotation = 0;
	//
	// 	while(true){
	//
	// 		interpolatedRotation += swingRate;
	//
	// 		this.transform.rotation = Quaternion.Euler(0, 0, interpolatedRotation);
	//
	// 		if(interpolatedRotation >= swingAmount){
	// 			//Debug.Log("switch neg");
	// 			swingRate *= -1;
	// 		}
	// 		else if(interpolatedRotation <= 0 - swingAmount){
	// 			swingRate *= -1;
	// 			//Debug.Log("switch pos");
	// 		}
	//
	//
	// 		//Debug.Log(interpolatedRotation);
	//
	// 		yield return new WaitForSeconds(Time.deltaTime);
	// 	}
}
