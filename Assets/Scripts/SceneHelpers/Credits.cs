using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour, IInputController{

	void Start(){
		InputManager.instance.mainInput = this;
	}
	
	public void OnSubmit(){
		SceneManager.LoadScene("MenuScreen");
	}

	public void OnUpArrow(){}
	public void OnDownArrow(){}
	public void OnAgentInteract(){}
}