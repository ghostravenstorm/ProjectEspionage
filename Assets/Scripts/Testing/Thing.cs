using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour{
	
	void Start(){
		Debug.Log("Sperms?");
		StartCoroutine("Routine");
	}

	private IEnumerator Routine(){

		yield return new WaitForSeconds(0.0001f);
		yield return StartCoroutine("Routine");
	}
}