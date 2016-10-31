using UnityEngine;
using System.Collections;

public class RopeSwing : MonoBehaviour{

	//private new Rigidbody rigidbody;
	[Tooltip("In degrees.")]
	public float swingAmount;
	//[Tooltip("In degrees per second.")]
	public float swingRate;

	private IEnumerator IESwing;

	void Start(){
		//rigidbody = this.GetComponent<Rigidbody>();
		IESwing = Swing();
		StartCoroutine(IESwing);
	}

	void Update(){
		//Debug.Log(this.transform.rotation.eulerAngles);
	}
	
	private IEnumerator Swing(){

		float interpolatedRotation = 0;

		while(true){

			interpolatedRotation += swingRate;

			this.transform.rotation = Quaternion.Euler(0, 0, interpolatedRotation);
			
			if(interpolatedRotation >= swingAmount){
				//Debug.Log("switch neg");
				swingRate *= -1;
			}
			else if(interpolatedRotation <= 0 - swingAmount){
				swingRate *= -1;
				//Debug.Log("switch pos");
			}


			//Debug.Log(interpolatedRotation);

			yield return new WaitForSeconds(Time.deltaTime);
		}

	}
}