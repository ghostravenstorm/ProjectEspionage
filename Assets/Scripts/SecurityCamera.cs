using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour, IOverrideableObject{
	
	public GameObject detector;

	public float rotationAmount = 20f;
	public float rotationRate = 1f;
	public bool isRotating = true;

	public bool isDisabledOnTimer = false;
	public float rebootTimer = 0;

	private float rotationFix;

	void Start(){
		rotationFix = this.transform.rotation.z;
		StartCoroutine(Rotate());
	}

	public void Disable(){
		isRotating = false;
		detector.SetActive(false);
		if(isDisabledOnTimer) StartCoroutine(Reboot());
	}

	public void Activate(){
		isRotating = true;
		detector.SetActive(true);
	}

	public IEnumerator PauseRotation(float seconds){
		isRotating = false;
		yield return new WaitForSeconds(seconds);
		isRotating = true;
	}

	private IEnumerator Rotate(){
		float interpolatedrotation = rotationFix;

		while(true){

			if(isRotating){

				interpolatedrotation += rotationRate;
				this.transform.rotation = Quaternion.Euler(0, 0, interpolatedrotation);

				if(interpolatedrotation >= rotationAmount)
					rotationRate *= -1;
				else if(interpolatedrotation <= 0 -rotationAmount)
					rotationRate *= -1;
			}

			yield return new WaitForSeconds(Time.deltaTime);
		}
	}

	private IEnumerator Reboot(){

		yield return new WaitForSeconds(rebootTimer);

		Activate();
	}
}