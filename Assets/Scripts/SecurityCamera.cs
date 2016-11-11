using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour, IOverrideable{

	public DeviceState state{get; set;}
	
	public GameObject detector;

	public bool isActiveOnStart = true;

	public float rotationAmount = 20f;
	public float rotationRate = 0.1f;
	public bool isRotating = true;

	public bool isDisabledOnTimer = false;
	public float rebootTimer = 0;

	private float rotationFix;

	void Start(){
		rotationFix = this.transform.rotation.z;
		StartCoroutine(Rotate());
		if(isActiveOnStart)
			Activate();
		else
			Disable();
	}

	public void Disable(){
		Debug.Log("Camera disabled.");
		state = DeviceState.Active;
		isRotating = false;
		detector.SetActive(false);
		if(isDisabledOnTimer) StartCoroutine(Reboot());
	}

	public void Activate(){
		state = DeviceState.Unactive;
		isRotating = true;
		detector.SetActive(true);
	}

	public void PauseRotation(float seconds){
		StartCoroutine(PauseRotationCoroutine(seconds));
	}

	private IEnumerator PauseRotationCoroutine(float seconds){
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
		Debug.Log("Camera rebooting.");
		Activate();
	}
}