using UnityEngine;
using System.Collections;

public class LaserGrid : MonoBehaviour, IOverrideable{
	
	public DeviceState state{get; set;}

	public bool isActiveOnStart = true;
	public bool isDisabledOnTimer = false;
	public float rebootTimer = 0f;

	public bool isPulsing = false;
	public float pulseOnTimer = 0f;
	public float pulseOffTimer = 0f;

	private bool triggerOnce = true;

	void Start(){
		StartCoroutine("Pulse");
		if(isActiveOnStart)
			Activate();
		else
			Disable();
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player" && state == DeviceState.Active){
			Agent.instance.Kill(0);
		}
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.tag == "Player" && state == DeviceState.Active && triggerOnce){
			Agent.instance.Kill(0);
			triggerOnce = false;
		}
	}

	void OnTriggerExit(Collider colldier){
		triggerOnce = true;
	}

	public void Disable(){
		Debug.Log("Laser disabled.");
		state = DeviceState.Unactive;
		GetComponent<SpriteRenderer>().enabled = false;
		StopCoroutine("Pulse");
		if(isDisabledOnTimer) StartCoroutine(Reboot());
	}

	public void Activate(){
		state = DeviceState.Active;
		GetComponent<SpriteRenderer>().enabled = true;
		StartCoroutine("Pulse");
	}

	private IEnumerator Pulse(){
		while(true){
			if(isPulsing){
				state = DeviceState.Active;
				GetComponent<SpriteRenderer>().enabled = true;
				yield return new WaitForSeconds(pulseOnTimer);
				state = DeviceState.Unactive;
				GetComponent<SpriteRenderer>().enabled = false;
				yield return new WaitForSeconds(pulseOffTimer);
			}

			yield return null;
		}
	}

	private IEnumerator Reboot(){
		yield return new WaitForSeconds(rebootTimer);
		Debug.Log("Laser rebooting.");
		Activate();
	}

}